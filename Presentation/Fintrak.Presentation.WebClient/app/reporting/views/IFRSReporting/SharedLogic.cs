using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.IO;
using MySql.Data.MySqlClient;

public class SharedLogic
{
    public static DataTable GetRundates()
    {
        string conection = ConfigurationManager.ConnectionStrings["FintrakDBConnection1"].ConnectionString;

        return SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "dropdown_Date_Grp").Tables[0];
    }

    public static DataTable GetCompanies()
    {
        string conection = ConfigurationManager.ConnectionStrings["FintrakDBConnection1"].ConnectionString;

        return MySqlHelper.ExecuteDataset(conection, "finstat_GetSubsidiaries").Tables[0];
    }

    public static DataTable GetCurrency()
    {
        string conection = ConfigurationManager.ConnectionStrings["FintrakDBConnection1"].ConnectionString;

        return SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "finstat_GetCurrency").Tables[0];
    }

    public static DataTable GetBranch()
    {
        string conection = ConfigurationManager.ConnectionStrings["FintrakDBConnection1"].ConnectionString;

        return SqlHelper.ExecuteDataset(conection, CommandType.StoredProcedure, "finstat_GetBranches").Tables[0];
    }




    public static DateTime? GetPreviousRunDate(DateTime value)
    {
        string conection = ConfigurationManager.ConnectionStrings["FintrakDBConnection1"].ConnectionString;

        DataTable table;
        table = SqlHelper.ExecuteDataset(conection, CommandType.Text, "select PrevDate = IFRS_FNPreviousRunDate('" + value + "')").Tables[0];

     

        if (table.Rows[0][0] + "" == "")
            return new DateTime(1900, 1, 1);
        else
            return DateTime.Parse(table.Rows[0][0].ToString());
        //dss.Tables["ITdata"].Rows[0]["pudate"].ToString();
    }







}
