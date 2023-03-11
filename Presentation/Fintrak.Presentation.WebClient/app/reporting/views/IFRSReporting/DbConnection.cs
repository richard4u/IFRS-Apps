using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Fintrak.Client.IFRS.Proxies;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Proxies;
using Fintrak.Client.SystemCore.Entities;

namespace IFRSReporting
{
    public class DbConnection
    {

        //public string DatabaseConnectionString = ConfigurationManager.ConnectionStrings["fintrakdbConnectionString"].ConnectionString;

        public string DatabaseConnectionString()
        {
            FinstatClient coreManager = new FinstatClient();
            string connection = coreManager.GetDataConnection();
            return connection;
        }
       
    }
}