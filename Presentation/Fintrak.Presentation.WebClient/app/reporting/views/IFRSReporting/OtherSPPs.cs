//using Fintrak.Client.MPR.Proxies;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using MySqlConnector;
using System.Linq;
using System.Web;

namespace IFRSReporting
{
    public class OtherSPPs
    {
        
        #region Report Color stored procedure
        public DataTable ReportColor()
        {
            DbConnection Connection = new DbConnection();
            string connectionString = Connection.DatabaseConnectionString();

            //string connectionString = mprcoreclient.DatabaseConnectionString();

            DataTable dt = new DataTable();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("GetReport_Color", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                //if (reader.HasRows)
                dt.Load(reader);

                con.Close();
            }  // using       
            return dt;
        }
        #endregion

        #region ReportsDivisor stored procedure
        public DataTable ReportsDivisor()
        {
            DbConnection Connection = new DbConnection();
            string connectionString = Connection.DatabaseConnectionString();

            //string connectionString = mprcoreclient.DatabaseConnectionString();

            DataTable dt = new DataTable();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("GetReport_Divisor", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                //if (reader.HasRows)
                dt.Load(reader);

                con.Close();
            }  // using       
            return dt;
        }
        #endregion




        #region Qualitative stored procedure
        public DataTable Qualitative(string RunDate,string QReport)
        {
            DbConnection Connection = new DbConnection();
            string connectionString = Connection.DatabaseConnectionString();

            //string connectionString = mprcoreclient.DatabaseConnectionString();

            DataTable dt = new DataTable();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("Finstat_Reports_QualitativeNotes", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Report", Value = QReport, });


                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                con.Close();
            }     
            return dt;
        }
        #endregion

      


        

    }
}