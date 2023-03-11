using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Shared.Core.Entities
{
    public class ProcessHistory : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        [Key]
        public int ProcessHistoryId { get; set; }

        [DataMember]
        [Required]
        public string Process { get; set; }

        [DataMember]
        [Required]
        public string ProcessMessage { get; set; }

        [DataMember]
        [Required]
        public string ProcessErrorMessage { get; set; }

        [DataMember]
        [Required]
        public string ProcessStatus { get; set; }


        public int EntityId
        {
            get
            {
                return ProcessHistoryId;
            }
        }
    }
}
