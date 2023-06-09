﻿using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class IfrsSectorCCF : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int SectorId { get; set; }

        [DataMember]
        public string code { get; set; }

        [DataMember]
        public string sector { get; set; }

        [DataMember]
        public double? ccf { get; set; }

        [DataMember]
        public double? lgdDownturn { get; set; }

        [DataMember]
        public double? lgdOptimistic { get; set; }

        [DataMember]
        public double? lgdBest { get; set; }

        [DataMember]
        public double? Pd { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return SectorId;
            }
        }
    }
}