using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Fintrak.Presentation.WebClient.Adapters
{
    public class WebSecurity
    {
        public static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakCoreDBConnection"].ConnectionString;
        public MySqlConnection connection = new MySqlConnection(connectionString);
        private object textBox1;

        public bool TableExists()
        {
            string cmdStr = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'fintrakcoredbhmb' AND table_name = 'cor_usersetup'";
            bool tableExist = false;
            MySqlCommand cmd = new MySqlCommand(cmdStr, connection);
            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())

            {

                int count = reader.GetInt32(0);

                if (count == 1)
                {
                    tableExist = true;
                    return tableExist;
                }
                else
                {
                    tableExist = false;
                    return tableExist;
                }
            }
            return tableExist;
        }

        public void InitializeDatabaseConnection(string connectionStringName, string userTableName, string userIdcolumn, string userNameColumn, bool autoCreateTables)
        {
            if (!TableExists())
            {
                connection.Open();
                MySqlCommand Create_table = new MySqlCommand("CREATE TABLE `"+ userTableName + "` ("+
                                                            "`"+ userIdcolumn + "` int NOT NULL,"+
                                                            "`"+ userNameColumn + "` varchar(200) NOT NULL,"+
                                                            "`Name` varchar(200) NOT NULL," +
                                                            "`Email` varchar(200) NOT NULL," +
                                                            "`StaffID` varchar(200) DEFAULT NULL," +
                                                            "`Photo` longblob," +
                                                            "`PhotoUrl` longtext," +
                                                            "`IsApplicationUser` tinyint(1) DEFAULT '0'," +
                                                            "`IsReportUser` tinyint(1) DEFAULT '0'," +
                                                            "`MultiCompanyAccess` tinyint(1) DEFAULT NULL," +
                                                            "`LatestConnection` datetime(6) DEFAULT NULL," +
                                                            "`CompanyCode` varchar(10) DEFAULT NULL," +
                                                            "`Active` tinyint(1) DEFAULT '1'," +
                                                            "`Deleted` tinyint(1) DEFAULT '0'," +
                                                            "`CreatedBy` varchar(50) DEFAULT 'auto'," +
                                                            "`CreatedOn` datetime DEFAULT CURRENT_TIMESTAMP," +
                                                            "`UpdatedBy` varchar(50) DEFAULT 'auto'," +
                                                            "`UpdatedOn` datetime DEFAULT CURRENT_TIMESTAMP," +
                                                            "`RowVersion` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP," +
                                                            "PRIMARY KEY(`UserSetupId`)," +
                                                            "UNIQUE KEY `CK_cor_usersetup_Email` (`Email`)," +
                                                            "UNIQUE KEY `CK_cor_usersetup_LoginID` (`LoginID`)" +
                                                            ") ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci; ", connection);
                Create_table.ExecuteNonQuery();
            }

        }

        public bool UserExists (string username)
        {
            connection.Open();

            bool usernameExists = false;

            MySqlCommand cmd1 = new MySqlCommand("SELECT LoginID FROM cor_usersetup WHERE LoginID = "+username+" LIMIT 1", connection);
            cmd1.Parameters.AddWithValue(username, username);

            usernameExists = (int)cmd1.ExecuteScalar() > 0;

            return usernameExists;

        }

        public void CreateUserAndAccount (string userName, string password, object propertyValues = null, bool requireConfirmationToken = false)
        {

        }


        private void GetDBData()
        {
            //try
            //{
            //    // prepare connection query 
            //    string strQuery = "SELECT users.Username, users.Password " + "FROM users " + "WHERE Username='User'";

            //}
            // '[UserSetupId] FROM [cor_usersetup] WHERE (UPPER([LoginID]) = UPPER('fintrak'))'
        }
    }
}