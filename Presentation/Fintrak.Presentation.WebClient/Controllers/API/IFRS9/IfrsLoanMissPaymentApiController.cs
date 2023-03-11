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
    [RoutePrefix("api/ifrsloanmisspayment")]
    [UsesDisposableService]
    public class IfrsloanmisspaymentAPIController   : ApiControllerBase {
        [ImportingConstructor]
        public IfrsloanmisspaymentAPIController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateifrsloanmisspayment")]
        public HttpResponseMessage Updateifrsloanmisspayment(HttpRequestMessage request, [FromBody] IfrsLoanMissPayment ifrsloanmisspaymentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsloanmisspayment = _IFRS9Service.UpdateIfrsLoanMissPayment(ifrsloanmisspaymentModel);
                return request.CreateResponse<IfrsLoanMissPayment>(HttpStatusCode.OK, ifrsloanmisspayment);
            });
        }


        [HttpPost]
        [Route("deleteifrsloanmisspayment")]
        public HttpResponseMessage Deleteifrsloanmisspayment(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                IfrsLoanMissPayment ifrsloanmisspayment = _IFRS9Service.GetIfrsLoanMissPayment(Id);
                if (ifrsloanmisspayment != null)
                {
                    _IFRS9Service.DeleteIfrsLoanMissPayment(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsloanmisspayment data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableifrsloanmisspayment")]
        public HttpResponseMessage GetAvailableifrsloanmisspayments(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                IfrsLoanMissPayment[] ifrsloanmisspayment = _IFRS9Service.GetAllIfrsLoanMissPayment().ToArray();
                return request.CreateResponse<IfrsLoanMissPayment[]>(HttpStatusCode.OK, ifrsloanmisspayment.ToArray());
            });
        }

        [HttpGet]
        [Route("getifrsloanmisspayment/{Id}")]
        public HttpResponseMessage Getifrsloanmisspayment(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IfrsLoanMissPayment ifrsloanmisspayment = _IFRS9Service.GetIfrsLoanMissPayment(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<IfrsLoanMissPayment>(HttpStatusCode.OK, ifrsloanmisspayment);
                return response;
            });
        }


        //[HttpGet]
        //[Route("getifrsloanmisspaymentbysearch/{searchParam}")]
        //public HttpResponseMessage GetifrsloanmisspaymentBySearch(HttpRequestMessage request, string searchParam) {
        //    return GetHttpResponse(request, () => {
        //        ifrsloanmisspayment[] ifrsloanmisspayment = _IFRS9Service.GetifrsloanmisspaymentBySearch(searchParam);
        //        return request.CreateResponse<ifrsloanmisspayment[]>(HttpStatusCode.OK, ifrsloanmisspayment.ToArray());
        //    });
        //}


        //[HttpGet]
        //[Route("availableifrsloanmisspayment/{defaultCount}")]
        //public HttpResponseMessage GetAvailableifrsloanmisspayment(HttpRequestMessage request, int defaultCount) {
        //    return GetHttpResponse(request, () => {
        //        if (defaultCount == 0)
        //        {
        //            string path = HostingEnvironment.MapPath("~");
        //            ifrsloanmisspayment[] ifrsloanmisspayment = _IFRS9Service.Getifrsloanmisspayments(defaultCount, path + "ExportedData\\").ToArray();
        //            var response = DownloadFileService.DownloadFile(path, "LGD%20Result%20-%20Overdrafts.zip");
        //            return response;
        //        }
        //        else
        //        {
        //            ifrsloanmisspayment[] ifrsloanmisspayment = _IFRS9Service.Getifrsloanmisspayments(defaultCount, null).ToArray();
        //            return request.CreateResponse<ifrsloanmisspayment[]>(HttpStatusCode.OK, ifrsloanmisspayment.ToArray());
        //        }
        //    });
        //}

    }
}
