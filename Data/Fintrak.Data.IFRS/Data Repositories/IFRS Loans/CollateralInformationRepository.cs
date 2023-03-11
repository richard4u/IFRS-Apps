using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICollateralInformationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CollateralInformationRepository : DataRepositoryBase<CollateralInformation>, ICollateralInformationRepository
    {

        protected override CollateralInformation AddEntity(IFRSContext entityContext, CollateralInformation entity)
        {
            return entityContext.Set<CollateralInformation>().Add(entity);
        }

        protected override CollateralInformation UpdateEntity(IFRSContext entityContext, CollateralInformation entity)
        {
            return (from e in entityContext.Set<CollateralInformation>()
                    where e.CollateralInformationId == entity.CollateralInformationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CollateralInformation> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CollateralInformation>()
                   select e;
        }

        protected override CollateralInformation GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CollateralInformation>()
                         where e.CollateralInformationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<CollateralDetailsInfo> GetCollateralDetails()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.CollateralTypeSet
                            join b in entityContext.CollateralCategorySet on a.CategoryCode equals b.Code
                            join c in entityContext.CollateralInformationSet on a.Code equals c.Type
                            select new CollateralDetailsInfo()
                            {
                                CollateralType = a,
                                CollateralCategory = b,
                                CollateralInformation = c
                            };

                return query.ToFullyLoaded();
            }
        }
 
    }
}
