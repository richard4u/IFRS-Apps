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
    [RoutePrefix("api/macroeconomicforecast")]
    [UsesDisposableService]
    public class MacroeconomicForecastApiController : ApiControllerBase {
        [ImportingConstructor]
        public MacroeconomicForecastApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemacroeconomicforecast")]
        public HttpResponseMessage Updatemacroeconomicforecast([FromBody] MacroEconomicForeCast macroeconomicforecastModel, HttpRequestMessage request)
        {
            return GetHttpResponse(request, () => {
                var macroeconomicforecast = _IFRS9Service.UpdateMacroEconomicForeCast(macroeconomicforecastModel);
                return request.CreateResponse<MacroEconomicForeCast>(HttpStatusCode.OK, macroeconomicforecast);
            });
        }


        [HttpPost]
        [Route("deletemacroeconomicforecast")]
        public HttpResponseMessage Deletemacroeconomicforecast(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                MacroEconomicForeCast macroeconomicforecast = _IFRS9Service.GetMacroEconomicForeCast(Id);
                if (macroeconomicforecast != null)
                {

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No macroeconomicforecast data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availablemacroeconomicforecast")]
        public HttpResponseMessage GetAvailablemacroeconomicforecasts(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                MacroEconomicForeCast [] macroeconomicforecast = _IFRS9Service.GetAllMacroEconomicForeCast().ToArray();
                return request.CreateResponse<MacroEconomicForeCast[]>(HttpStatusCode.OK, macroeconomicforecast.ToArray());
            });
        }

        [HttpGet]
        [Route("getmacroeconomicforecast/{Id}")]
        public HttpResponseMessage Getmacroeconomicforecast(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                MacroEconomicForeCast macroeconomicforecast = _IFRS9Service.GetMacroEconomicForeCast(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<MacroEconomicForeCast>(HttpStatusCode.OK, macroeconomicforecast);
                return response;
            });
        }


        //[HttpGet]
        //[Route("getmacroeconomicforecastbysearch/{searchParam}")]
        //public HttpResponseMessage GetmacroeconomicforecastBySearch(HttpRequestMessage request, string searchParam) {
        //    return GetHttpResponse(request, () => {
        //        macroeconomicforecast[] macroeconomicforecast = _IFRS9Service.GetmacroeconomicforecastBySearch(searchParam);
        //        return request.CreateResponse<macroeconomicforecast[]>(HttpStatusCode.OK, macroeconomicforecast.ToArray());
        //    });
        //}


        //[HttpGet]
        //[Route("availablemacroeconomicforecast/{defaultCount}")]
        //public HttpResponseMessage GetAvailablemacroeconomicforecast(HttpRequestMessage request, int defaultCount) {
        //    return GetHttpResponse(request, () => {
        //        if (defaultCount == 0)
        //        {
        //            string path = HostingEnvironment.MapPath("~");
        //            macroeconomicforecast[] macroeconomicforecast = _IFRS9Service.Getmacroeconomicforecasts(defaultCount, path + "ExportedData\\").ToArray();
        //            var response = DownloadFileService.DownloadFile(path, "LGD%20Result%20-%20Overdrafts.zip");
        //            return response;
        //        }
        //        else
        //        {
        //            macroeconomicforecast[] macroeconomicforecast = _IFRS9Service.Getmacroeconomicforecasts(defaultCount, null).ToArray();
        //            return request.CreateResponse<macroeconomicforecast[]>(HttpStatusCode.OK, macroeconomicforecast.ToArray());
        //        }
        //    });
        //}

    }
}
