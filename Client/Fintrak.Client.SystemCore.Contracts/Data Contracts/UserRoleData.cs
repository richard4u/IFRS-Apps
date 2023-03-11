using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.SystemCore.Framework;


namespace Fintrak.Client.SystemCore.Contracts
{
    [DataContract]
    public class UserRoleData : DataContractBase
    {
        [DataMember]
        public int UserRoleId { get; set; }

        [DataMember]
        public int UserSetupId { get; set; }

        [DataMember]
        public string LoginID { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public RoleType Type { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
