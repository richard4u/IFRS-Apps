using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IConditionalPDRepository : IDataRepository<ConditionalPD>
    {
        IEnumerable<ConditionalPD> ShowAllData();

        IEnumerable<ConditionalPD> GetConditionalPDBySearch(string searchParam, string path);
        IEnumerable<ConditionalPD> GetConditionalPDs(int defaultCount);
        IEnumerable<ConditionalPD> ExportConditionalPD(int defaultCount, string path);
        IEnumerable<string> GetDistinctAssetType();
        IEnumerable<ConditionalPD> GetConditionalPDByAssetType(string assetTypeVal);
        //IEnumerable<ConditionalPD> GetConditionalPDbyAssetType(string assetType);
    }
}
