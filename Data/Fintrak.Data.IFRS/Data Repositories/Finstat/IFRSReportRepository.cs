using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
//using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using MySqlConnector;
//using MySql.Data.MySqlClient;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIFRSReportRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSReportRepository : DataRepositoryBase<IFRSReport>, IIFRSReportRepository
    {

        protected override IFRSReport AddEntity(IFRSContext entityContext, IFRSReport entity)
        {
            return entityContext.Set<IFRSReport>().Add(entity);
        }

        protected override IFRSReport UpdateEntity(IFRSContext entityContext, IFRSReport entity)
        {
            return (from e in entityContext.Set<IFRSReport>()
                    where e.IFRSReportId == entity.IFRSReportId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSReport> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IFRSReport>()
                   select e;
        }

        protected override IFRSReport GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSReport>()
                         where e.IFRSReportId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IFRSReport> GetAllRunDate()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = IFRSContext.GetDataConnection();

            var ifrsreports = new List<IFRSReport>();
            using (var con = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand("spp_ifrs_report_rundates", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var report = new IFRSReport();

                    if (reader["RunDate"] != DBNull.Value)
                        report.RunDate = DateTime.Parse(reader["RunDate"].ToString());
                    ifrsreports.Add(report);
                }

                con.Close();
            }

            return ifrsreports;
        }
    }
}
