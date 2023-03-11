//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//using DocumentFormat.OpenXml.Drawing;
//using Fintrak.Client.MPR.Contracts;
//using Fintrak.Client.MPR.Proxies;
//using Fintrak.Presentation.WebClient.app.pages.ReportStoredProcedures;
//using Fintrak.Presentation.WebClient.Models;
//using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
//using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using MySqlConnector;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IFRSReporting
{
    public partial class FinstatReportViewerNote : System.Web.UI.Page
    {

        string ondrillthroughreport = "null";
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherSPPs sppObj = new OtherSPPs();

            string reportId = "Note";

            //string reportId = Request.QueryString["id"].ToString();
            if (!Page.IsPostBack)
            {


                string freshrundate = Request.QueryString["closedPeriods"].ToString();
                string RunDate = freshrundate.Substring(0, 10);

                string Currency = Request.QueryString["currencies"].ToString();


                //string RunDate = "2019-12-31";
                string Company = "HMB";
                //string Currency = "NGN";
                string QReport = "income";

           

                //string Period = Request.QueryString["period"].ToString();
                //string Year = Request.QueryString["year"].ToString();
                ////string reportId = Request.QueryString["id"].ToString();
                //string Currency = Request.QueryString["currency"].ToString();
                //string Path = Request.QueryString["path"].ToString();
                //string Type = Request.QueryString["type"].ToString();
                //string NRFF = Request.QueryString["nrff"].ToString();


                // Session 
                string MisCode = "";//SessionVariableModel.LoggedInMISCode;
                string Level = "";// SessionVariableModel.LoggedInLevel;
           

                // if (string.IsNullOrEmpty(Convert.ToString(SessionVariableModel.SelectedMISCode)))

                //if (!string.IsNullOrEmpty(SessionVariableModel.SelectedMISCode))
                //{
                //    MisCode = SessionVariableModel.SelectedMISCode;
                //    Level = SessionVariableModel.SelectedLevel;
                //}

                 // end of session

                ReportViewer1.Reset();
                ReportViewer1.Visible = true;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.DataSources.Clear();

          
                //================== Note report ============================================
             

                    ReportDataSource rdc1 = new ReportDataSource("PL", Income(RunDate, Company, Currency));
                    ReportDataSource rdc2 = new ReportDataSource("ReportColor", sppObj.ReportColor());
                    ReportDataSource rdc3 = new ReportDataSource("divisor", sppObj.ReportsDivisor());
                    ReportDataSource rdc4 = new ReportDataSource("Qualitative", sppObj.Qualitative(RunDate, QReport));
                    ReportDataSource rdc5 = new ReportDataSource("BS", Balancesheet(RunDate, Company, Currency));
                  

                    ReportViewer1.LocalReport.DataSources.Add(rdc1);
                    ReportViewer1.LocalReport.DataSources.Add(rdc2);
                    ReportViewer1.LocalReport.DataSources.Add(rdc3);
                    ReportViewer1.LocalReport.DataSources.Add(rdc4);
                    ReportViewer1.LocalReport.DataSources.Add(rdc5);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/app/reporting/views/IFRSReporting/Report/Finstat_ManagementNote.rdlc");

                    ReportParameter[] param = new ReportParameter[3];
                    param[0] = new ReportParameter("RunDate", Convert.ToString(RunDate));
                    param[1] = new ReportParameter("Company", Convert.ToString(Company));
                    param[2] = new ReportParameter("Currency", Convert.ToString(Currency));


                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();

              
            }

        }


        //=================== Various stored procedure methods =============================================================

        #region  Income statement stored procedure
        private DataTable Income(string RunDate, string Company, string Curreny)
        {
            DbConnection Connection = new DbConnection();

            DataTable dt = new DataTable();
            var newList = new List<string>();

            string connectionString = Connection.DatabaseConnectionString();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("finstat_Report_ManagementNotePL", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Company", Value = Company, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Currency", Value = Curreny, });
               

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                con.Close();
            }  // using              
            return dt;
            //return newList;
        }
        #endregion


        #region  Balancesheet stored procedure
        private DataTable Balancesheet(string RunDate, string Company, string Curreny)
        {
            DbConnection Connection = new DbConnection();

            DataTable dt = new DataTable();
            var newList = new List<string>();

            string connectionString = Connection.DatabaseConnectionString();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("finstat_Report_ManagementNoteBS", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Company", Value = Company, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Currency", Value = Curreny, });

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                con.Close();
            }  // using              
            return dt;
            //return newList;
        }
        #endregion



           
    }
}