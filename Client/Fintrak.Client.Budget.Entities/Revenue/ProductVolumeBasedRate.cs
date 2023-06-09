﻿using System;
using System.Linq;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class ProductVolumeBasedRate : TransactionObjectDoubleBase
    {
        int _ProductVolumeBasedRateId;
        string _DefintionCode;
        string _MisCode;       
        string _Year;
        string _ProductCode;     
        string _ReviewCode; 
        bool _Active;

        public int ProductVolumeBasedRateId
        {
            get { return _ProductVolumeBasedRateId; }
            set
            {
                if (_ProductVolumeBasedRateId != value)
                {
                    _ProductVolumeBasedRateId = value;
                    OnPropertyChanged(() => ProductVolumeBasedRateId);
                }
            }
        }

        public string DefintionCode
        {
            get { return _DefintionCode; }
            set
            {
                if (_DefintionCode != value)
                {
                    _DefintionCode = value;
                    OnPropertyChanged(() => DefintionCode);
                }
            }
        }

        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
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


        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
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

        class ProductVolumeBasedRateValidator : AbstractValidator<ProductVolumeBasedRate>
        {
            public ProductVolumeBasedRateValidator()
            {
                RuleFor(obj => obj.DefintionCode).NotEmpty().WithMessage("DefintionCode is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProductVolumeBasedRateValidator();
        }
    }
}
