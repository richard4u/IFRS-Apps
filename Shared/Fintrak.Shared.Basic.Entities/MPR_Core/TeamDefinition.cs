﻿using Fintrak.Shared.Common.Contracts;
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

namespace Fintrak.Shared.Basic.Entities
{
    public partial class TeamDefinition : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int TeamDefinitionId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        [Required]
        public bool CanClassified { get; set; }

        [DataMember]
        public Nullable<int> Period { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }
      
        public int EntityId
        {
            get
            {
                return TeamDefinitionId;
            }
        }
    }
}
