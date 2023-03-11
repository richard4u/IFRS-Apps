using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Client.IFRS.Contracts
{
    [DataContract]
    public class SectorVariableMappingData : DataContractBase
    {

        [DataMember]
        public int SectorVariableMappingId { get; set; }
        
        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string SectorName { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public string Variable { get; set; }

        [DataMember]
        public string VariableName { get; set; }

        [DataMember]
        public double? Value { get; set; }

        [DataMember]
        public bool Active { get; set; }


    }
}
