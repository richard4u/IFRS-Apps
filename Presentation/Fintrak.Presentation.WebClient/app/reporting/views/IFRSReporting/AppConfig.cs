
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//using System.Data.SqlClient;
using MySqlConnector;
using System.Configuration;


namespace SMS.Connection
{
    public class AppConfig
    {


        public string CnnStr = ConfigurationManager.ConnectionStrings["fintrakdbConnectionString"].ConnectionString;

        public MySqlConnection cnn = new MySqlConnection();



    }
}