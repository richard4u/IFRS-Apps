using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.Core.Contracts;
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/processhistory")]
    [UsesDisposableService]
    public class ProcessHistoryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProcessHistoryApiController(IExtractionProcessService coreService)
        {
            _ExtractionProcessService = coreService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updateprocesshistory")]
        public HttpResponseMessage UpdateProcessHistory(HttpRequestMessage request, [FromBody]ProcessHistory processHistoryModel)
        {
            return GetHttpResponse(request, () =>
            {
                var processHistory = _ExtractionProcessService.UpdateProcessHistory(processHistoryModel);

                return request.CreateResponse<ProcessHistory>(HttpStatusCode.OK, processHistory);
            });
        }

        [HttpPost]
        [Route("deleteprocesshistory")]
        public HttpResponseMessage DeleteProcessHistory(HttpRequestMessage request, [FromBody]int processHistoryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ProcessHistory processHistory = _ExtractionProcessService.GetProcessHistory(processHistoryId);

                if (processHistory != null)
                {
                    _ExtractionProcessService.DeleteProcessHistory(processHistoryId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ProcessHistory found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getprocesshistory/{processHistoryId}")]
        public HttpResponseMessage GetProcessHistory(HttpRequestMessage request, int processHistoryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProcessHistory processHistory = _ExtractionProcessService.GetProcessHistory(processHistoryId);

                // notice no need to create a seperate model object since ProcessHistory entity will do just fine
                response = request.CreateResponse<ProcessHistory>(HttpStatusCode.OK, processHistory);

                return response;
            });
        }

        [HttpGet]
        [Route("availableprocesshistorys/{defaultCount}")]
        public HttpResponseMessage GetAvailableProcessHistory(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                    ProcessHistory[] processHistory = _ExtractionProcessService.GetProcessHistorys(defaultCount);

                    return request.CreateResponse<ProcessHistory[]>(HttpStatusCode.OK, processHistory);
            });
        }


        [HttpGet]
        [Route("getallprocesshistory")]
        public HttpResponseMessage GetAvailableProcessHistory(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProcessHistory[] processHistory = _ExtractionProcessService.GetAllProcessHistory();

                return request.CreateResponse<ProcessHistory[]>(HttpStatusCode.OK, processHistory.OrderBy(c => c.ProcessHistoryId).ToArray());
            });
        }

        [HttpGet]
        [Route("getprocesshistoryrun")]
        public HttpResponseMessage GetAvailableProcessHistoryRun(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProcessHistoryRun[] processHistoryRun = _ExtractionProcessService.GetAllProcessHistoryRun();

                return request.CreateResponse<ProcessHistoryRun[]>(HttpStatusCode.OK, processHistoryRun.OrderBy(c => c.ProcessHistoryRunId).ToArray());
            });
        }

        [HttpPost]
        [Route("runprocesshistory")]
        public HttpResponseMessage RunProcessHistory(HttpRequestMessage request, int[] processhistoryrunId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //if (processHistoryRun.Count() > 0)
                //{
                //    foreach (var trigger in processTriggers)
                //    { }
                //}
                //for (int i = 1; i <= processhistoryrunId.Length; i++)
                foreach (var processhistoryrun in processhistoryrunId)
                {
                    ProcessHistoryRun processHistoryRun = _ExtractionProcessService.GetProcessHistoryRun(processhistoryrun);
                    if (processHistoryRun != null)
                    {
                        _ExtractionProcessService.RunProcessHistory(processhistoryrun);

                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ProcessHistoryRun found under that ID.");
                }
                return response;
            });
        }
    }
}
