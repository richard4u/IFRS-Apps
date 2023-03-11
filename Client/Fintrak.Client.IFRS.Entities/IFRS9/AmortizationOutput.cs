using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class AmortizationOutput : ObjectBase
    {
        int _ID;
        string _Refno;
        DateTime _Date;
        double _EIR;
        double _DailyEir;
        double _NorminalRate;
        double _AmountPrincEnd;
        double _AmortizedCost;
        double _AmortizedCost_OverDue;
        bool _Active;

        public int ID {
            get { return _ID; }
            set {
                if (_ID != value) {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }

        public string Refno {
            get { return _Refno; }
            set {
                if (_Refno != value) {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
                }
            }
        }

        public DateTime Date {
            get { return _Date; }
            set {
                if (_Date != value) {
                    _Date = value;
                    OnPropertyChanged(() => Date);
                }
            }
        }

        public double EIR {
            get { return _EIR; }
            set {
                if (_EIR != value) {
                    _EIR = value;
                    OnPropertyChanged(() => EIR);
                }
            }
        }

        public double DailyEir {
            get { return _DailyEir; }
            set {
                if (_DailyEir != value) {
                    _DailyEir = value;
                    OnPropertyChanged(() => DailyEir);
                }
            }
        }

        public double NorminalRate {
            get { return _NorminalRate; }
            set {
                if (_NorminalRate != value) {
                    _NorminalRate = value;
                    OnPropertyChanged(() => NorminalRate);
                }
            }
        }

        public double AmountPrincEnd {
            get { return _AmountPrincEnd; }
            set {
                if (_AmountPrincEnd != value) {
                    _AmountPrincEnd = value;
                    OnPropertyChanged(() => AmountPrincEnd);
                }
            }
        }

        public double AmortizedCost {
            get { return _AmortizedCost; }
            set {
                if (_AmortizedCost != value) {
                    _AmortizedCost = value;
                    OnPropertyChanged(() => AmortizedCost);
                }
            }
        }

        public double AmortizedCost_OverDue {
            get { return _AmortizedCost_OverDue; }
            set {
                if (_AmortizedCost_OverDue != value) {
                    _AmortizedCost_OverDue = value;
                    OnPropertyChanged(() => AmortizedCost_OverDue);
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


        class AmortizationOutputValidator : AbstractValidator<AmortizationOutput>
        {
            public AmortizationOutputValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new AmortizationOutputValidator();
        }
    }
}
