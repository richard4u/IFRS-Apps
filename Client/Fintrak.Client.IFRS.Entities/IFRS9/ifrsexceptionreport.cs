using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ifrsexceptionreport : ObjectBase
    {

        int _Id;
        string _RefNo;
        double _NorminalRate;
        double _EIR;
        string _Classification;
        string _ExceptionType;
        DateTime _RunDate;
       
        bool _Active;

        public int Id {
            get { return _Id; }
            set {
                if (_Id != value) {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }

        public string RefNo
        {
            get { return _RefNo; }
            set {
                if (_RefNo != value) {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
                }
            }
        }

        public double NorminalRate
        {
            get { return _NorminalRate; }
            set {
                if (_NorminalRate != value) {
                    _NorminalRate = value;
                    OnPropertyChanged(() => NorminalRate);
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


        public string Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }



        public string ExceptionType
        {
            get { return _ExceptionType; }
            set
            {
                if (_ExceptionType != value)
                {
                    _ExceptionType = value;
                    OnPropertyChanged(() => ExceptionType);
                }
            }
        }


        public DateTime RunDate
        {
            get { return _RunDate; }
            set {
                if (_RunDate != value) {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
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
        class ifrsexceptionreportValidator : AbstractValidator<ifrsexceptionreport>
        {
            public ifrsexceptionreportValidator(){               
            }
        }

        protected override IValidator GetValidator()
        {
            return new ifrsexceptionreportValidator();
        }

    }
}
