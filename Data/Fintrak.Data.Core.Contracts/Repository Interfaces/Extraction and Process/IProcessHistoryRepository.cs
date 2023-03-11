using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public interface IProcessHistoryRepository : IDataRepository<ProcessHistory>
    {
        IEnumerable<ProcessHistory> GetProcessHistorys(int defaultCount);
    }
}
