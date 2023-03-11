using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Core.Entities
{
    public class ProcessHistoryRun : ObjectBase
    {
        int _ProcessHistoryRunId;
        string _Alias;
        string _Action;
        bool _Active;

        public int ProcessHistoryRunId
        {
            get { return _ProcessHistoryRunId; }
            set
            {
                if (_ProcessHistoryRunId != value)
                {
                    _ProcessHistoryRunId = value;
                    OnPropertyChanged(() => ProcessHistoryRunId);
                }
            }
        }

        public string Alias
        {
            get { return _Alias; }
            set
            {
                if (_Alias != value)
                {
                    _Alias = value;
                    OnPropertyChanged(() => Alias);
                }
            }
        }

        public String Action
        {
            get { return _Action; }
            set
            {
                if (_Action != value)
                {
                    _Action = value;
                    OnPropertyChanged(() => Action);
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


        class ProcessHistoryRunValidator : AbstractValidator<ProcessHistoryRun>
        {
            public ProcessHistoryRunValidator()
            {
                RuleFor(obj => obj.Alias).NotEmpty().WithMessage("Alias must not be empty.");
                RuleFor(obj => obj.Action).NotEmpty().WithMessage("Action must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProcessHistoryRunValidator();
        }
    }
}
