﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Data.Core;
using Fintrak.Data.Basic;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Services.Installer
{
    public class DataManager
    {
        SolutionRepository _solutionRepository = null;
        SolutionRunDateRepository _runDateRepository = null;
        ExtractionJobRepository _jobRepository = null;
        ExtractionTriggerRepository _triggerRepository = null;
        ExtractionRepository _extractionRepository = null;

        public DataManager()
        {
            _solutionRepository = new SolutionRepository();
            _runDateRepository = new SolutionRunDateRepository();
            _jobRepository = new ExtractionJobRepository();
            _triggerRepository = new ExtractionTriggerRepository();
            _extractionRepository = new ExtractionRepository();
        }


        public List<ExtractionJob> GetNewOrPendingJobs()
        {
            var jobs = _jobRepository.Get().Where(c=>c.Status == PackageStatus.New ||  c.Status == PackageStatus.Pending);

            return jobs.ToList();
        }
        public List<ExtractionJob> GetJobsToStop()
        {
            var jobs = _jobRepository.Get().Where(c => c.Status == PackageStatus.Stop || c.Status == PackageStatus.Done || c.Status == PackageStatus.Fail);

            return jobs.ToList();
        }
        public void UpdateJob(ExtractionJob job)
        {
            _jobRepository.Update(job);
        }
  
    }
}
