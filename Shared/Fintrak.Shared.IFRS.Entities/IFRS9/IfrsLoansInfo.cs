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
    public partial class IfrsLoansInfo : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int  Id { get; set; }

        [DataMember]
        [Required]
        public string Refno { get; set; }

        [DataMember]
        [Required]
        public string ProductType { get; set; }

        [DataMember]
        public double OutstandingBal { get; set; }

        [DataMember]
        public int Number_Payment_OutStanding { get; set; }


        [DataMember]
        public int DaysPastDue { get; set; }


        [DataMember]
        public DateTime Rundate { get; set; }

       


        //[DataMember]
        //public string CompanyCode { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
