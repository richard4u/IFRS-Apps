using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;
using System.Linq.Dynamic;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IHarmortizationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HarmortizationRepository : DataRepositoryBase<Harmortization>, IHarmortizationRepository
    {
        protected override Harmortization AddEntity(IFRSContext entityContext, Harmortization entity)
        {
            return entityContext.Set<Harmortization>().Add(entity);
        }

        protected override Harmortization UpdateEntity(IFRSContext entityContext, Harmortization entity)
        {
            return (from e in entityContext.Set<Harmortization>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Harmortization> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Harmortization>()
                   select e;
        }

        protected override Harmortization GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Harmortization>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<Harmortization> ShowAllData()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<Harmortization>()
                             select e);
                return query.ToArray();
            }
        }


        //public IEnumerable<Harmortization> GetHarmortizationBySearch(string searchParam, string path)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        if (searchParam.Contains("ExportData "))
        //        {
        //            searchParam = searchParam.Replace("ExportData ", "");
        //            var query = (from e in entityContext.Set<Harmortization>()
        //                         where searchParam.Contains(e.AssetDescription)
        //                         //orderby e.AssetDescription, e.AssetType
        //                         select new
        //                         {
        //                             e.AssetDescription,
        //                             e.AssetType,
        //                             e.Counterparty,
        //                             e.RatingAgency,
        //                             e.CreditRating,
        //                             e.SandPRating,
        //                             e.PD1,
        //                             e.PD2,
        //                             e.PD3,
        //                             e.PD4,
        //                             e.PD5,
        //                             e.PD6,
        //                             e.PD7,
        //                             e.PD8,
        //                             e.PD9,
        //                             e.PD10,
        //                             e.PD11,
        //                             e.PD12,
        //                             e.PD13
        //                         });

        //            if (searchParam.Substring(0, 5) == "split")
        //            {
        //                searchParam = searchParam.Substring(5, searchParam.Length - 5);
        //                var accounts = (from e in query select new { e.AssetDescription }).Distinct();
        //                var count = accounts.Count();
        //                var ExportHandler = new ExcelService(path);
        //                var accountNo = count > 0 ? accounts.ToList().ElementAt(0).AssetDescription : "";
        //                string response = null;
        //                for (int i = 0; i < count; ++i)
        //                {
        //                    accountNo = accounts.ToList().ElementAt(i).AssetDescription;
        //                    response = ExportHandler.Export(query.Where(e => e.AssetDescription == accountNo).ToList(), path + accountNo.Replace("/", ""));
        //                }
        //            }
        //            else
        //            {
        //                var ExportHandler = new ExcelService(path);
        //                string response = ExportHandler.Export(query.ToList(), path);
        //            }

        //            return new List<Harmortization>().Take(0).ToArray();
        //        }
        //        else
        //        {
        //            var query = (from e in entityContext.Set<Harmortization>()
        //                         where e.AssetDescription == searchParam
        //                         //orderby e.AssetDescription, e.datepmt
        //                         select e);

        //            return query.ToArray();
        //        }
        //    }
        //}


        //public IEnumerable<Harmortization> GetHarmortizationByAssetType(string assetTypeVal)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        var query = (from e in entityContext.Set<Harmortization>()
        //                     where e.AssetType == assetTypeVal
        //                     //orderby e.AssetDescription, e.datepmt
        //                     select e);

        //        return query.ToArray();
        //    }
        //}


        public IEnumerable<Harmortization> GetHarmortizations(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<Harmortization>().Take(defaultCount) //.OrderBy(c => c.AssetDescription).ThenBy(c => c.AssetType)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }


        //public IEnumerable<Harmortization> ExportHarmortization(int defaultCount, string path)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        if (!string.IsNullOrEmpty(path))
        //        {
        //            var query = (from e in entityContext.Set<Harmortization>()
        //                         select new
        //                         {
        //                             e.AssetDescription,
        //                             e.AssetType,
        //                             e.Counterparty,
        //                             e.RatingAgency,
        //                             e.CreditRating,
        //                             e.SandPRating,
        //                             e.PD1,
        //                             e.PD2,
        //                             e.PD3,
        //                             e.PD4,
        //                             e.PD5,
        //                             e.PD6,
        //                             e.PD7,
        //                             e.PD8,
        //                             e.PD9,
        //                             e.PD10,
        //                             e.PD11,
        //                             e.PD12,
        //                             e.PD13
        //                         });

        //            var ExportHandler = new ExcelService();
        //            var response = ExportHandler.Export(query.ToList(), path);

        //            return new List<Harmortization>().Take(0).ToArray();

        //        }
        //        else
        //        {
        //            var query = (from e in entityContext.Set<Harmortization>().Take(defaultCount) //.OrderBy(c => c.AssetDescription).ThenBy(c => c.datepmt)
        //                         select e);

        //            return query.ToArray();
        //        }
        //    }
        //}


        //public IEnumerable<string> GetDistinctAssetType()
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        var query = (entityContext.HarmortizationSet.Select<Harmortization, string>(r => r.AssetType)).Distinct();

        //        return query.ToFullyLoaded();
        //    }
        //}


        //public IEnumerable<Harmortization> GetHarmortizationbyAssetType(string assetType)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        var query = entityContext.HarmortizationSet.AsQueryable().Where(r => r.AssetType == assetType);

        //        return query.ToFullyLoaded();
        //    }
        //}

    }
}