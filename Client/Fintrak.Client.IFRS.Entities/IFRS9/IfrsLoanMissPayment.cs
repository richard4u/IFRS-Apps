using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsLoanMissPayment : ObjectBase
    {

        int _ID;
        string  _DefaultType;
        int    _DefaultParam;
        int    _DaysPastDue;


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


        public string DefaultType
        {
            get { return _DefaultType; }
            set {
                if (_DefaultType != value) {
                    _DefaultType = value;
                    OnPropertyChanged(() => DefaultType);
                }
            }
        }


        public int DefaultParam
        {
            get { return _DefaultParam; }
            set {
                if (_DefaultParam != value) {
                    _DefaultParam = value;
                    OnPropertyChanged(() => DefaultParam);
                }
            }
        }

        public int DaysPastDue
        {
            get { return _DaysPastDue; }
            set {
                if (_DaysPastDue != value) {
                    _DaysPastDue = value;
                    OnPropertyChanged(() => DaysPastDue);
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
        class IfrsLoanMissPaymentValidator : AbstractValidator<IfrsLoanMissPayment>
        {
            public IfrsLoanMissPaymentValidator(){               
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsLoanMissPaymentValidator();
        }

    }
}
