using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API {
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/Ifrsexceptionreport")]
    [UsesDisposableService]
    public class IfrsexceptionreportApiController : ApiControllerBase {
        [ImportingConstructor]
        public IfrsexceptionreportApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsexceptionreport")]
        public HttpResponseMessage UpdateIfrsexceptionreport(HttpRequestMessage request, [FromBody]ifrsexceptionreport IfrsExceptionReportModel) {
            return GetHttpResponse(request, () => {
                var Ifrsloansinfo = _IFRS9Service.Updateifrsexceptionreport(IfrsExceptionReportModel);
                return request.CreateResponse<ifrsexceptionreport>(HttpStatusCode.OK, Ifrsloansinfo);
            });
        }


        [HttpPost]
        [Route("deleteIfrsexceptionreport")]
        public HttpResponseMessage DeleteIfrsexceptionreport(HttpRequestMessage request, [FromBody]int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                ifrsexceptionreport Ifrsloansinfo = _IFRS9Service.Getifrsexceptionreport(Id);
                if (Ifrsloansinfo != null) {
                    _IFRS9Service.Deleteifrsexceptionreport(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Ifrsexceptionreport data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableIfrsexceptionreport")]
        public HttpResponseMessage GetAvailableIfrsexceptionreports(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                ifrsexceptionreport[] Ifrsloansinfo = _IFRS9Service.GetAllifrsexceptionreport().ToArray();
                return request.CreateResponse<ifrsexceptionreport[]>(HttpStatusCode.OK, Ifrsloansinfo.ToArray());
            });
        }

        [HttpGet]
        [Route("getIfrsexceptionreport/{Id}")]
        public HttpResponseMessage GetIfrsexceptionreport(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                ifrsexceptionreport Ifrsloansinfo = _IFRS9Service.Getifrsexceptionreport(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<ifrsexceptionreport>(HttpStatusCode.OK, Ifrsloansinfo);
                return response;
            });
        }


        [HttpGet]
        [Route("getIfrsexceptionreportbysearch/{searchParam}")]
        public HttpResponseMessage GetIfrsexceptionreportBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                ifrsexceptionreport[] Ifrsloansinfo = _IFRS9Service.GetifrsexceptionreportBySearch(searchParam);
                return request.CreateResponse<ifrsexceptionreport[]>(HttpStatusCode.OK, Ifrsloansinfo.ToArray());
            });
        }


        [HttpGet]
        [Route("getexceptionreportbysearch/{exceptionType}/{classification}")]
        public HttpResponseMessage getexceptionreportbysearch(HttpRequestMessage request, string exceptionType, string classification)
        {
            return GetHttpResponse(request, () => {
                ifrsexceptionreport[] Ifrsloansinfo = _IFRS9Service.GetExceptionBySearch(exceptionType, classification);
                return request.CreateResponse<ifrsexceptionreport[]>(HttpStatusCode.OK, Ifrsloansinfo.ToArray());
            });
        }

        [HttpGet]
        [Route("availableIfrsexceptionreport/{defaultCount}")]
        public HttpResponseMessage GetAvailableIfrsexceptionreport(HttpRequestMessage request, int defaultCount) {
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    ifrsexceptionreport[] Ifrsloansinfo = _IFRS9Service.Getifrsexceptionreports(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "LoansInfo");
                    return response;
                }
                else
                {
                    ifrsexceptionreport[] Ifrsloansinfo = _IFRS9Service.Getifrsexceptionreports(defaultCount, null).ToArray();
                    return request.CreateResponse<ifrsexceptionreport[]>(HttpStatusCode.OK, Ifrsloansinfo.ToArray());
                }
            });
        }

    }
}
