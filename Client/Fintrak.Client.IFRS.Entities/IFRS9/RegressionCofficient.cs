using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class RegressionCofficient : ObjectBase
    {
        int _Id;
        string _ProductType;
        string _ParameterType;
        double _Alpa;
        double _Beta1;
        double _Beta2;
        double _Beta3;
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

        public string ProductType
        {
            get { return _ProductType; }
            set
            {
                if (_ProductType != value)
                {
                    _ProductType = value;
                    OnPropertyChanged(() => ProductType);
                }
            }
        }

        public string ParameterType
        {
            get { return _ParameterType; }
            set
            {
                if (_ParameterType != value)
                {
                    _ParameterType = value;
                    OnPropertyChanged(() => ParameterType);
                }
            }
        }

        public double Alpa
        {
            get { return _Alpa; }
            set
            {
                if (_Alpa != value)
                {
                    _Alpa = value;
                    OnPropertyChanged(() => Alpa);
                }
            }
        }

        public double Beta1
        {
            get { return _Beta1; }
            set
            {
                if (_Beta1 != value)
                {
                    _Beta1 = value;
                    OnPropertyChanged(() => Beta1);
                }
            }
        }



        public double Beta2
        {
            get { return _Beta2; }
            set
            {
                if (_Beta2 != value)
                {
                    _Beta2 = value;
                    OnPropertyChanged(() => Beta2);
                }
            }
        }




        public double Beta3
        {
            get { return _Beta3; }
            set
            {
                if (_Beta3 != value)
                {
                    _Beta3 = value;
                    OnPropertyChanged(() => Beta3);
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


        class RegressionCofficientValidator : AbstractValidator<RegressionCofficient>
        {
            public RegressionCofficientValidator()
            {
                //RuleFor(obj => obj.type).NotEmpty().WithMessage("Type is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new RegressionCofficientValidator();
        }
    }
}
