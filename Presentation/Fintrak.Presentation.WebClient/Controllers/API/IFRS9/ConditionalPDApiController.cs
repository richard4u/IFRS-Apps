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
        [RoutePrefix("api/conditionalpd")]
        [UsesDisposableService]
        public class ConditionalPDApiController : ApiControllerBase
        {
            [ImportingConstructor]
            public ConditionalPDApiController(IIFRS9Service ifrs9Service)
            {
                _IFRS9Service = ifrs9Service;
            }

            IIFRS9Service _IFRS9Service;

            protected override void RegisterServices(List<IServiceContract> disposableServices)
            {
                disposableServices.Add(_IFRS9Service);
            }

            [HttpPost]
            [Route("updateconditionalpd")]
            public HttpResponseMessage UpdateConditionalPD(HttpRequestMessage request, [FromBody]ConditionalPD conditionalpdModel)
            {
                return GetHttpResponse(request, () =>
                {
                    var conditionalpd = _IFRS9Service.UpdateConditionalPD(conditionalpdModel);

                    return request.CreateResponse<ConditionalPD>(HttpStatusCode.OK, conditionalpd);
                });
            }

            [HttpPost]
            [Route("deleteconditionalpd")]
            public HttpResponseMessage DeleteConditionalPD(HttpRequestMessage request, [FromBody]int ConditionalPD_Id)
            {
                return GetHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;

                    // not that calling the WCF service here will authenticate access to the data
                    ConditionalPD conditionalpd = _IFRS9Service.GetConditionalPD(ConditionalPD_Id);

                    if (conditionalpd != null)
                    {
                        _IFRS9Service.DeleteConditionalPD(ConditionalPD_Id);

                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ConditionalPD found under that ID.");

                    return response;
                });
            }

            [HttpGet]
            [Route("getconditionalpd/{ConditionalPD_Id}")]
            public HttpResponseMessage GetConditionalPD(HttpRequestMessage request, int ConditionalPD_Id)
            {
                return GetHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;

                    ConditionalPD conditionalpd = _IFRS9Service.GetConditionalPD(ConditionalPD_Id);

                    // notice no need to create a seperate model object since ConditionalPD entity will do just fine
                    response = request.CreateResponse<ConditionalPD>(HttpStatusCode.OK, conditionalpd);

                    return response;
                });
            }


            [HttpGet]
            [Route("allconditionalpd")]
            public HttpResponseMessage AllData(HttpRequestMessage request)
            {
                return GetHttpResponse(request, () =>
                {

                    ConditionalPD[] conditionalpd = _IFRS9Service.ShowAllData();

                    return request.CreateResponse<ConditionalPD[]>(HttpStatusCode.OK, conditionalpd.ToArray());

                });
            }


            [HttpGet]
            [Route("conditionalpdstoreprocess/{date}")]
            public HttpResponseMessage ConditionalPDStoreProcess(HttpRequestMessage request, DateTime date)
            {
                return GetHttpResponse(request, () => {
                    ConditionalPD[] conditionalpd = _IFRS9Service.ConditionalPDStoreProcess(date);
                    return request.CreateResponse<ConditionalPD[]>(HttpStatusCode.OK, conditionalpd);
                });
            }


            [HttpGet]
            [Route("getconditionalpdbysearch/{searchParam}")]
            public HttpResponseMessage GetConditionalPDbysearch(HttpRequestMessage request, string searchParam)
            {
                return GetHttpResponse(request, () => {
                    if (searchParam.Contains("ExportData "))
                    {
                        string path = System.Web.Hosting.HostingEnvironment.MapPath("~");
                        ConditionalPD[] conditionalpd = _IFRS9Service.GetConditionalPDBySearch(searchParam, path + "ExportedData\\");
                        var response = DownloadFileService.DownloadFile(path, "ConditionalPDSchedule.zip");
                        return response;
                    }
                    else
                    {
                        ConditionalPD[] conditionalpd = _IFRS9Service.GetConditionalPDBySearch(searchParam, null);
                        return request.CreateResponse<ConditionalPD[]>(HttpStatusCode.OK, conditionalpd.ToArray());
                    }
                });
            }



            [HttpGet]
            [Route("availableconditionalpds/{defaultCount}")]
            public HttpResponseMessage GetAvailableConditionalPD(HttpRequestMessage request, int defaultCount)
            {
                return GetHttpResponse(request, () =>
                {
                    if (defaultCount <= 0)
                    {
                        string path = HostingEnvironment.MapPath("~");
                        ConditionalPD[] conditionalpd = _IFRS9Service.ExportConditionalPD(defaultCount, path + "ExportedData\\");
                        var response = DownloadFileService.DownloadFile(path, "LoanAmortizationSchedule.zip");
                        return response;
                    }
                    else
                    {
                        ConditionalPD[] conditionalpd = _IFRS9Service.ExportConditionalPD(defaultCount, null);

                        return request.CreateResponse<ConditionalPD[]>(HttpStatusCode.OK, conditionalpd);
                    }
                });
            }


            [HttpGet]
            [Route("distinctassettype")]
            public HttpResponseMessage GetDistinctAssetType(HttpRequestMessage request)
            {
                return GetHttpResponse(request, () =>
                {
                    string[] assetType = _IFRS9Service.GetDistinctAssetType();

                    var model = new List<KeyValueData>();

                    foreach (var s in assetType)
                        model.Add(new KeyValueData() { Key = s, Value = s });

                    return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, model.ToArray());

                });
            }

        //[HttpGet]
        //[Route("getconditionalpdbyassettype/{AssetType}")]
        //public HttpResponseMessage GetConditionalPDbyAssetType(HttpRequestMessage request, string AssetType)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        //IndividualScheduleData[] individualschedule = _LoanService.GetIndividualImpairments(refno);

        //        IndividualImpairmentData[] conditionalpd = _IFRS9Service.GetConditionalPDbyAssetType(AssetType);

        //        // notice no need to create a seperate model object since BondComputation entity will do just fine
        //        response = request.CreateResponse<IndividualImpairmentData[]>(HttpStatusCode.OK, conditionalpd);

        //        return response;
        //    });
        //}


            [HttpGet]
            [Route("getconditionalpdbyassettype/{assetTypeVal}")]
            public HttpResponseMessage GetConditionalPDByAssetType(HttpRequestMessage request, string assetTypeVal)
            {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                ConditionalPD[] conditionalpd = _IFRS9Service.GetConditionalPDByAssetType(assetTypeVal);
                response = request.CreateResponse<ConditionalPD[]>(HttpStatusCode.OK, conditionalpd.ToArray());
                return response;
            });
            }

    }
}