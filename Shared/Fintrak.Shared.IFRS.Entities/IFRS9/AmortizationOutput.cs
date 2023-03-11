using Fintrak.Shared.IFRS.Framework;
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
    public partial class AmortizationOutput : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)] public int ID { get; set; }
        [DataMember] public string Refno { get; set; }
        [DataMember] public DateTime Date { get; set; }
        [DataMember] public double EIR { get; set; }
        [DataMember] public double DailyEir { get; set; }
        [DataMember] public double NorminalRate { get; set; }
        [DataMember] public double AmountPrincEnd { get; set; }
        [DataMember] public double AmortizedCost { get; set; }
        [DataMember] public double AmortizedCost_OverDue { get; set; }

        public int EntityId{
            get{
                return ID;
            }
        }
    }
}
