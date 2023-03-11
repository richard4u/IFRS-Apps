using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMPRBalanceSheetRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRBalanceSheetRepository : DataRepositoryBase<MPRBalanceSheet>, IMPRBalanceSheetRepository
    {

        protected override MPRBalanceSheet AddEntity(MPRContext entityContext, MPRBalanceSheet entity)
        {
            return entityContext.Set<MPRBalanceSheet>().Add(entity);
        }

        protected override MPRBalanceSheet UpdateEntity(MPRContext entityContext, MPRBalanceSheet entity)
        {
            return (from e in entityContext.Set<MPRBalanceSheet>()
                    where e.BalanceSheetId == entity.BalanceSheetId
                    select e).FirstOrDefault();
        }


        protected override IEnumerable<MPRBalanceSheet> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MPRBalanceSheet>()
                   select e;

            //var query = (from e in entityContext.Set<MPRBalanceSheet>()
            //             select e).Take(1000);
            //var results = query;
            //return results;
        }

        public IEnumerable<MPRBalanceSheet> GetRunDate()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = MPRContext.GetDataConnection();

            var balSheets = new List<MPRBalanceSheet>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_getDistinctbalancesheetRundate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var balSheet = new MPRBalanceSheet();

                    if (reader["RunDate"] != DBNull.Value)
                        balSheet.Rundate = DateTime.Parse(reader["RunDate"].ToString());

                    balSheets.Add(balSheet);
                }

                con.Close();
            }

            return balSheets;
        }


        protected override MPRBalanceSheet GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRBalanceSheet>()
                         where e.BalanceSheetId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MPRBalanceSheet> GetBalanceSheets(DateTime runDate, int number)
        {
            //using (MPRContext entityContext = new MPRContext())
            //{
            //    var query = from a in entityContext.MPRBalanceSheetSet
            //                where a.Rundate == runDate.Date 
            //                select a;

            //    if (number == 0)
            //        return query.ToFullyLoaded();

            //    return query.ToFullyLoaded().Take(number);
            //}

            return GetBalanceSheetBySearch("All", "All", number);
        }

        public IEnumerable<MPRBalanceSheet> GetAllBalanceSheets(string searchType, string searchValue, int number, DateTime fromDate)
        {
            //using (MPRContext entityContext = new MPRContext())
            //{
            //    var query = from a in entityContext.MPRBalanceSheetSet
            //                where a.Rundate == runDate.Date 
            //                select a;

            //    if (number == 0)
            //        return query.ToFullyLoaded();

            //    return query.ToFullyLoaded().Take(number);
            //}

            return GetAllBalanceSheetBySearch(searchType, searchValue, number, fromDate);
        }


        public List<MPRBalanceSheet> GetAllBalanceSheetBySearch(string searchType, string searchValue, int number, DateTime fromDate)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = MPRContext.GetDataConnection();

            var balSheets = new List<MPRBalanceSheet>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_balancesheetAll", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SearchType",
                    Value = searchType,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SearchValue",
                    Value = searchValue,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Number",
                    Value = number,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "FromDate",
                    Value = fromDate,
                });

                //cmd.Parameters.Add(new SqlParameter
                //{
                //    ParameterName = "ToDate",
                //    Value = toDate,
                //});

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var balSheet = new MPRBalanceSheet();

                    if (reader["BalancesheetId"] != DBNull.Value)
                        balSheet.BalanceSheetId = int.Parse(reader["BalancesheetId"].ToString());

                    if (reader["AccountNo"] != DBNull.Value)
                        balSheet.AccountNo = reader["AccountNo"].ToString();

                    if (reader["AccountName"] != DBNull.Value)
                        balSheet.AccountName = reader["AccountName"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        balSheet.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        balSheet.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["CaptionName"] != DBNull.Value)
                        balSheet.CaptionName = reader["CaptionName"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        balSheet.BranchCode = reader["BranchCode"].ToString();

                    if (reader["ProductCode"] != DBNull.Value)
                        balSheet.ProductCode = reader["ProductCode"].ToString();

                    if (reader["Category"] != DBNull.Value)
                        balSheet.Category = reader["Category"].ToString();

                    if (reader["CurrencyType"] != DBNull.Value)
                        balSheet.CurrencyType = reader["CurrencyType"].ToString();

                    if (reader["Currency"] != DBNull.Value)
                        balSheet.Currency = reader["Currency"].ToString();

                    if (reader["ActualBal"] != DBNull.Value)
                        balSheet.ActualBal = decimal.Parse(reader["ActualBal"].ToString());

                    if (reader["AverageBal"] != DBNull.Value)
                        balSheet.AverageBal = decimal.Parse(reader["AverageBal"].ToString());

                    if (reader["Interest"] != DBNull.Value)
                        balSheet.Interest = decimal.Parse(reader["Interest"].ToString());

                    if (reader["EffIntRate"] != DBNull.Value)
                        balSheet.EffIntRate = double.Parse(reader["EffIntRate"].ToString());

                    if (reader["Pool"] != DBNull.Value)
                        balSheet.Pool = decimal.Parse(reader["Pool"].ToString());

                    if (reader["PoolRate"] != DBNull.Value)
                        balSheet.PoolRate = double.Parse(reader["PoolRate"].ToString());

                    if (reader["ContractRate"] != DBNull.Value)
                        balSheet.ContractRate = double.Parse(reader["ContractRate"].ToString());

                    if (reader["VolumeGL"] != DBNull.Value)
                        balSheet.VolumeGL = reader["VolumeGL"].ToString();

                    if (reader["InterestGL"] != DBNull.Value)
                        balSheet.InterestGL = reader["InterestGL"].ToString();

                    if (reader["EntryStatus"] != DBNull.Value)
                        balSheet.EntryStatus = reader["EntryStatus"].ToString();

                    if (reader["MaxRate"] != DBNull.Value)
                        balSheet.MaxRate = double.Parse(reader["MaxRate"].ToString());

                    if (reader["PenalCharge"] != DBNull.Value)
                        balSheet.PenalCharge = decimal.Parse(reader["PenalCharge"].ToString());

                    if (reader["PenalRate"] != DBNull.Value)
                        balSheet.PenalRate = double.Parse(reader["PenalRate"].ToString());

                    if (reader["AcctStatus"] != DBNull.Value)
                        balSheet.AcctStatus = reader["AcctStatus"].ToString();

                    if (reader["CreditRating"] != DBNull.Value)
                        balSheet.CreditRating = reader["CreditRating"].ToString();

                    if (reader["CompanyCode"] != DBNull.Value)
                        balSheet.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["RunDate"] != DBNull.Value)
                        balSheet.Rundate = DateTime.Parse(reader["RunDate"].ToString());

                    balSheets.Add(balSheet);
                }

                con.Close();
            }

            return balSheets;
        }

        public List<MPRBalanceSheet> GetBalanceSheetBySearch(string searchType, string searchValue, int number)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = MPRContext.GetDataConnection();

            var balSheets = new List<MPRBalanceSheet>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_balancesheet", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SearchType",
                    Value = searchType,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SearchValue",
                    Value = searchValue,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Number",
                    Value = number,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var balSheet = new MPRBalanceSheet();

                    if (reader["BalancesheetId"] != DBNull.Value)
                        balSheet.BalanceSheetId = int.Parse(reader["BalancesheetId"].ToString());

                    if (reader["AccountNo"] != DBNull.Value)
                        balSheet.AccountNo = reader["AccountNo"].ToString();

                    if (reader["AccountName"] != DBNull.Value)
                        balSheet.AccountName = reader["AccountName"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        balSheet.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        balSheet.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["CaptionName"] != DBNull.Value)
                        balSheet.CaptionName = reader["CaptionName"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        balSheet.BranchCode = reader["BranchCode"].ToString();

                    if (reader["ProductCode"] != DBNull.Value)
                        balSheet.ProductCode = reader["ProductCode"].ToString();

                    if (reader["Category"] != DBNull.Value)
                        balSheet.Category = reader["Category"].ToString();

                    if (reader["CurrencyType"] != DBNull.Value)
                        balSheet.CurrencyType = reader["CurrencyType"].ToString();

                    if (reader["Currency"] != DBNull.Value)
                        balSheet.Currency = reader["Currency"].ToString();

                    if (reader["ActualBal"] != DBNull.Value)
                        balSheet.ActualBal = decimal.Parse(reader["ActualBal"].ToString());

                    if (reader["AverageBal"] != DBNull.Value)
                        balSheet.AverageBal = decimal.Parse(reader["AverageBal"].ToString());

                    if (reader["Interest"] != DBNull.Value)
                        balSheet.Interest = decimal.Parse(reader["Interest"].ToString());

                    if (reader["EffIntRate"] != DBNull.Value)
                        balSheet.EffIntRate = double.Parse(reader["EffIntRate"].ToString());

                    if (reader["Pool"] != DBNull.Value)
                        balSheet.Pool = decimal.Parse(reader["Pool"].ToString());

                    if (reader["PoolRate"] != DBNull.Value)
                        balSheet.PoolRate = double.Parse(reader["PoolRate"].ToString());

                    if (reader["ContractRate"] != DBNull.Value)
                        balSheet.ContractRate = double.Parse(reader["ContractRate"].ToString());

                    if (reader["VolumeGL"] != DBNull.Value)
                        balSheet.VolumeGL = reader["VolumeGL"].ToString();

                    if (reader["InterestGL"] != DBNull.Value)
                        balSheet.InterestGL = reader["InterestGL"].ToString();

                    if (reader["EntryStatus"] != DBNull.Value)
                        balSheet.EntryStatus = reader["EntryStatus"].ToString();

                    if (reader["MaxRate"] != DBNull.Value)
                        balSheet.MaxRate = double.Parse(reader["MaxRate"].ToString());

                    if (reader["PenalCharge"] != DBNull.Value)
                        balSheet.PenalCharge = decimal.Parse(reader["PenalCharge"].ToString());

                    if (reader["PenalRate"] != DBNull.Value)
                        balSheet.PenalRate = double.Parse(reader["PenalRate"].ToString());

                    if (reader["AcctStatus"] != DBNull.Value)
                        balSheet.AcctStatus = reader["AcctStatus"].ToString();

                    if (reader["CreditRating"] != DBNull.Value)
                        balSheet.CreditRating = reader["CreditRating"].ToString();

                    if (reader["CompanyCode"] != DBNull.Value)
                        balSheet.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["RunDate"] != DBNull.Value)
                        balSheet.Rundate = DateTime.Parse(reader["RunDate"].ToString());

                    balSheets.Add(balSheet);
                }

                con.Close();
            }

            return balSheets;
        }
      
    }
}
