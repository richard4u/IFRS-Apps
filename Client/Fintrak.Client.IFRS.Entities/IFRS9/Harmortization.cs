using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class Harmortization : ObjectBase
    {
        int _Id;
        string _Amount;
        string _Vat;
        string _Fee;
        string _TransID;
        string _SrcAcctNo;
        string _SrcInstCode;
        string _SrcInstBranchCode;
        int _SrcInstType;
        string _SrcInstUniqueID;
        string _DestAcctNo;
        string _DestInstCode;
        string _DestInstBranchCode;
        int _DestInstType;
        string _DestInstUniqueID;
        int _PaymentType;
        string _BankIncome;
        DateTime _TransDate;
        string _PsspParty;
        int _AccountType;
        int _AccountClass;
        int _AccountDesignation;
        int _Currency;
        int _Channel;
        string _TransactionTypeCode;
        int _PepDesignatedAccount;
        int _CyberSecurityLevyExempt;
        int _StampdutyExempt;
        int _inflow;
        bool _Active;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }

        public string Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
                }
            }
        }

        public string Vat
        {
            get { return _Vat; }
            set
            {
                if (_Vat != value)
                {
                    _Vat = value;
                    OnPropertyChanged(() => Vat);
                }
            }
        }

        public string Fee
        {
            get { return _Fee; }
            set
            {
                if (_Fee != value)
                {
                    _Fee = value;
                    OnPropertyChanged(() => Fee);
                }
            }
        }

        public string TransID
        {
            get { return _TransID; }
            set
            {
                if (_TransID != value)
                {
                    _TransID = value;
                    OnPropertyChanged(() => TransID);
                }
            }
        }

        public string SrcAcctNo
        {
            get { return _SrcAcctNo; }
            set
            {
                if (_SrcAcctNo != value)
                {
                    _SrcAcctNo = value;
                    OnPropertyChanged(() => SrcAcctNo);
                }
            }
        }

        public string SrcInstCode
        {
            get { return _SrcInstCode; }
            set
            {
                if (_SrcInstCode != value)
                {
                    _SrcInstCode = value;
                    OnPropertyChanged(() => SrcInstCode);
                }
            }
        }

        public string SrcInstBranchCode
        {
            get { return _SrcInstBranchCode; }
            set
            {
                if (_SrcInstBranchCode != value)
                {
                    _SrcInstBranchCode = value;
                    OnPropertyChanged(() => SrcInstBranchCode);
                }
            }
        }

        public int SrcInstType
        {
            get { return _SrcInstType; }
            set
            {
                if (_SrcInstType != value)
                {
                    _SrcInstType = value;
                    OnPropertyChanged(() => SrcInstType);
                }
            }
        }

        public string SrcInstUniqueID
        {
            get { return _SrcInstUniqueID; }
            set
            {
                if (_SrcInstUniqueID != value)
                {
                    _SrcInstUniqueID = value;
                    OnPropertyChanged(() => SrcInstUniqueID);
                }
            }
        }

        public string DestAcctNo
        {
            get { return _DestAcctNo; }
            set
            {
                if (_DestAcctNo != value)
                {
                    _DestAcctNo = value;
                    OnPropertyChanged(() => DestAcctNo);
                }
            }
        }

        public string DestInstCode
        {
            get { return _DestInstCode; }
            set
            {
                if (_DestInstCode != value)
                {
                    _DestInstCode = value;
                    OnPropertyChanged(() => DestInstCode);
                }
            }
        }

        public string DestInstBranchCode
        {
            get { return _DestInstBranchCode; }
            set
            {
                if (_DestInstBranchCode != value)
                {
                    _DestInstBranchCode = value;
                    OnPropertyChanged(() => DestInstBranchCode);
                }
            }
        }

        public int DestInstType
        {
            get { return _DestInstType; }
            set
            {
                if (_DestInstType != value)
                {
                    _DestInstType = value;
                    OnPropertyChanged(() => DestInstType);
                }
            }
        }

        public string DestInstUniqueID
        {
            get { return _DestInstUniqueID; }
            set
            {
                if (_DestInstUniqueID != value)
                {
                    _DestInstUniqueID = value;
                    OnPropertyChanged(() => DestInstUniqueID);
                }
            }
        }

        public int PaymentType
        {
            get { return _PaymentType; }
            set
            {
                if (_PaymentType != value)
                {
                    _PaymentType = value;
                    OnPropertyChanged(() => PaymentType);
                }
            }
        }

        public string BankIncome
        {
            get { return _BankIncome; }
            set
            {
                if (_BankIncome != value)
                {
                    _BankIncome = value;
                    OnPropertyChanged(() => BankIncome);
                }
            }
        }

        public DateTime TransDate
        {
            get { return _TransDate; }
            set
            {
                if (_TransDate != value)
                {
                    _TransDate = value;
                    OnPropertyChanged(() => TransDate);
                }
            }
        }

        public string PsspParty
        {
            get { return _PsspParty; }
            set
            {
                if (_PsspParty != value)
                {
                    _PsspParty = value;
                    OnPropertyChanged(() => PsspParty);
                }
            }
        }

        public int AccountType
        {
            get { return _AccountType; }
            set
            {
                if (_AccountType != value)
                {
                    _AccountType = value;
                    OnPropertyChanged(() => AccountType);
                }
            }
        }

        public int AccountClass
        {
            get { return _AccountClass; }
            set
            {
                if (_AccountClass != value)
                {
                    _AccountClass = value;
                    OnPropertyChanged(() => AccountClass);
                }
            }
        }

        public int AccountDesignation
        {
            get { return _AccountDesignation; }
            set
            {
                if (_AccountDesignation != value)
                {
                    _AccountDesignation = value;
                    OnPropertyChanged(() => AccountDesignation);
                }
            }
        }

        public int Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }

        public int Channel
        {
            get { return _Channel; }
            set
            {
                if (_Channel != value)
                {
                    _Channel = value;
                    OnPropertyChanged(() => Channel);
                }
            }
        }

        public string TransactionTypeCode
        {
            get { return _TransactionTypeCode; }
            set
            {
                if (_TransactionTypeCode != value)
                {
                    _TransactionTypeCode = value;
                    OnPropertyChanged(() => TransactionTypeCode);
                }
            }
        }

        public int PepDesignatedAccount
        {
            get { return _PepDesignatedAccount; }
            set
            {
                if (_PepDesignatedAccount != value)
                {
                    _PepDesignatedAccount = value;
                    OnPropertyChanged(() => PepDesignatedAccount);
                }
            }
        }

        public int CyberSecurityLevyExempt
        {
            get { return _CyberSecurityLevyExempt; }
            set
            {
                if (_CyberSecurityLevyExempt != value)
                {
                    _CyberSecurityLevyExempt = value;
                    OnPropertyChanged(() => CyberSecurityLevyExempt);
                }
            }
        }

        public int StampdutyExempt
        {
            get { return _StampdutyExempt; }
            set
            {
                if (_StampdutyExempt != value)
                {
                    _StampdutyExempt = value;
                    OnPropertyChanged(() => StampdutyExempt);
                }
            }
        }

        public int inflow
        {
            get { return _inflow; }
            set
            {
                if (_inflow != value)
                {
                    _inflow = value;
                    OnPropertyChanged(() => inflow);
                }
            }
        }

        public bool Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }

        class HarmortizationValidator : AbstractValidator<Harmortization>
        {
            public HarmortizationValidator()
            {
                RuleFor(obj => obj.Amount).NotEmpty().WithMessage("Asset Description is required.");
                RuleFor(obj => obj.Fee).NotEmpty().WithMessage("Asset Type is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new HarmortizationValidator();
        }
    }
}
