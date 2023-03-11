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
    public partial class Harmortization : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        public string Amount { get; set; }

        [DataMember]
        public string Vat { get; set; }

        [DataMember]
        public string Fee { get; set; }

        [DataMember]
        public string TransID { get; set; }

        [DataMember]
        public string SrcAcctNo { get; set; }

        [DataMember]
        public string SrcInstCode { get; set; }

        [DataMember]
        public string SrcInstBranchCode { get; set; }

        [DataMember]
        public int SrcInstType { get; set; }

        [DataMember]
        public string SrcInstUniqueID { get; set; }

        [DataMember]
        public string DestAcctNo { get; set; }

        [DataMember]
        public string DestInstCode { get; set; }

        [DataMember]
        public string DestInstBranchCode { get; set; }

        [DataMember]
        public int DestInstType { get; set; }

        [DataMember]
        public string DestInstUniqueID { get; set; }

        [DataMember]
        public int PaymentType { get; set; }

        [DataMember]
        public string BankIncome { get; set; }

        [DataMember]
        public DateTime TransDate { get; set; }

        [DataMember]
        public string PsspParty { get; set; }

        [DataMember]
        public int AccountType { get; set; }

        [DataMember]
        public int AccountClass { get; set; }

        [DataMember]
        public int AccountDesignation { get; set; }

        [DataMember]
        public int Currency { get; set; }

        [DataMember]
        public int Channel { get; set; }

        [DataMember]
        public string TransactionTypeCode { get; set; }

        [DataMember]
        public int PepDesignatedAccount { get; set; }

        [DataMember]
        public int CyberSecurityLevyExempt { get; set; }

        [DataMember]
        public int StampdutyExempt { get; set; }

        [DataMember]
        public int inflow { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}