using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanSetupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanSetupRepository : DataRepositoryBase<LoanSetup>, ILoanSetupRepository
    {

        protected override LoanSetup AddEntity(IFRSContext entityContext, LoanSetup entity)
        {
            return entityContext.Set<LoanSetup>().Add(entity);
        }

        protected override LoanSetup UpdateEntity(IFRSContext entityContext, LoanSetup entity)
        {
            return (from e in entityContext.Set<LoanSetup>() 
                    where e.LoanSetupId == entity.LoanSetupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanSetup> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanSetup>()
                   select e;
        }

        protected override LoanSetup GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanSetup>()
                         where e.LoanSetupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
