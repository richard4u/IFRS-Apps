using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IUserSetupAzureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserSetupAzureRepository : DataRepositoryBase<UserSetupAzure>, IUserSetupAzureRepository
    {
        protected override UserSetupAzure AddEntity(SystemCoreContext entityContext, UserSetupAzure entity)
        {
            return entityContext.Set<UserSetupAzure>().Add(entity);
        }

        protected override UserSetupAzure UpdateEntity(SystemCoreContext entityContext, UserSetupAzure entity)
        {
            return (from e in entityContext.Set<UserSetupAzure>()
                    where e.UserSetupAzureId == entity.UserSetupAzureId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserSetupAzure> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<UserSetupAzure>()
                   select e;
        }

        protected override UserSetupAzure GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UserSetupAzure>()
                         where e.UserSetupAzureId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public UserSetupAzure GetByLoginID(string loginID)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.UserSetupAzureSet
                            where a.LoginID == loginID
                            select a;

                return query.FirstOrDefault();
            }
        }
    }
}
