using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SegmentPerformance : ObjectBase
    {
        int _SegmentId;
        string _SegmentCode;
        string _SegmentName;
        int _Period;
        int _Non_Performing_Count;
        int _Previously_Performing_Count;      
        double? _Estimate { get; set; }
        string _Param1;
        string _Param2;
        int _Param3;
        int _Param4;
        DateTime? _Param5;
        DateTime? _Param6;
        bool _Active;

        public int SegmentId
        {
            get { return _SegmentId; }
            set
            {
                if (_SegmentId != value)
                {
                    _SegmentId = value;
                    OnPropertyChanged(() => SegmentId);
                }
            }
        }

        public string Param1
        {
            get { return _Param1; }
            set
            {
                if (_Param1 != value)
                {
                    _Param1 = value;
                    OnPropertyChanged(() => Param1);
                }
            }
        }


        public string Param2
        {
            get { return _Param2; }
            set
            {
                if (_Param2 != value)
                {
                    _Param2 = value;
                    OnPropertyChanged(() => Param2);
                }
            }
        }


        public int Param3
        {
            get { return _Param3; }
            set
            {
                if (_Param3 != value)
                {
                    _Param3 = value;
                    OnPropertyChanged(() => Param3);
                }
            }
        }

        public int Param4
        {
            get { return _Param4; }
            set
            {
                if (_Param4 != value)
                {
                    _Param4 = value;
                    OnPropertyChanged(() => Param4);
                }
            }
        }

        public DateTime? Param5
        {
            get { return _Param5; }
            set
            {
                if (_Param5 != value)
                {
                    _Param5 = value;
                    OnPropertyChanged(() => Param5);
                }
            }
        }

        public DateTime? Param6
        {
            get { return _Param5; }
            set
            {
                if (_Param5 != value)
                {
                    _Param5 = value;
                    OnPropertyChanged(() => Param5);
                }
            }
        }

        public string SegmentCode
        {
            get { return _SegmentCode; }
            set
            {
                if (_SegmentCode != value)
                {
                    _SegmentCode = value;
                    OnPropertyChanged(() => SegmentCode);
                }
            }
        }


        public string SegmentName
        {
            get { return _SegmentName; }
            set
            {
                if (_SegmentName != value)
                {
                    _SegmentName = value;
                    OnPropertyChanged(() => SegmentName);
                }
            }
        }

        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }

        public int Non_Performing_Count
        {
            get { return _Non_Performing_Count; }
            set
            {
                if (_Non_Performing_Count != value)
                {
                    _Non_Performing_Count = value;
                    OnPropertyChanged(() => Non_Performing_Count);
                }
            }
        }

        public int Previously_Performing_Count
        {
            get { return _Previously_Performing_Count; }
            set
            {
                if (_Previously_Performing_Count != value)
                {
                    _Previously_Performing_Count = value;
                    OnPropertyChanged(() => Previously_Performing_Count);
                }
            }
        }

        public double? Estimate
        {
            get { return _Estimate; }
            set
            {
                if (_Estimate != value)
                {
                    _Estimate = value;
                    OnPropertyChanged(() => Estimate);
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


        class SegmentPerformanceValidator : AbstractValidator<SegmentPerformance>
        {
            public SegmentPerformanceValidator()
            {
                RuleFor(obj => obj.SegmentCode).NotEmpty().WithMessage("SectorCode is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new SegmentPerformanceValidator();
        }
    }
}
