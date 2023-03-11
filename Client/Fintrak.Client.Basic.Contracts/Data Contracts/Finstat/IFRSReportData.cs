using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Client.Basic.Contracts
{
    [DataContract]
    public class IFRSReportData : DataContractBase
    {
        [DataMember]
        public int IFRSReportId { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string GLCode  { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public DateTime ReportDate { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
