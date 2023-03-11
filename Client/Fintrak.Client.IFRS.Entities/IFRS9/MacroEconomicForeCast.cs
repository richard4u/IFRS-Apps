using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacroEconomicForeCast: ObjectBase
    {

        int _ID;
        DateTime _Period;
        string _Coefficient;
        double _Baseline_InflationRate;
        double _Baseline_ExchangeRate;
        double _Best_InflationRate;
        double _Best_ExchangeRate;
        double _Worst_InflationRate;
        double _Worst_ExchangeRate;
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


        public DateTime Period{
            get { return _Period; }
            set {
                if (_Period!= value) {
                    _Period= value;
                    OnPropertyChanged(() => Period);
                }
            }
        }


        public string Coefficient
        {
            get { return _Coefficient; }
            set {
                if (_Coefficient != value) {
                    _Coefficient = value;
                    OnPropertyChanged(() => Coefficient);
                }
            }
        }

        public double Baseline_InflationRate
        {
            get { return _Baseline_InflationRate; }
            set {
                if (_Baseline_InflationRate != value) {
                    _Baseline_InflationRate = value;
                    OnPropertyChanged(() => Baseline_InflationRate);
                }
            }
        }


        public double Baseline_ExchangeRate
        {
            get { return _Baseline_ExchangeRate; }
            set {
                if (_Baseline_ExchangeRate != value) {
                    _Baseline_ExchangeRate = value;
                    OnPropertyChanged(() => Baseline_ExchangeRate);
                }
            }
        }


        public double Best_InflationRate
        {
            get { return _Best_InflationRate; }
            set {
                if (_Best_InflationRate != value) {
                    _Best_InflationRate = value;
                    OnPropertyChanged(() => Best_InflationRate);
                }
            }
        }


        public double Best_ExchangeRate
        {
            get { return _Best_ExchangeRate; }
            set {
                if (_Best_ExchangeRate != value) {
                    _Best_ExchangeRate = value;
                    OnPropertyChanged(() => Best_ExchangeRate);
                }
            }
        }

        public double Worst_InflationRate
        {
            get { return _Worst_InflationRate; }
            set {
                if (_Worst_InflationRate != value) {
                    _Worst_InflationRate = value;
                    OnPropertyChanged(() => Worst_InflationRate);
                }
            }
        }

        public double Worst_ExchangeRate
        {
            get { return _Worst_ExchangeRate; }
            set
            {
                if (_Worst_ExchangeRate != value)
                {
                    _Worst_ExchangeRate = value;
                    OnPropertyChanged(() => Worst_ExchangeRate);
                }
            }
        }




        public bool Active {
            get { return _Active; }
            set {
                if (_Active != value) {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }
        class MacroEconomicForeCastValidator : AbstractValidator<MacroEconomicForeCast>
        {
            public MacroEconomicForeCastValidator(){               
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacroEconomicForeCastValidator();
        }

    }
}
