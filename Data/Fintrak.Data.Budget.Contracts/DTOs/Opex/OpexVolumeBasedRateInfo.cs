﻿using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class OpexVolumeBasedRateInfo
    {
        public OpexVolumeBasedRate OpexVolumeBasedRate { get; set; }
        public OpexItem OpexItem { get; set; }
        public OpexCategory OpexCategory { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
    }
}