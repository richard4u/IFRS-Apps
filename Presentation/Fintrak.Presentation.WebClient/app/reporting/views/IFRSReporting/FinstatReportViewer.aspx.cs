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
//using System.Data.SqClient;
using MySqlConnector;
//using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.Composition;
using Fintrak.Client.SystemCore.Contracts;

namespace IFRSReporting
{
    public partial class FinstatReportViewer : System.Web.UI.Page
    {

        string ondrillthroughreport = "null";
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherSPPs sppObj = new OtherSPPs();

            string reportId = "income";

            //string reportId = Request.QueryString["id"].ToString();
            if (!Page.IsPostBack)
            {
                //RunDate.DataSource = SharedLogic.GetRundates();
                //RunDate.DataBind();


                //Company.DataSource = SharedLogic.GetCompanies();
                //Company.DataBind();

                //Currency.DataSource = SharedLogic.GetCurrency();
                //Currency.DataBind();



                string freshrundate = Request.QueryString["closedPeriods"].ToString();
                string RunDate = freshrundate.Substring(0, 10);
                //string RunDate = Request.QueryString["closedPeriods"].ToString();

                string Currency = Request.QueryString["currencies"].ToString();
                string QReport = "income";
                Int32.TryParse(Request.QueryString["budgettype"].ToString(), out int budgettype1);
                int BudgetType = budgettype1;

                string BSReportType = "BS";
                string PLReportType = "PL";


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


                //================== balancesheet report ============================================
                if (reportId.Trim().ToLower() == "balancesheet")
                {
                    string Company = "HMB";
                    string ReportType = "BS";

                    ReportDataSource rdc1 = new ReportDataSource("Balansheet", BalancesheetReport(RunDate, Company, ReportType, Currency, BudgetType));
                    ReportDataSource rdc2 = new ReportDataSource("ReportColor", sppObj.ReportColor());
                    ReportDataSource rdc3 = new ReportDataSource("divisor", sppObj.ReportsDivisor());
                    ReportDataSource rdc4 = new ReportDataSource("Qualitative", sppObj.Qualitative(RunDate, QReport));



                    ReportViewer1.LocalReport.DataSources.Add(rdc1);
                    ReportViewer1.LocalReport.DataSources.Add(rdc2);
                    ReportViewer1.LocalReport.DataSources.Add(rdc3);
                    ReportViewer1.LocalReport.DataSources.Add(rdc4);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/app/reporting/views/IFRSReporting/Report/Finstat_Balancesheet.rdlc");

                    ReportParameter[] param = new ReportParameter[5];
                    param[0] = new ReportParameter("RunDate", Convert.ToString(RunDate));
                    param[1] = new ReportParameter("Company", Convert.ToString(Company));
                    param[2] = new ReportParameter("ReportType", Convert.ToString(ReportType));
                    param[3] = new ReportParameter("Currency", Convert.ToString(Currency));
                    param[4] = new ReportParameter("BudgetType", Convert.ToString(BudgetType));

                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();

                }

                //================== Income report ============================================
                else if (reportId.Trim().ToLower() == "income")
                {
                    string Company = "HMB";
                    string ReportType = "PL";

                    ReportDataSource rdc1 = new ReportDataSource("Income", IncomeReport(RunDate, Company, ReportType, Currency, BudgetType));
                    ReportDataSource rdc2 = new ReportDataSource("ReportColor", sppObj.ReportColor());
                    ReportDataSource rdc3 = new ReportDataSource("divisor", sppObj.ReportsDivisor());
                    ReportDataSource rdc4 = new ReportDataSource("Qualitative", sppObj.Qualitative(RunDate, QReport));
                    ReportDataSource rdc5 = new ReportDataSource("OCI", IncomeReport(RunDate, Company, ReportType, Currency, BudgetType));


                    ReportViewer1.LocalReport.DataSources.Add(rdc1);
                    ReportViewer1.LocalReport.DataSources.Add(rdc2);
                    ReportViewer1.LocalReport.DataSources.Add(rdc3);
                    ReportViewer1.LocalReport.DataSources.Add(rdc4);
                    ReportViewer1.LocalReport.DataSources.Add(rdc5);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/app/reporting/views/IFRSReporting/Report/Finstat_IncomeStatement.rdlc");

                    ReportParameter[] param = new ReportParameter[5];
                    param[0] = new ReportParameter("RunDate", Convert.ToString(RunDate));
                    param[1] = new ReportParameter("Company", Convert.ToString(Company));
                    param[2] = new ReportParameter("ReportType", Convert.ToString(ReportType));
                    param[3] = new ReportParameter("Currency", Convert.ToString(Currency));
                    param[4] = new ReportParameter("BudgetType", Convert.ToString(BudgetType));


                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();

                }

            }



