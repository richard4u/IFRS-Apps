#region Copyright 2009 Laimonas Simutis. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Elmah.Pgsql
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using Npgsql;
    using NpgsqlTypes;

    #endregion

    /// <summary>
    /// An <see cref="ErrorLog"/> implementation that uses PostgreSQL
    /// as its backing store.
    /// </summary>
    /// 
    public class PgsqlErrorLog : ErrorLog
    {
        private readonly string _connectionString;

        private const int _maxAppNameLength = 60;

        /// <summary>
        /// Initializes a new instance of the <see cref="PgsqlErrorLog"/> class
        /// using a dictionary of configured settings.
        /// </summary>

        public PgsqlErrorLog(IDictionary config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            var connectionString = ConnectionStringHelper.GetConnectionString(config);

            //
            // If there is no connection string to use then throw an 
            // exception to abort construction.
            //

            if (connectionString.Length == 0)
                throw new ApplicationException("Connection string is missing for the Postgres SQL error log.");

            _connectionString = connectionString;

            //
            // Set the application name as this implementation provides
            // per-application isolation over a single store.
            //

            var appName = config.Find("applicationName", string.Empty);

            if (appName.Length > _maxAppNameLength)
            {
                throw new ApplicationException(string.Format(
                    "Application name is too long. Maximum length allowed is {0} characters.",
                    _maxAppNameLength.ToString("N0")));
            }

            ApplicationName = appName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PgsqlErrorLog"/> class
        /// to use a specific connection string for connecting to the database.
        /// </summary>

        public PgsqlErrorLog(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

            if (connectionString.Length == 0)
                throw new ArgumentException(null, "connectionString");
            
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets the name of this error log implementation.
        /// </summary>
        
        public override string Name
        {
            get { return "PostgreSQL Error Log"; }
        }

        /// <summary>
        /// Gets the connection string used by the log to connect to the database.
        /// </summary>
        
        public virtual string ConnectionString
        {
            get { return _connectionString; }
        }

        public override string Log(Error error)
        {
            if (error == null)
                throw new ArgumentNullException("error");

            var errorXml = ErrorXml.EncodeString(error);
            var id = Guid.NewGuid();

            using (var connection = new NpgsqlConnection(ConnectionString))
            using (var command = Commands.LogError(id, this.ApplicationName, error.HostName, error.Type, error.Source, error.Message, error.User, error.StatusCode, error.Time, errorXml))
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                return id.ToString();
            }
        }

        public override ErrorLogEntry GetError(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (id.Length == 0) throw new ArgumentException(null, "id");

            Guid errorGuid;

            try
            {
                errorGuid = new Guid(id);
            }
            catch (FormatException e)
            {
                throw new ArgumentException(e.Message, "id", e);
            }

            string errorXml;

            using (var connection = new NpgsqlConnection(ConnectionString))
            using (var command = Commands.GetErrorXml(ApplicationName, errorGuid))
            {
                command.Connection = connection;
                connection.Open();
                errorXml = (string)command.ExecuteScalar();
            }

            if (errorXml == null)
                return null;

            var error = ErrorXml.DecodeString(errorXml);
            return new ErrorLogEntry(this, id, error);
        }

        public override int GetErrors(int pageIndex, int pageSize, ICollection<ErrorLogEntry> errorEntryList)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException("pageIndex", pageIndex, null);
            if (pageSize < 0) throw new ArgumentOutOfRangeException("pageSize", pageSize, null);

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = Commands.GetErrorsXml(this.ApplicationName, pageIndex, pageSize))
                {
                    command.Connection = connection;    
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetGuid(0);
                            var xml = reader.GetString(1);
                            var error = ErrorXml.DecodeString(xml);
                            errorEntryList.Add(new ErrorLogEntry(this, id.ToString(), error));
                        }
                    }
                }

                using (var command = Commands.GetErrorsXmlTotal(this.ApplicationName))
                {
                    command.Connection = connection;
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }


        private static class Commands
        {
            public static NpgsqlCommand LogError(
                Guid id,
                string appName,
                string hostName,
                string typeName,
                string source,
                string message,
                string user,
                int statusCode,
                DateTime time,
                string xml)
            {
                var command = new NpgsqlCommand();
                command.CommandText = 
@"
INSERT INTO Elmah_Error (ErrorId, Application, Host, Type, Source, Message, ""User"", StatusCode, TimeUtc, AllXml)
VALUES (@ErrorId, @Application, @Host, @Type, @Source, @Message, @User, @StatusCode, @TimeUtc, @AllXml)
";
                command.Parameters.Add(new NpgsqlParameter("ErrorId", id));
                command.Parameters.Add(new NpgsqlParameter("Application", appName));
                command.Parameters.Add(new NpgsqlParameter("Host", hostName));
                command.Parameters.Add(new NpgsqlParameter("Type", typeName));
                command.Parameters.Add(new NpgsqlParameter("Source", source));
                command.Parameters.Add(new NpgsqlParameter("Message", message));
                command.Parameters.Add(new NpgsqlParameter("User", user));
                command.Parameters.Add(new NpgsqlParameter("StatusCode", statusCode));
                command.Parameters.Add(new NpgsqlParameter("TimeUtc", time.ToUniversalTime()));
                command.Parameters.Add(new NpgsqlParameter("AllXml", xml));

                return command;
            }

            public static NpgsqlCommand GetErrorXml(string appName, Guid id)
            {
                var command = new NpgsqlCommand();

                command.CommandText = 
@"
SELECT AllXml FROM Elmah_Error 
WHERE 
    Application = @Application 
    AND ErrorId = @ErrorId
";

                command.Parameters.Add(new NpgsqlParameter("Application", appName));
                command.Parameters.Add(new NpgsqlParameter("ErrorId", id));

                return command;
            }

            public static NpgsqlCommand GetErrorsXml(string appName, int pageIndex, int pageSize)
            {
                var command = new NpgsqlCommand();

                command.CommandText =
@"
SELECT ErrorId, AllXml FROM Elmah_Error
WHERE
    Application = @Application
ORDER BY Sequence DESC
OFFSET @offset
LIMIT @limit
";

                command.Parameters.Add("@Application", NpgsqlDbType.Text, _maxAppNameLength).Value = appName;
                command.Parameters.Add("@offset", NpgsqlDbType.Integer).Value = (pageIndex) * pageSize;
                command.Parameters.Add("@limit", NpgsqlDbType.Integer).Value = pageSize;

                return command;
            }

            public static NpgsqlCommand GetErrorsXmlTotal(string appName)
            {
                var command = new NpgsqlCommand();
                command.CommandText = "SELECT COUNT(*) FROM Elmah_Error WHERE Application = @Application";
                command.Parameters.Add("@Application", NpgsqlDbType.Text, _maxAppNameLength).Value = appName;
                return command;
            }
        }
    }
}
