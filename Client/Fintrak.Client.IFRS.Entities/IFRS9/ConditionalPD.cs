using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ConditionalPD : ObjectBase
    {
        int _ConditionalPD_Id;
        string _AssetDescription;
        string _AssetType;
        string _Counterparty;
        string _RatingAgency;
        string _CreditRating;
        string _SandPRating;
        double _PD1;
        double _PD2;
        double _PD3;
        double _PD4;
        double _PD5;
        double _PD6;
        double _PD7;
        double _PD8;
        double _PD9;
        double _PD10;
        double _PD11;
        double _PD12;
        double _PD13;
        bool _Active;

        public int ConditionalPD_Id
        {
            get { return _ConditionalPD_Id; }
            set
            {
                if (_ConditionalPD_Id != value)
                {
                    _ConditionalPD_Id = value;
                    OnPropertyChanged(() => ConditionalPD_Id);
                }
            }
        }

        public string AssetDescription
        {
            get { return _AssetDescription; }
            set
            {
                if (_AssetDescription != value)
                {
                    _AssetDescription = value;
                    OnPropertyChanged(() => AssetDescription);
                }
            }
        }

        public string AssetType
        {
            get { return _AssetType; }
            set
            {
                if (_AssetType != value)
                {
                    _AssetType = value;
                    OnPropertyChanged(() => AssetType);
                }
            }
        }

        public string Counterparty
        {
            get { return _Counterparty; }
            set
            {
                if (_Counterparty != value)
                {
                    _Counterparty = value;
                    OnPropertyChanged(() => Counterparty);
                }
            }
        }

        public string RatingAgency
        {
            get { return _RatingAgency; }
            set
            {
                if (_RatingAgency != value)
                {
                    _RatingAgency = value;
                    OnPropertyChanged(() => RatingAgency);
                }
            }
        }

        public string CreditRating
        {
            get { return _CreditRating; }
            set
            {
                if (_CreditRating != value)
                {
                    _CreditRating = value;
                    OnPropertyChanged(() => CreditRating);
                }
            }
        }

        public string SandPRating
        {
            get { return _SandPRating; }
            set
            {
                if (_SandPRating != value)
                {
                    _SandPRating = value;
                    OnPropertyChanged(() => SandPRating);
                }
            }
        }

        public double PD1
        {
            get { return _PD1; }
            set
            {
                if (_PD1 != value)
                {
                    _PD1 = value;
                    OnPropertyChanged(() => PD1);
                }
            }
        }

        public double PD2
        {
            get { return _PD2; }
            set
            {
                if (_PD2 != value)
                {
                    _PD2 = value;
                    OnPropertyChanged(() => PD2);
                }
            }
        }

        public double PD3
        {
            get { return _PD3; }
            set
            {
                if (_PD3 != value)
                {
                    _PD3 = value;
                    OnPropertyChanged(() => PD3);
                }
            }
        }

        public double PD4
        {
            get { return _PD4; }
            set
            {
                if (_PD4 != value)
                {
                    _PD4 = value;
                    OnPropertyChanged(() => PD4);
                }
            }
        }

        public double PD5
        {
            get { return _PD5; }
            set
            {
                if (_PD5 != value)
                {
                    _PD5 = value;
                    OnPropertyChanged(() => PD5);
                }
            }
        }

        public double PD6
        {
            get { return _PD6; }
            set
            {
                if (_PD6 != value)
                {
                    _PD6 = value;
                    OnPropertyChanged(() => PD6);
                }
            }
        }

        public double PD7
        {
            get { return _PD7; }
            set
            {
                if (_PD7 != value)
                {
                    _PD7 = value;
                    OnPropertyChanged(() => PD7);
                }
            }
        }

        public double PD8
        {
            get { return _PD8; }
            set
            {
                if (_PD8 != value)
                {
                    _PD8 = value;
                    OnPropertyChanged(() => PD8);
                }
            }
        }

        public double PD9
        {
            get { return _PD9; }
            set
            {
                if (_PD9 != value)
                {
                    _PD9 = value;
                    OnPropertyChanged(() => PD9);
                }
            }
        }

        public double PD10
        {
            get { return _PD10; }
            set
            {
                if (_PD10 != value)
                {
                    _PD10 = value;
                    OnPropertyChanged(() => PD10);
                }
            }
        }

        public double PD11
        {
            get { return _PD11; }
            set
            {
                if (_PD11 != value)
                {
                    _PD11 = value;
                    OnPropertyChanged(() => PD11);
                }
            }
        }

        public double PD12
        {
            get { return _PD12; }
            set
            {
                if (_PD12 != value)
                {
                    _PD12 = value;
                    OnPropertyChanged(() => PD12);
                }
            }
        }

        public double PD13
        {
            get { return _PD13; }
            set
            {
                if (_PD13 != value)
                {
                    _PD13 = value;
                    OnPropertyChanged(() => PD13);
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


        class ConditionalPDValidator : AbstractValidator<ConditionalPD>
        {
            public ConditionalPDValidator()
            {
                RuleFor(obj => obj.AssetDescription).NotEmpty().WithMessage("Asset Description is required.");
                RuleFor(obj => obj.AssetType).NotEmpty().WithMessage("Asset Type is required.");

            }
        }
        
        protected override IValidator GetValidator()
        {
            return new ConditionalPDValidator();
        }
    }
}
