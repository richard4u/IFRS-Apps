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
    public class BalancesheetTrendRelated
    {
        #region  Balancesheet Breakdown stored procedure
        public DataTable BalancesheetBreakdown(DateTime RunDate, string ReportType,string MainCaption, string Company, string Currency,string DurationType)
        {
            DbConnection Connection = new DbConnection();

            DataTable dt = new DataTable();
            var newList = new List<string>();

            string connectionString = Connection.DatabaseConnectionString();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("Finstat_Report_BalancesheetOrPL_Sub_Trend", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_ReportType", Value = ReportType, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_MainCaption", Value = MainCaption, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Company", Value = Company, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Currency", Value = Currency, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_DurationType", Value = DurationType, });
               
               

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                con.Close();
            }  // using              
            return dt;
            //return newList;
        }
        #endregion

        #region  Balancesheet BSGLBreakdown stored procedure
        public DataTable BSGLBreakdown(DateTime RunDate, string ReportType, string MainCaption, string SubCaption, string Company, string Currency, string DurationType)
        {
            DbConnection Connection = new DbConnection();
           

            DataTable dt = new DataTable();
            var newList = new List<string>();

            string connectionString = Connection.DatabaseConnectionString();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("Finstat_Report_BalancesheetOrPL_Gl_Trend", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_ReportType", Value = ReportType, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_MainCaption", Value = MainCaption, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_SubCaption", Value = SubCaption, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Company", Value = Company, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Currency", Value = Currency, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_DurationType", Value = DurationType, });
                
        

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