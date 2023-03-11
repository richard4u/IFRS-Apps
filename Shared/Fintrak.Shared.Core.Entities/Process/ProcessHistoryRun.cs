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
    public class ProcessHistoryRun : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        [Key]
        public int ProcessHistoryRunId { get; set; }

        [DataMember]
        [Required]
        public string Alias { get; set; }

        [DataMember]
        [Required]
        public string Action { get; set; }


        public int EntityId
        {
            get
            {
                return ProcessHistoryRunId;
            }
        }
    }
}