            if (reportId.Trim().ToLower() == "balancesheet")
            {

                ReportViewer1.Drillthrough += new DrillthroughEventHandler(OnDrillthroughBalancesheetBreakdown);
                ReportViewer1.Drillthrough += new DrillthroughEventHandler(OnDrillthroughBSGLBreakdown);
            }

            else if (reportId.Trim().ToLower() == "income")
            {
                ReportViewer1.Drillthrough += new DrillthroughEventHandler(OnDrillthroughIncomeBreakdown);
                ReportViewer1.Drillthrough += new DrillthroughEventHandler(OnDrillthroughIncomeGLBreakdown);

            }
        }


        //=================== Various stored procedure methods =============================================================

        #region  Balancesheet stored procedure
        private DataTable BalancesheetReport(string RunDate, string Company, string ReportType, string Currency, int BudgetType)
        {
            DbConnection mprcoreclient = new DbConnection();

            DataTable dt = new DataTable();
            var newList = new List<string>();

            string connectionString = mprcoreclient.DatabaseConnectionString();


            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("finstat_Report_BalancesheetOrPL", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Company", Value = Company, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_ReportType", Value = ReportType, });
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


        #region  Income statement stored procedure
        private DataTable IncomeReport(string RunDate, string Company, string ReportType, string Curreny, int BudgetType)
        {
            DbConnection Connection = new DbConnection();

            DataTable dt = new DataTable();
            var newList = new List<string>();

            string connectionString = Connection.DatabaseConnectionString();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("finstat_Report_BalancesheetOrPL", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Company", Value = Company, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_ReportType", Value = ReportType, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_Currency", Value = Curreny, });
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


        #region  Income statement stored procedure
        private DataTable OCIReport(string RunDate, string ReportType, string Company, string Curreny)
        {
            DbConnection Connection = new DbConnection();

            DataTable dt = new DataTable();
            var newList = new List<string>();

            string connectionString = Connection.DatabaseConnectionString();

            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("finstat_Report_BalancesheetOrPL", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_RunDate", Value = RunDate, });
                cmd.Parameters.Add(new MySqlParameter { ParameterName = "p_ReportType", Value = ReportType, });
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




        //=================== Balancesheet Drillthrough report ===============================

        #region Drillthrough Balancesheet Breakdown
        protected void OnDrillthroughBalancesheetBreakdown(object sender, DrillthroughEventArgs e)
        {
            BalancesheetRelated br = new BalancesheetRelated();
            OtherSPPs sppObj = new OtherSPPs();

            LocalReport localReport = (LocalReport)e.Report;
            ondrillthroughreport = localReport.ReportPath;
            if (ondrillthroughreport.Trim().Contains("Finstat_Balancesheet_SubBS.rdlc"))
            {

                ReportParameterInfoCollection DrillThroughValues = localReport.GetParameters();
                DateTime RunDate = Convert.ToDateTime(DrillThroughValues["RunDate"].Values[0]);
                string MainCaption = Convert.ToString(DrillThroughValues["MainCaption"].Values[0]);
                string ReportType = Convert.ToString(DrillThroughValues["ReportType"].Values[0]);
                string Company = Convert.ToString(DrillThroughValues["Company"].Values[0]);
                string Currency = Convert.ToString(DrillThroughValues["Currency"].Values[0]);
                string BudgetType = Convert.ToString(DrillThroughValues["BudgetType"].Values[0]);


                DataTable bs = br.BalancesheetBreakdown(RunDate, MainCaption, ReportType, Company, Currency, BudgetType);

                DataTable ReportColorList = sppObj.ReportColor();
                DataTable ReportsDivisor = sppObj.ReportsDivisor();

                localReport.DataSources.Add(new ReportDataSource("FinstatBalancesheetSub", bs));
                localReport.DataSources.Add(new ReportDataSource("divisor", ReportsDivisor));
                localReport.DataSources.Add(new ReportDataSource("ReportColor", ReportColorList));
                localReport.Refresh();
            }
        }
        #endregion


        #region Drillthrough BSBreakdown
        private void OnDrillthroughBSGLBreakdown(object sender, DrillthroughEventArgs e)
        {
            BalancesheetRelated br = new BalancesheetRelated();
            OtherSPPs sppObj = new OtherSPPs();

            LocalReport localReport = (LocalReport)e.Report;
            ondrillthroughreport = localReport.ReportPath;

            if (ondrillthroughreport.Trim().Contains("Finstat_BalancesheetGlBS.rdlc"))
            {
                //localReport.DataSources.Clear();
                /*Collect report parameter from drillthrough report*/
                ReportParameterInfoCollection DrillThroughValues = localReport.GetParameters();
                DateTime RunDate = Convert.ToDateTime(DrillThroughValues["RunDate"].Values[0]);
                string MainCaption = Convert.ToString(DrillThroughValues["MainCaption"].Values[0]);
                string ReportType = Convert.ToString(DrillThroughValues["ReportType"].Values[0]);
                string SubCaption = Convert.ToString(DrillThroughValues["SubCaption"].Values[0]);
                string Company = Convert.ToString(DrillThroughValues["Company"].Values[0]);
                string Currency = Convert.ToString(DrillThroughValues["Currency"].Values[0]);
                string BudgetType = Convert.ToString(DrillThroughValues["BudgetType"].Values[0]);


                DataTable bsgl = br.BSGLBreakdown(RunDate, MainCaption, ReportType, SubCaption, Company, Currency, BudgetType);
                DataTable ReportColorList = sppObj.ReportColor();
                DataTable ReportsDivisor = sppObj.ReportsDivisor();

                localReport.DataSources.Add(new ReportDataSource("Finstat_BalancesheetGL", bsgl));
                localReport.DataSources.Add(new ReportDataSource("divisor", ReportsDivisor));
                localReport.DataSources.Add(new ReportDataSource("ReportColor", ReportColorList));

                localReport.Refresh();
            }
        }
        #endregion




        //=================== Income Drillthrough report ===============================

        #region Drillthrough Balancesheet Breakdown
        protected void OnDrillthroughIncomeBreakdown(object sender, DrillthroughEventArgs e)
        {
            IncomeRelated br = new IncomeRelated();
            OtherSPPs sppObj = new OtherSPPs();

            LocalReport localReport = (LocalReport)e.Report;
            ondrillthroughreport = localReport.ReportPath;
            if (ondrillthroughreport.Trim().Contains("Finstat_Income_SubPL.rdlc"))
            {

                ReportParameterInfoCollection DrillThroughValues = localReport.GetParameters();
                DateTime RunDate = Convert.ToDateTime(DrillThroughValues["RunDate"].Values[0]);
                string MainCaption = Convert.ToString(DrillThroughValues["MainCaption"].Values[0]);
                string ReportType = Convert.ToString(DrillThroughValues["ReportType"].Values[0]);
                string Company = Convert.ToString(DrillThroughValues["Company"].Values[0]);
                string Currency = Convert.ToString(DrillThroughValues["Currency"].Values[0]);
                string BudgetType = Convert.ToString(DrillThroughValues["BudgetType"].Values[0]);


                DataTable pl = br.IncomeBreakdown(RunDate, MainCaption, ReportType, Company, Currency, BudgetType);

                DataTable ReportColorList = sppObj.ReportColor();
                DataTable ReportsDivisor = sppObj.ReportsDivisor();

                localReport.DataSources.Add(new ReportDataSource("IncomeSub", pl));
                localReport.DataSources.Add(new ReportDataSource("divisor", ReportsDivisor));
                localReport.DataSources.Add(new ReportDataSource("ReportColor", ReportColorList));
                localReport.Refresh();
            }
        }
        #endregion


        #region Drillthrough BSBreakdown
        private void OnDrillthroughIncomeGLBreakdown(object sender, DrillthroughEventArgs e)
        {
            IncomeRelated br = new IncomeRelated();
            OtherSPPs sppObj = new OtherSPPs();

            LocalReport localReport = (LocalReport)e.Report;
            ondrillthroughreport = localReport.ReportPath;

            if (ondrillthroughreport.Trim().Contains("Finstat_IncomeGlPL.rdlc"))
            {
                ReportParameterInfoCollection DrillThroughValues = localReport.GetParameters();
                DateTime RunDate = Convert.ToDateTime(DrillThroughValues["RunDate"].Values[0]);
                string MainCaption = Convert.ToString(DrillThroughValues["MainCaption"].Values[0]);
                string ReportType = Convert.ToString(DrillThroughValues["ReportType"].Values[0]);
                string SubCaption = Convert.ToString(DrillThroughValues["SubCaption"].Values[0]);
                string Company = Convert.ToString(DrillThroughValues["Company"].Values[0]);
                string Currency = Convert.ToString(DrillThroughValues["Currency"].Values[0]);
                string BudgetType = Convert.ToString(DrillThroughValues["BudgetType"].Values[0]);


                DataTable plgl = br.IncomeGLBreakdown(RunDate, MainCaption, ReportType, SubCaption, Company, Currency, BudgetType);
                DataTable ReportColorList = sppObj.ReportColor();
                DataTable ReportsDivisor = sppObj.ReportsDivisor();

                localReport.DataSources.Add(new ReportDataSource("Finstat_IncomeGL", plgl));
                localReport.DataSources.Add(new ReportDataSource("divisor", ReportsDivisor));
                localReport.DataSources.Add(new ReportDataSource("ReportColor", ReportColorList));

                localReport.Refresh();
            }
        }
        #endregion


    }
}