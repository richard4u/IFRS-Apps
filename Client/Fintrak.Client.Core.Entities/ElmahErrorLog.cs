using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class ElmahErrorLog : ObjectBase
    {
        Guid _ErrorId;
        string _Application;
        string _Host;
        string _Type;
        string _Source;
        string _Message;
        string _User;
        int _StatusCode;
        DateTime _TimeUtc;
        int _Sequence;
        string _AllXml;
        string _VisitorsIPAddress;
        string _Manufacturer;
        bool _Active;

        public Guid ErrorId
        {
            get { return _ErrorId; }
            set
            {
                if (_ErrorId != value)
                {
                    _ErrorId = value;
                    OnPropertyChanged(() => ErrorId);
                }
            }
        }

        public string Application
        {
            get { return _Application; }
            set
            {
                if (_Application != value)
                {
                    _Application = value;
                    OnPropertyChanged(() => Application);
                }
            }
        }

        public string Host
        {
            get { return _Host; }
            set
            {
                if (_Host != value)
                {
                    _Host = value;
                    OnPropertyChanged(() => Host);
                }
            }
        }

        public string Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
                }
            }
        }

        public string Source
        {
            get { return _Source; }
            set
            {
                if (_Source != value)
                {
                    _Source = value;
                    OnPropertyChanged(() => Source);
                }
            }
        }

        public string Message
        {
            get { return _Message; }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged(() => Message);
                }
            }
        }

        public string User
        {
            get { return _User; }
            set
            {
                if (_User != value)
                {
                    _User = value;
                    OnPropertyChanged(() => User);
                }
            }
        }

        public int StatusCode
        {
            get { return _StatusCode; }
            set
            {
                if (_StatusCode != value)
                {
                    _StatusCode = value;
                    OnPropertyChanged(() => StatusCode);
                }
            }
        }

        public DateTime TimeUtc
        {
            get { return _TimeUtc; }
            set
            {
                if (_TimeUtc != value)
                {
                    _TimeUtc = value;
                    OnPropertyChanged(() => TimeUtc);
                }
            }
        }

        public int Sequence
        {
            get { return _Sequence; }
            set
            {
                if (_Sequence != value)
                {
                    _Sequence = value;
                    OnPropertyChanged(() => Sequence);
                }
            }
        }

        public string AllXml
        {
            get { return _AllXml; }
            set
            {
                if (_AllXml != value)
                {
                    _AllXml = value;
                    OnPropertyChanged(() => AllXml);
                }
            }
        }

        public string VisitorsIPAddress
        {
            get { return _VisitorsIPAddress; }
            set
            {
                if (_VisitorsIPAddress != value)
                {
                    _VisitorsIPAddress = value;
                    OnPropertyChanged(() => VisitorsIPAddress);
                }
            }
        }

        public string Manufacturer
        {
            get { return _Manufacturer; }
            set
            {
                if (_Manufacturer != value)
                {
                    _Manufacturer = value;
                    OnPropertyChanged(() => Manufacturer);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _Application, _Host );
            }
        }

        class ElmahErrorLogValidator : AbstractValidator<ElmahErrorLog>
        {
            public ElmahErrorLogValidator()
            {
                RuleFor(obj => obj.Application).NotEmpty().WithMessage("Application must not be empty.");
                RuleFor(obj => obj.Host).NotEmpty().WithMessage("Host must not be empty.");
                
            }
        }

        protected override IValidator GetValidator()
        {
            return new ElmahErrorLogValidator();
        }
    }
}
