﻿using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class ProductGroupEntry : TransactionEntityBase<decimal>, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ProductGroupEntryId { get; set; }

        [DataMember]
        [Required]
        public string DefintionCode { get; set; }

        [DataMember]
        [Required]
        public string MisCode { get; set; }

        [DataMember]
        [Required]
        public string GroupCode { get; set; }

        [DataMember]
        [Required]
        public string CurrencyCode { get; set; }

        [DataMember]
        [Required]
        public string ReviewCode { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        public int EntityId
        {
            get
            {
                return ProductGroupEntryId;
            }
        }
    }
}
