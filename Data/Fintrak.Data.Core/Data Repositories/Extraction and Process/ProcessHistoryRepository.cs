using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Data.Core
{
    [Export(typeof(IProcessHistoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProcessHistoryRepository : DataRepositoryBase<ProcessHistory>, IProcessHistoryRepository
    {
        protected override ProcessHistory AddEntity(CoreContext entityContext, ProcessHistory entity)
        {
            return entityContext.Set<ProcessHistory>().Add(entity);
        }

        protected override ProcessHistory UpdateEntity(CoreContext entityContext, ProcessHistory entity)
        {
            return (from e in entityContext.Set<ProcessHistory>()
                    where e.ProcessHistoryId == entity.ProcessHistoryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProcessHistory> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ProcessHistory>()
                   select e;
        }

        protected override ProcessHistory GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProcessHistory>()
                         where e.ProcessHistoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProcessHistory> GetProcessHistorys(int defaultCount)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                //var query = (from e in entityContext.Set<ProcessHistory>().Take(defaultCount) //.OrderBy(c => c.AssetDescription).ThenBy(c => c.AssetType)
                //             select e).Take(defaultCount);
                //return query.ToArray();
                var query = (from e in entityContext.Set<ProcessHistory>().Take(defaultCount).OrderByDescending(c => c.CreatedOn) //.OrderBy(c => c.AssetDescription).ThenBy(c => c.datepmt)
                             select e);

                return query.ToArray();
            }
        }
    }
}
