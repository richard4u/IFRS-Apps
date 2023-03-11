using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class MemoProductMapData : DataContractBase
    {
        [DataMember]
        public int MemoProductMapId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string UnitName { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
