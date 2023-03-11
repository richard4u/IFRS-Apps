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
using Fintrak.Presentation.WebClient.Services;

namespace Fintrak.Presentation.WebClient.API
{

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/harmortization")]
    [UsesDisposableService]
    public class HarmortizationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public HarmortizationApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateharmortization")]
        public HttpResponseMessage UpdateHarmortization(HttpRequestMessage request, [FromBody] Harmortization[] HarmortizationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var harmortization = _IFRS9Service.UpdateHarmortization(HarmortizationModel);

                return request.CreateResponse<Harmortization>(HttpStatusCode.OK, harmortization);
            });
        }

        [HttpPost]
        [Route("deleteharmortization")]
        public HttpResponseMessage DeleteHarmortization(HttpRequestMessage request, [FromBody] int Harmortization_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Harmortization harmortization = _IFRS9Service.GetHarmortization(Harmortization_Id);

                if (harmortization != null)
                {
                    _IFRS9Service.DeleteHarmortization(Harmortization_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Harmortization found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getharmortization/{Harmortization_Id}")]
        public HttpResponseMessage GetHarmortization(HttpRequestMessage request, int Harmortization_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Harmortization harmortization = _IFRS9Service.GetHarmortization(Harmortization_Id);

                // notice no need to create a seperate model object since Harmortization entity will do just fine
                response = request.CreateResponse<Harmortization>(HttpStatusCode.OK, harmortization);

                return response;
            });
        }


        //[HttpGet]
        //[Route("allHarmortization")]
        //public HttpResponseMessage AllData(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {

        //        Harmortization[] Harmortization = _IFRS9Service.ShowAllData();

        //        return request.CreateResponse<Harmortization[]>(HttpStatusCode.OK, Harmortization.ToArray());

        //    });
        //}


        //[HttpGet]
        //[Route("Harmortizationstoreprocess/{date}")]
        //public HttpResponseMessage HarmortizationStoreProcess(HttpRequestMessage request, DateTime date)
        //{
        //    return GetHttpResponse(request, () => {
        //        Harmortization[] Harmortization = _IFRS9Service.HarmortizationStoreProcess(date);
        //        return request.CreateResponse<Harmortization[]>(HttpStatusCode.OK, Harmortization);
        //    });
        //}


        //[HttpGet]
        //[Route("getHarmortizationbysearch/{searchParam}")]
        //public HttpResponseMessage GetHarmortizationbysearch(HttpRequestMessage request, string searchParam)
        //{
        //    return GetHttpResponse(request, () => {
        //        if (searchParam.Contains("ExportData "))
        //        {
        //            string path = System.Web.Hosting.HostingEnvironment.MapPath("~");
        //            Harmortization[] Harmortization = _IFRS9Service.GetHarmortizationBySearch(searchParam, path + "ExportedData\\");
        //            var response = DownloadFileService.DownloadFile(path, "HarmortizationSchedule.zip");
        //            return response;
        //        }
        //        else
        //        {
        //            Harmortization[] Harmortization = _IFRS9Service.GetHarmortizationBySearch(searchParam, null);
        //            return request.CreateResponse<Harmortization[]>(HttpStatusCode.OK, Harmortization.ToArray());
        //        }
        //    });
        //}



        //[HttpGet]
        //[Route("availableHarmortizations/{defaultCount}")]
        //public HttpResponseMessage GetAvailableHarmortization(HttpRequestMessage request, int defaultCount)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        if (defaultCount <= 0)
        //        {
        //            string path = HostingEnvironment.MapPath("~");
        //            Harmortization[] Harmortization = _IFRS9Service.ExportHarmortization(defaultCount, path + "ExportedData\\");
        //            var response = DownloadFileService.DownloadFile(path, "LoanAmortizationSchedule.zip");
        //            return response;
        //        }
        //        else
        //        {
        //            Harmortization[] Harmortization = _IFRS9Service.ExportHarmortization(defaultCount, null);

        //            return request.CreateResponse<Harmortization[]>(HttpStatusCode.OK, Harmortization);
        //        }
        //    });
        //}


        //[HttpGet]
        //[Route("distinctassettype")]
        //public HttpResponseMessage GetDistinctAssetType(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        string[] assetType = _IFRS9Service.GetDistinctAssetType();

        //        var model = new List<KeyValueData>();

        //        foreach (var s in assetType)
        //            model.Add(new KeyValueData() { Key = s, Value = s });

        //        return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, model.ToArray());

        //    });
        //}

        //[HttpGet]
        //[Route("getHarmortizationbyassettype/{AssetType}")]
        //public HttpResponseMessage GetHarmortizationbyAssetType(HttpRequestMessage request, string AssetType)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        //IndividualScheduleData[] individualschedule = _LoanService.GetIndividualImpairments(refno);

        //        IndividualImpairmentData[] Harmortization = _IFRS9Service.GetHarmortizationbyAssetType(AssetType);

        //        // notice no need to create a seperate model object since BondComputation entity will do just fine
        //        response = request.CreateResponse<IndividualImpairmentData[]>(HttpStatusCode.OK, Harmortization);

        //        return response;
        //    });
        //}


        //[HttpGet]
        //[Route("getHarmortizationbyassettype/{assetTypeVal}")]
        //public HttpResponseMessage GetHarmortizationByAssetType(HttpRequestMessage request, string assetTypeVal)
        //{
        //    return GetHttpResponse(request, () => {
        //        HttpResponseMessage response = null;
        //        Harmortization[] Harmortization = _IFRS9Service.GetHarmortizationByAssetType(assetTypeVal);
        //        response = request.CreateResponse<Harmortization[]>(HttpStatusCode.OK, Harmortization.ToArray());
        //        return response;
        //    });
        //}

    }
}