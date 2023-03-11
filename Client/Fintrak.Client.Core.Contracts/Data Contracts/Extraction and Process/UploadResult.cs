using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Client.Core.Contracts
{
    [DataContract]
    public class UploadResult : DataContractBase
    {
        [DataMember]
        public int UploadId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public string AdditionalInfo { get; set; }

        [DataMember]
        public bool Pass { get; set; }
    }
}
