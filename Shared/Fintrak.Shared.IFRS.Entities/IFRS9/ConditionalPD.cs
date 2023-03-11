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
    public partial class ConditionalPD : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ConditionalPD_Id { get; set; }

        [DataMember]
        public string AssetDescription { get; set; }

        [DataMember]
        public string AssetType { get; set; }

        [DataMember]
        public string Counterparty { get; set; }

        [DataMember]
        public string RatingAgency { get; set; }

        [DataMember]
        public string CreditRating { get; set; }

        [DataMember]
        public string SandPRating { get; set; }

        [DataMember]
        public double PD1 { get; set; }

        [DataMember]
        public double PD2 { get; set; }

        [DataMember]
        public double PD3 { get; set; }

        [DataMember]
        public double PD4 { get; set; }

        [DataMember]
        public double PD5 { get; set; }

        [DataMember]
        public double PD6 { get; set; }

        [DataMember]
        public double PD7 { get; set; }

        [DataMember]
        public double PD8 { get; set; }

        [DataMember]
        public double PD9 { get; set; }

        [DataMember]
        public double PD10 { get; set; }

        [DataMember]
        public double PD11 { get; set; }

        [DataMember]
        public double PD12 { get; set; }

        [DataMember]
        public double PD13 { get; set; }

        public int EntityId
        {
            get
            {
                return ConditionalPD_Id;
            }
        }
    }
}
