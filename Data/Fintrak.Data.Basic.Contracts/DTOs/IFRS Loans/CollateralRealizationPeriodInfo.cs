﻿using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class CollateralRealizationPeriodInfo
    {
        public CollateralRealizationPeriod CollateralRealizationPeriod { get; set; }
        public CollateralType CollateralType { get; set; }
    }
}