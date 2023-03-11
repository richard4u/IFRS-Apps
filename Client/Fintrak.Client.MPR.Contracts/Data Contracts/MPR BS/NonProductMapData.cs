using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;


namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class NonProductMapData : DataContractBase
    {
        [DataMember]
        public int NonProductMapId { get; set; }

        [DataMember]
        public string NonProductCode { get; set; }

        [DataMember]
        public string NonProductName { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public BalanceSheetCategory Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public CurrencyType CurrencyType { get; set; }

        [DataMember]
        public string CurrencyTypeName { get; set; }

       [DataMember]
        public bool Active { get; set; }
    }
}
