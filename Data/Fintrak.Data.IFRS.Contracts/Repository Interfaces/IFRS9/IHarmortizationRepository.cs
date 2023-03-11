using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IHarmortizationRepository : IDataRepository<Harmortization>
    {
        //IEnumerable<Harmortization> ShowAllData();

        //IEnumerable<Harmortization> GetHarmortizationBySearch(string searchParam, string path);
        //IEnumerable<Harmortization> GetHarmortizations(int defaultCount);
        //IEnumerable<Harmortization> ExportHarmortization(int defaultCount, string path);
        //IEnumerable<string> GetDistinctAssetType();
        //IEnumerable<Harmortization> GetHarmortizationByAssetType(string assetTypeVal);
        //IEnumerable<Harmortization> GetHarmortizationbyAssetType(string assetType);
    }
}
