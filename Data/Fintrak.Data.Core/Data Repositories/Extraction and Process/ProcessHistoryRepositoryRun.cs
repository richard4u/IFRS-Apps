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
    [Export(typeof(IProcessHistoryRunRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProcessHistoryRunRepository : DataRepositoryBase<ProcessHistoryRun>, IProcessHistoryRunRepository
    {
        protected override ProcessHistoryRun AddEntity(CoreContext entityContext, ProcessHistoryRun entity)
        {
            return entityContext.Set<ProcessHistoryRun>().Add(entity);
        }

        protected override ProcessHistoryRun UpdateEntity(CoreContext entityContext, ProcessHistoryRun entity)
        {
            return (from e in entityContext.Set<ProcessHistoryRun>()
                    where e.ProcessHistoryRunId == entity.ProcessHistoryRunId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProcessHistoryRun> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ProcessHistoryRun>()
                   select e;
        }

        protected override ProcessHistoryRun GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProcessHistoryRun>()
                         where e.ProcessHistoryRunId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProcessHistoryRun> GetProcessHistoryRuns(int defaultCount)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = (from e in entityContext.Set<ProcessHistoryRun>().Take(defaultCount) //.OrderBy(c => c.AssetDescription).ThenBy(c => c.AssetType)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }
    }
}
