﻿using Fintrak.Shared.SystemCore.Entities;
//using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
//using System.Data.SqlClient;
using System.Linq;

namespace Fintrak.Data.SystemCore
{
    public static class LogDataManager
    {
        #region Elmah

        public static LogEvent GetELMAHErrorLogById(string Id)
        {
            var logEvents = new List<LogEvent>();

            var connectionString = ConfigurationManager.ConnectionStrings["FintrakLogDBConnection"].ConnectionString;
            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("GetELMAHErrorById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter("ErrorId", Id));

                con.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var logEvent = new LogEvent()
                    {
                        IdType = "guid",
                        Id = "",
                        IdAsInteger = 0,
                        IdAsGuid = reader["ErrorId"] != DBNull.Value ? (Guid)reader["ErrorId"] : Guid.Empty,
                        LoggerProviderName = "Elmah",
                        LogDate = reader["TimeUtc"] != DBNull.Value ? DateTime.Parse(reader["TimeUtc"].ToString()) : DateTime.MinValue,
                        MachineName = reader["Host"] != DBNull.Value ? reader["Host"].ToString() : string.Empty,
                        Message = reader["Message"] != DBNull.Value ? reader["Message"].ToString() : string.Empty,
                        Type = reader["Type"] != DBNull.Value ? reader["Type"].ToString() : string.Empty,
                        Level = "Error",
                        Source = reader["Source"] != DBNull.Value ? reader["Source"].ToString() : string.Empty,
                        StackTrace = "",
                        AllXml = reader["AllXml"] != DBNull.Value ? reader["AllXml"].ToString() : string.Empty

                    };
                }

                con.Close();
            }

            return logEvents.FirstOrDefault();
        }

        public static List<LogEvent> GetELMAHErrorLogByDateRangeAndType(DateTime start, DateTime end, string logLevel)
        {
            var logEvents = new List<LogEvent>();

            var connectionString = ConfigurationManager.ConnectionStrings["FintrakLogDBConnection"].ConnectionString;
            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("GetELMAHErrors", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter("StartDate", start));
                cmd.Parameters.Add(new MySqlParameter("EndDate", end));
                cmd.Parameters.Add(new MySqlParameter("LogLevel", logLevel));

                con.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var logEvent = new LogEvent()
                    {
                        IdType = "guid",
                        Id = reader["ErrorId"] != DBNull.Value ? reader["ErrorId"].ToString() : string.Empty,
                        IdAsInteger = 0,
                        IdAsGuid = reader["ErrorId"] != DBNull.Value ? (Guid)reader["ErrorId"] : Guid.Empty,
                        LoggerProviderName = "Elmah",
                        LogDate = reader["TimeUtc"] != DBNull.Value ? DateTime.Parse(reader["TimeUtc"].ToString()) : DateTime.MinValue,
                        MachineName = reader["Host"] != DBNull.Value ? reader["Host"].ToString() : string.Empty,
                        Message = reader["Message"] != DBNull.Value ? reader["Message"].ToString() : string.Empty,
                        Type = reader["Type"] != DBNull.Value ? reader["Type"].ToString() : string.Empty,
                        Level = "Error",
                        Source = reader["Source"] != DBNull.Value ? reader["Source"].ToString() : string.Empty,
                        StackTrace = ""

                    };

                    logEvents.Add(logEvent);
                }

                con.Close();
            }

            return logEvents;
        }

        public static void ClearELMAHErrorLog(DateTime start, DateTime end, string[] logLevels)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FintrakLogDBConnection"].ConnectionString;
            using (var con = new MySqlConnection(connectionString))
            {
                string commandText = "delete from Elmah_Error WHERE TimeUtc >= @p0 and TimeUtc <= @p1";
                var cmd = new MySqlCommand(commandText, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 0;

                MySqlParameter paramStartDate = new MySqlParameter { ParameterName = "p0", Value = start.ToUniversalTime(), DbType = System.Data.DbType.DateTime };
                MySqlParameter paramEndDate = new MySqlParameter { ParameterName = "p1", Value = end.ToUniversalTime(), DbType = System.Data.DbType.DateTime };

                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        #endregion
       
    }
}
