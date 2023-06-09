﻿using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class FeeCaption : ObjectBase
    {
        int _FeeCaptionId;
        string _Code;
        string _Name;
        string _Year;
        string _ReviewCode; 
        bool _Active;

        public int FeeCaptionId
        {
            get { return _FeeCaptionId; }
            set
            {
                if (_FeeCaptionId != value)
                {
                    _FeeCaptionId = value;
                    OnPropertyChanged(() => FeeCaptionId);
                }
            }
        }

        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

     

        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }

      

        public string ReviewCode
        {
            get { return _ReviewCode; }
            set
            {
                if (_ReviewCode != value)
                {
                    _ReviewCode = value;
                    OnPropertyChanged(() => ReviewCode);
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

        class FeeCaptionValidator : AbstractValidator<FeeCaption>
        {
            public FeeCaptionValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
             
            }
        }

        protected override IValidator GetValidator()
        {
            return new FeeCaptionValidator();
        }
    }
}
