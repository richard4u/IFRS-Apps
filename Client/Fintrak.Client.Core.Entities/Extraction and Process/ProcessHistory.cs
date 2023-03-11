using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Core.Entities
{
    public class ProcessHistory : ObjectBase
    {
        int _ProcessHistoryId;
        string _Process;
        string _ProcessMessage;
        string _ProcessErrorMessage;
        string _ProcessStatus;
        bool _Active;

        public int ProcessHistoryId
        {
            get { return _ProcessHistoryId; }
            set
            {
                if (_ProcessHistoryId != value)
                {
                    _ProcessHistoryId = value;
                    OnPropertyChanged(() => ProcessHistoryId);
                }
            }
        }

        public string Process
        {
            get { return _Process; }
            set
            {
                if (_Process != value)
                {
                    _Process = value;
                    OnPropertyChanged(() => Process);
                }
            }
        }

        public String ProcessMessage
        {
            get { return _ProcessMessage; }
            set
            {
                if (_ProcessMessage != value)
                {
                    _ProcessMessage = value;
                    OnPropertyChanged(() => ProcessMessage);
                }
            }
        }

        public string ProcessErrorMessage
        {
            get { return _ProcessErrorMessage; }
            set
            {
                if (_ProcessErrorMessage != value)
                {
                    _ProcessErrorMessage = value;
                    OnPropertyChanged(() => ProcessErrorMessage);
                }
            }
        }

        public string ProcessStatus
        {
            get { return _ProcessStatus; }
            set
            {
                if (_ProcessStatus != value)
                {
                    _ProcessStatus = value;
                    OnPropertyChanged(() => ProcessStatus);
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


        class ProcessHistoryValidator : AbstractValidator<ProcessHistory>
        {
            public ProcessHistoryValidator()
            {
                RuleFor(obj => obj.Process).NotEmpty().WithMessage("Process must not be empty.");
                RuleFor(obj => obj.ProcessStatus).NotEmpty().WithMessage("ProcessStatus must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProcessHistoryValidator();
        }
    }
}
