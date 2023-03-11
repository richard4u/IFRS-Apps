using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.Core
{
    [Export(typeof(IElmahErrorLogRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ElmahErrorLogRepository : DataRepositoryBase<ElmahErrorLog>, IElmahErrorLogRepository
    {
        protected override ElmahErrorLog AddEntity(CoreContext entityContext, ElmahErrorLog entity)
        {
            return entityContext.Set<ElmahErrorLog>().Add(entity);
        }

        protected override ElmahErrorLog UpdateEntity(CoreContext entityContext, ElmahErrorLog entity)
        {
            return (from e in entityContext.Set<ElmahErrorLog>()
                    where e.Sequence == entity.Sequence
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<ElmahErrorLog> GetEntities(CoreContext entityContext)
        {
            var query = from e in entityContext.Set<ElmahErrorLog>()
                        select e;
            query = query.OrderBy(a => a.Sequence).GroupBy(e => e.StatusCode).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override ElmahErrorLog GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ElmahErrorLog>()
                         where e.Sequence == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ElmahErrorLog> GetElmahErrorLogBySearch(string searchParam, string path)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<ElmahErrorLog>()
                                 where searchParam.Contains(e.Message)
                                 orderby e.Type
                                 select new
                                 {
                                     e.ErrorId,
                                     e.Application,
                                     e.Host,
                                     e.Type,
                                     e.Source,
                                     e.Message,
                                     e.User,
                                     e.StatusCode,
                                     e.TimeUtc,
                                     e.Sequence,
                                     e.AllXml,
                                     e.VisitorsIPAddress,
                                     e.Manufacturer
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.Message }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).Message : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).Message;
                            response = ExportHandler.Export(query.Where(e => e.Message == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<ElmahErrorLog>().Take(0).ToArray().OrderBy(c => c.Type).ThenBy(c => c.StatusCode); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<ElmahErrorLog>()
                                 where e.Message.Contains(searchParam)
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<ElmahErrorLog> GetElmahErrorLogs(int defaultCount)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = (from e in entityContext.Set<ElmahErrorLog>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<ElmahErrorLog> ExportElmahErrorLog(int defaultCount, string path)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<ElmahErrorLog>()
                                 select new
                                 {
                                     e.ErrorId,
                                     e.Application,
                                     e.Host,
                                     e.Type,
                                     e.Source,
                                     e.Message,
                                     e.User,
                                     e.StatusCode,
                                     e.TimeUtc,
                                     e.Sequence,
                                     e.AllXml,
                                     e.VisitorsIPAddress,
                                     e.Manufacturer
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<ElmahErrorLog>().Take(0).ToArray().OrderBy(c => c.Type).ThenBy(c => c.Message);

                }
                else
                {
                    var query = (from e in entityContext.Set<ElmahErrorLog>().OrderByDescending(c => c.TimeUtc).Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
