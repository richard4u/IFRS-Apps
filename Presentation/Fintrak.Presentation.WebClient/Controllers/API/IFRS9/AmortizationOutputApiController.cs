using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/amortizationoutput")]
    [UsesDisposableService]
    public class AmortizationOutputApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AmortizationOutputApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateamortizationoutput")]
        public HttpResponseMessage UpdateAmortizationOutput(HttpRequestMessage request, [FromBody]AmortizationOutput amortizationoutputModel)
        {
            return GetHttpResponse(request, () =>
            {
                var amortizationoutput = _IFRS9Service.UpdateAmortizationOutput(amortizationoutputModel);

                return request.CreateResponse<AmortizationOutput>(HttpStatusCode.OK, amortizationoutput);
            });
        }

        [HttpPost]
        [Route("deleteamortizationoutput")]
        public HttpResponseMessage DeleteAmortizationOutput(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                AmortizationOutput amortizationoutput = _IFRS9Service.GetAmortizationOutput(ID);

                if (amortizationoutput != null)
                {
                    _IFRS9Service.DeleteAmortizationOutput(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No AmortizationOutput found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getamortizationoutput/{InstrumentID}")]
        public HttpResponseMessage GetAmortizationOutput(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AmortizationOutput amortizationoutput = _IFRS9Service.GetAmortizationOutput(ID);

                // notice no need to create a seperate model object since AmortizationOutput entity will do just fine
                response = request.CreateResponse<AmortizationOutput>(HttpStatusCode.OK, amortizationoutput);

                return response;
            });
        }

        //[HttpGet]
        //[Route("availableamortizationoutputs/{defaultCount}")]
        //public HttpResponseMessage GetAvailableAmortizationOutputs(HttpRequestMessage request,int defaultCount)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        AmortizationOutput[] amortizationoutputs = _IFRS9Service.GetAllAmortizationOutputs(defaultCount);

        //        return request.CreateResponse<AmortizationOutput[]>(HttpStatusCode.OK, amortizationoutputs);
        //    });
        //}

        [HttpGet]
        [Route("amortizationoutputstoreprocess/{date}")]
        public HttpResponseMessage AmortizationOutputStoreProcess(HttpRequestMessage request, DateTime date){
            return GetHttpResponse(request, () =>{
                AmortizationOutput[] amortizationoutputs = _IFRS9Service.AmortizationOutputStoreProcess(date);
                return request.CreateResponse<AmortizationOutput[]>(HttpStatusCode.OK, amortizationoutputs);
            });
        }


        [HttpGet]
        [Route("getamortizationoutputsbysearch/{searchParam}")]
        public HttpResponseMessage GetAmortizationoutputsbysearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () => {
                if (searchParam.Contains("ExportData "))
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~");
                    AmortizationOutput[] amortizationoutputs = _IFRS9Service.GetAmortizationOutputBySearch(searchParam, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "LoanAmortizationSchedule.zip");
                    return response;
                }
                else
                {
                    AmortizationOutput[] amortizationoutputs = _IFRS9Service.GetAmortizationOutputBySearch(searchParam, null);
                    return request.CreateResponse<AmortizationOutput[]>(HttpStatusCode.OK, amortizationoutputs.ToArray());
                }
            });
        }

        [HttpGet]
        [Route("availableamortizationoutputs/{defaultCount}")]
        public HttpResponseMessage GetAvailableAmortizationoutputs(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount <= 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    AmortizationOutput[] amortizationoutputs = _IFRS9Service.ExportAmortizationOutput(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "LoanAmortizationSchedule.zip");
                    return response;
                }
                else
                {
                    AmortizationOutput[] amortizationoutputs = _IFRS9Service.ExportAmortizationOutput(defaultCount, null);

                    return request.CreateResponse<AmortizationOutput[]>(HttpStatusCode.OK, amortizationoutputs);
                }
            });
        }
    }
}
