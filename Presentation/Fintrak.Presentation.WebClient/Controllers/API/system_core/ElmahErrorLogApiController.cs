using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Client.Core.Contracts;
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/elmaherrorlog")]
    [UsesDisposableService]
    public class ElmahErrorLogApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ElmahErrorLogApiController(ICoreService ifrs9Service)
        {
            _CoreService = ifrs9Service;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateelmaherrorlog")]
        public HttpResponseMessage UpdateElmahErrorLog(HttpRequestMessage request, [FromBody] ElmahErrorLog elmaherrorlogModel)
        {
            return GetHttpResponse(request, () =>
            {
                var elmaherrorlog = _CoreService.UpdateElmahErrorLog(elmaherrorlogModel);

                return request.CreateResponse<ElmahErrorLog>(HttpStatusCode.OK, elmaherrorlog);
            });
        }

        [HttpPost]
        [Route("deleteelmaherrorlog")]
        public HttpResponseMessage DeleteElmahErrorLog(HttpRequestMessage request, [FromBody] int Sequence)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ElmahErrorLog elmaherrorlog = _CoreService.GetElmahErrorLog(Sequence);

                if (elmaherrorlog != null)
                {
                    _CoreService.DeleteElmahErrorLog(Sequence);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ElmahErrorLog found under that Sequence.");

                return response;
            });
        }

        [HttpGet]
        [Route("getelmaherrorlog/{Sequence}")]
        public HttpResponseMessage GetElmahErrorLog(HttpRequestMessage request, int Sequence)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ElmahErrorLog elmaherrorlog = _CoreService.GetElmahErrorLog(Sequence);

                // notice no need to create a seperate model object since ElmahErrorLog entity will do just fine
                response = request.CreateResponse<ElmahErrorLog>(HttpStatusCode.OK, elmaherrorlog);

                return response;
            });
        }

        [HttpGet]
        [Route("getelmaherrorlogsbysearch/{searchParam}")]
        public HttpResponseMessage GetElmahErrorLogsbysearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () => {
                if (searchParam.Contains("ExportData "))
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~");
                    ElmahErrorLog[] elmaherrorlogs = _CoreService.GetElmahErrorLogBySearch(searchParam, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "LoanAmortizationSchedule.zip");
                    return response;
                }
                else
                {
                    ElmahErrorLog[] elmaherrorlogs = _CoreService.GetElmahErrorLogBySearch(searchParam, null);
                    return request.CreateResponse<ElmahErrorLog[]>(HttpStatusCode.OK, elmaherrorlogs.ToArray());
                }
            });
        }

        //[HttpGet]
        //[Route("availableelmaherrorlogs")]
        //public HttpResponseMessage GetAvailableElmahErrorLogs(HttpRequestMessage request, int defaultCount)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        string path = "";
        //        ElmahErrorLog[] elmaherrorlogs = _CoreService.GetAvailableElmahErrorLog(defaultCount, path);

        //        return request.CreateResponse<ElmahErrorLog[]>(HttpStatusCode.OK, elmaherrorlogs);
        //    });
        //}

        [HttpGet]
        [Route("availableelmaherrorlogs/{defaultCount}")]
        public HttpResponseMessage GetAvailableElmahErrorLogs(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount <= 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    ElmahErrorLog[] elmaherrorlogs = _CoreService.ExportElmahErrorLog(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "LoanAmortizationSchedule.zip");
                    return response;
                }
                else
                {
                    ElmahErrorLog[] elmaherrorlogs = _CoreService.ExportElmahErrorLog(defaultCount, null);

                    return request.CreateResponse<ElmahErrorLog[]>(HttpStatusCode.OK, elmaherrorlogs);
                }
            });
        }
    }
}
