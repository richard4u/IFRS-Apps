﻿
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public interface IUserSetupAzureRepository : IDataRepository<UserSetupAzure>
    {
        UserSetupAzure GetByLoginID(string loginID);
    }
}
