using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class CashFlowTB : ObjectBase
    {
        int _ID;
        string _Refno;
        string _Component;
        DateTime _Start_date;
        DateTime _Due_Date;
        double _Amount_Due;
        double _amount_settled;
        double _Over_due;
        DateTime _Rundate;
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

        public string Component {
            get { return _Component; }
            set {
                if (_Component != value) {
                    _Component = value;
                    OnPropertyChanged(() => Component);
                }
            }
        }

        public DateTime Start_date {
            get { return _Start_date; }
            set {
                if (_Start_date != value) {
                    _Start_date = value;
                    OnPropertyChanged(() => Start_date);
                }
            }
        }

        public DateTime Due_Date {
            get { return _Due_Date; }
            set {
                if (_Due_Date != value) {
                    _Due_Date = value;
                    OnPropertyChanged(() => Due_Date);
                }
            }
        }

        public double Amount_Due {
            get { return _Amount_Due; }
            set {
                if (_Amount_Due != value) {
                    _Amount_Due = value;
                    OnPropertyChanged(() => Amount_Due);
                }
            }
        }

        public double amount_settled {
            get { return _amount_settled; }
            set {
                if (_amount_settled != value) {
                    _amount_settled = value;
                    OnPropertyChanged(() => amount_settled);
                }
            }
        }

        public double Over_due {
            get { return _Over_due; }
            set {
                if (_Over_due != value) {
                    _Over_due = value;
                    OnPropertyChanged(() => Over_due);
                }
            }
        }

        public DateTime Rundate {
            get { return _Rundate; }
            set {
                if (_Rundate != value) {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
                }
            }
        }

        public bool Active{
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


        class CashFlowTBValidator : AbstractValidator<CashFlowTB>
        {
            public CashFlowTBValidator(){
                //RuleFor(obj => obj.ID).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CashFlowTBValidator();
        }
    }
}
