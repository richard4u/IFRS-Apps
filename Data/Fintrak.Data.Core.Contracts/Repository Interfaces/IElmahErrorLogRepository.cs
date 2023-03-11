using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;


namespace Fintrak.Data.Core.Contracts
{
    public interface IElmahErrorLogRepository : IDataRepository<ElmahErrorLog>
    {
        IEnumerable<ElmahErrorLog> GetElmahErrorLogBySearch(string searchParam, string path);
        IEnumerable<ElmahErrorLog> GetElmahErrorLogs(int defaultCount);
        IEnumerable<ElmahErrorLog> ExportElmahErrorLog(int defaultCount, string path);
    }
}
