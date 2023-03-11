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
    public class BalancesheetRelated
    {
        #region  Balancesheet Breakdown stored procedure
        public DataTable BalancesheetBreakdown(DateTime RunDate, string MainCaption, string ReportType, string Company, string Currency, string BudgetType)
        {
            DbConnection Connection = new DbConnection();

            DataTable dt = new DataTable();
            var newList = new List<string>();

            string connectionString = Connection.DatabaseConnectionString();

            //string connectionString = mprcoreclient.DatabaseConnectionString();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("finstat_Report_BalancesheetOrPL_Sub", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_MainCaption", Value = MainCaption, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_ReportType", Value = ReportType, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Company", Value = Company, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Currency", Value = Currency, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_BudgetType", Value = BudgetType, });
               

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
        public DataTable BSGLBreakdown(DateTime RunDate, string MainCaption, string ReportType, string SubCaption, string Company, string Currency, string BudgetType)
        {
            DbConnection Connection = new DbConnection();
           

            DataTable dt = new DataTable();
            var newList = new List<string>();

            string connectionString = Connection.DatabaseConnectionString();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("finstat_Report_BalancesheetOrPL_Gl", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_MainCaption", Value = MainCaption, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_ReportType", Value = ReportType, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_SubCaption", Value = SubCaption, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Company", Value = Company, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Currency", Value = Currency, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_BudgetType", Value = BudgetType, });
           
           

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