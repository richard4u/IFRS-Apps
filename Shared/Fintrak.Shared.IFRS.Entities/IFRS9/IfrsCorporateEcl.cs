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
    public partial class IfrsCorporateEcl : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ecl_id { get; set; }

        [DataMember]
        public string refno { get; set; }

        [DataMember]
        public int period { get; set; }         

        [DataMember]
        public double eclbest { get; set; }  

        [DataMember]
        public double ecloptimisitc { get; set; }  

        [DataMember]
        public double ecldownturn { get; set; }

        [DataMember]
        public double unsecured_exposure { get; set; }

        [DataMember]
        public double prob_wighted_opt { get; set; }   

        [DataMember]
        public double probwighted_best { get; set; }  

        [DataMember]
        public double probwighted_down { get; set; }  

        [DataMember]
        public double lgdbest { get; set; }

        [DataMember]
        public double lgdoptimistic { get; set; }

        [DataMember]
        public double lgdDown { get; set; } 

        [DataMember]
        public double lgd_macro_best { get; set; }  

        [DataMember]
        public double lgd_macro_optim { get; set; }  

        [DataMember]
        public double lgd_macro_down { get; set; }

        [DataMember]
        public double pdbest { get; set; }

        [DataMember]
        public double pdoptimistic { get; set; } 

        [DataMember]
        public double pd_down { get; set; }  

        [DataMember]
        public double interestfactor { get; set; }  

        [DataMember]
        public double interest_rate { get; set; }

        [DataMember]
        public double eir { get; set; }

        [DataMember]
        public double discount_factor { get; set; }

        [DataMember]
        public string rating { get; set; }

        [DataMember]
        public string staging { get; set; }

        [DataMember]
        public double Exposure_net_impairment { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ecl_id;
            }
        }
    }
}