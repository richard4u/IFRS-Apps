﻿using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.Core.Entities
{
    public partial class ProcessTrigger : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        [Key]
        public int ProcessTriggerId { get; set; }

        [DataMember]
        [Required]
        public int ProcessJobId { get; set; }

        [DataMember]
        [Required]
        public int ProcessId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public PackageStatus Status  { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        [Required]
        public string UserName { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public DateTime? RunTime { get; set; }

        public int EntityId
        {
            get
            {
                return ProcessTriggerId;
            }
        }
    }
}




















