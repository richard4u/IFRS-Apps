﻿using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Client.IFRS.Entities
{
    public class GLAdjustment : ObjectBase
    {
        int _GLAdjustmentId;
        string _GLCode;
        string _Narration;
        Indicator _Indicator;
        AdjustmentType _AdjustmentType;
        ReportType _ReportType;
        decimal _Amount;
        decimal _CCY_Amount;
        decimal _ExchangeRate;
        string _CompanyCode;
        string _GroupCode;
        string _Currency;
        int _AdjustmentFlag;
        DateTime _RunDate;
        bool _Posted;
        bool _Active;

        public int GLAdjustmentId
        {
            get { return _GLAdjustmentId; }
            set
            {
                if (_GLAdjustmentId != value)
                {
                    _GLAdjustmentId = value;
                    OnPropertyChanged(() => GLAdjustmentId);
                }
            }
        }

        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
                }
            }
        }

        public string GroupCode
        {
            get { return _GroupCode; }
            set
            {
                if (_GroupCode != value)
                {
                    _GroupCode = value;
                    OnPropertyChanged(() => GroupCode);
                }
            }
        }

        public string Narration
        {
            get { return _Narration; }
            set
            {
                if (_Narration != value)
                {
                    _Narration = value;
                    OnPropertyChanged(() => Narration);
                }
            }
        }

        public Indicator Indicator
        {
            get { return _Indicator; }
            set
            {
                if (_Indicator != value)
                {
                    _Indicator = value;
                    OnPropertyChanged(() => Indicator);
                }
            }
        }

        public AdjustmentType AdjustmentType
        {
            get { return _AdjustmentType; }
            set
            {
                if (_AdjustmentType != value)
                {
                    _AdjustmentType = value;
                    OnPropertyChanged(() => AdjustmentType);
                }
            }
        }


        public ReportType ReportType
        {
            get { return _ReportType; }
            set
            {
                if (_ReportType != value)
                {
                    _ReportType = value;
                    OnPropertyChanged(() => ReportType);
                }
            }
        }

        public int AdjustmentFlag
        {
            get { return _AdjustmentFlag; }
            set
            {
                if (_AdjustmentFlag != value)
                {
                    _AdjustmentFlag = value;
                    OnPropertyChanged(() => AdjustmentFlag);
                }
            }
        }

        public decimal Amount
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
        public decimal ExchangeRate
        {
            get { return _ExchangeRate; }
            set
            {
                if (_ExchangeRate != value)
                {
                    _ExchangeRate = value;
                    OnPropertyChanged(() => ExchangeRate);
                }
            }
        }

        public decimal CCY_Amount
        {
            get { return _CCY_Amount; }
            set
            {
                if (_CCY_Amount != value)
                {
                    _CCY_Amount = value;
                    OnPropertyChanged(() => CCY_Amount);
                }
            }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }

        public string Currency
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

        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
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

        public bool Posted
        {
            get { return _Posted; }
            set
            {
                if (_Posted != value)
                {
                    _Posted = value;
                    OnPropertyChanged(() => Posted);
                }
            }
        }

        
        class GLAdjustmentValidator : AbstractValidator<GLAdjustment>
        {
            public GLAdjustmentValidator()
            {
                RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GLCode is required.");
                RuleFor(obj => obj.Narration).NotEmpty().WithMessage("Narration is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new GLAdjustmentValidator();
        }
    }
}
