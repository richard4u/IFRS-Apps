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
using Fintrak.Shared.Common.Services;
using System.Web.Hosting;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/cashflowtb")]
    [UsesDisposableService]
    public class CashFlowTBApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CashFlowTBApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatecashflowtb")]
        public HttpResponseMessage UpdateCashFlowTB(HttpRequestMessage request, [FromBody]CashFlowTB cashflowtbModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cashflowtb = _IFRS9Service.UpdateCashFlowTB(cashflowtbModel);

                return request.CreateResponse<CashFlowTB>(HttpStatusCode.OK, cashflowtb);
            });
        }

        [HttpPost]
        [Route("deletecashflowtb")]
        public HttpResponseMessage DeleteCashFlowTB(HttpRequestMessage request, [FromBody]int InstrumentID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CashFlowTB cashflowtb = _IFRS9Service.GetCashFlowTB(InstrumentID);

                if (cashflowtb != null)
                {
                    _IFRS9Service.DeleteCashFlowTB(InstrumentID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No CashFlowTB found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcashflowtb/{ID}")]
        public HttpResponseMessage GetCashFlowTB(HttpRequestMessage request, int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CashFlowTB cashflowtb = _IFRS9Service.GetCashFlowTB(ID);

                // notice no need to create a seperate model object since CashFlowTB entity will do just fine
                response = request.CreateResponse<CashFlowTB>(HttpStatusCode.OK, cashflowtb);

                return response;
            });
        }

        [HttpGet]
        [Route("getcashflowtbbysearch/{searchParam}")]
        public HttpResponseMessage GetCashflowtbbysearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () => {
                if (searchParam.Contains("ExportData "))
                {
                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~");
                    CashFlowTB[] cashFlowtb = _IFRS9Service.GetCashFlowTBBySearch(searchParam, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "LoanCashFlows_Results.zip");
                    return response;
                }
                else
                {
                    CashFlowTB[] cashFlowtb = _IFRS9Service.GetCashFlowTBBySearch(searchParam, null);
                    return request.CreateResponse<CashFlowTB[]>(HttpStatusCode.OK, cashFlowtb.ToArray());
                }
            });
        }

        //[HttpGet]
        //[Route("availablecashflowtbs")]
        //public HttpResponseMessage GetAvailableCashFlowTB(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        CashFlowTB[] cashflowtbs = _IFRS9Service.GetAllCashFlowTBs();

        //        return request.CreateResponse<CashFlowTB[]>(HttpStatusCode.OK, cashflowtbs);
        //    });
        //} 


        [HttpGet]
        [Route("availablecashflowtbs/{defaultCount}")]
        public HttpResponseMessage GetAvailableCashFlowTBs(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () => {
                if (defaultCount <= 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    CashFlowTB[] cashflowtbs = _IFRS9Service.GetAllCashFlowTBs(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "LoanCashFlows_Results.zip");
                    return response;
                }
                else
                {
                    CashFlowTB[] cashflowtbs = _IFRS9Service.GetAllCashFlowTBs(defaultCount, null);

                    return request.CreateResponse<CashFlowTB[]>(HttpStatusCode.OK, cashflowtbs);
                }
            });
        }
    }
}
