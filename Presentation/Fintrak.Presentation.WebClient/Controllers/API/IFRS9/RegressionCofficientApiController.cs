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
    [RoutePrefix("api/RegressionCofficient")]
    [UsesDisposableService]
    public class RegressionCofficientApiController : ApiControllerBase {
        [ImportingConstructor]
        public RegressionCofficientApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateRegressionCofficient")]
        public HttpResponseMessage UpdateRegressionCofficient(HttpRequestMessage request, [FromBody] RegressionCofficient RegressionCofficientModel) {
            return GetHttpResponse(request, () => {
                var RegressionCofficient = _IFRS9Service.UpdateRegressionCofficient(RegressionCofficientModel);
                return request.CreateResponse<RegressionCofficient>(HttpStatusCode.OK, RegressionCofficient);
            });
        }


        [HttpPost]
        [Route("deleteRegressionCofficient")]
        public HttpResponseMessage DeleteRegressionCofficient(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                RegressionCofficient RegressionCofficient = _IFRS9Service.GetRegressionCofficient(Id);
                if (RegressionCofficient != null)
                {

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No RegressionCofficient data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableRegressionCofficient")]
        public HttpResponseMessage GetAvailableRegressionCofficients(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                RegressionCofficient [] RegressionCofficient = _IFRS9Service.GetAllRegressionCofficient().ToArray();
                return request.CreateResponse<RegressionCofficient[]>(HttpStatusCode.OK, RegressionCofficient.ToArray());
            });
        }

        [HttpGet]
        [Route("getRegressionCofficient/{Id}")]
        public HttpResponseMessage GetRegressionCofficient(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                RegressionCofficient RegressionCofficient = _IFRS9Service.GetRegressionCofficient(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<RegressionCofficient>(HttpStatusCode.OK, RegressionCofficient);
                return response;
            });
        }


        [HttpGet]
        [Route("getRegressionCofficientbysearch/{searchParam}")]
        public HttpResponseMessage GetRegressionCofficientBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                RegressionCofficient[] RegressionCofficient = _IFRS9Service.GetRegressionCofficientBySearch(searchParam);
                return request.CreateResponse<RegressionCofficient[]>(HttpStatusCode.OK, RegressionCofficient.ToArray());
            });
        }


        [HttpGet]
        [Route("availableRegressionCofficient/{defaultCount}")]
        public HttpResponseMessage GetAvailableRegressionCofficient(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    RegressionCofficient[] RegressionCofficient = _IFRS9Service.GetRegressionCofficients(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "LGD%20Result%20-%20Overdrafts.zip");
                    return response;
                }
                else
                {
                    RegressionCofficient[] RegressionCofficient = _IFRS9Service.GetRegressionCofficients(defaultCount, null).ToArray();
                    return request.CreateResponse<RegressionCofficient[]>(HttpStatusCode.OK, RegressionCofficient.ToArray());
                }
            });
        }

    }
}
