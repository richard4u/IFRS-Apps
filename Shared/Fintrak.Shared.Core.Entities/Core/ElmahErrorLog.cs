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

namespace Fintrak.Shared.Core.Entities
{
    public partial class ElmahErrorLog : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Required]
        public Guid ErrorId { get; set; }

        [DataMember]
        [Required]
        public string Application { get; set; }

        [DataMember]
        [Required]
        public string Host { get; set; }

        [DataMember]
        [Required]
        public string Type { get; set; }

        [DataMember]
        [Required]
        public string Source { get; set; }

        [DataMember]
        [Required]
        public string Message { get; set; }

        [DataMember]
        [Required]
        public string User { get; set; }

        [DataMember]
        [Required]
        public int StatusCode { get; set; }

        [DataMember]
        [Required]
        public DateTime TimeUtc { get; set; }

        [DataMember]
        [Browsable(false)]
        [Key]
        public int Sequence { get; set; }

        [DataMember]
        [Required]
        public string AllXml { get; set; }

        [DataMember]
        [Required]
        public string VisitorsIPAddress { get; set; }

        [DataMember]
        [Required]
        public string Manufacturer { get; set; }




        public int EntityId
        {
            get
            {
                return Sequence;
            }
        }
    }
}
