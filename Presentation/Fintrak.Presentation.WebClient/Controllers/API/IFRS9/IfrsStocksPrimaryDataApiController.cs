﻿using System;
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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ifrsstocksprimarydata")]
    [UsesDisposableService]
    public class IfrsStocksPrimaryDataApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsStocksPrimaryDataApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateifrsStocksPrimaryData")]
        public HttpResponseMessage UpdateIfrsStocksPrimaryData(HttpRequestMessage request, [FromBody]IfrsStocksPrimaryData ifrsStocksPrimaryDataModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsStocksPrimaryData = _IFRS9Service.UpdateIfrsStocksPrimaryData(ifrsStocksPrimaryDataModel);

                return request.CreateResponse<IfrsStocksPrimaryData>(HttpStatusCode.OK, ifrsStocksPrimaryData);
            });
        }

        [HttpPost]
        [Route("deleteifrsStocksPrimaryData")]
        public HttpResponseMessage DeleteIfrsStocksPrimaryData(HttpRequestMessage request, [FromBody]int ifrsStocksPrimaryDataId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsStocksPrimaryData ifrsStocksPrimaryData = _IFRS9Service.GetIfrsStocksPrimaryData(ifrsStocksPrimaryDataId);

                if (ifrsStocksPrimaryData != null)
                {
                    _IFRS9Service.DeleteIfrsStocksPrimaryData(ifrsStocksPrimaryDataId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsStocksPrimaryData found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsStocksPrimaryData/{ifrsStocksPrimaryDataId}")]
        public HttpResponseMessage GetIfrsStocksPrimaryData(HttpRequestMessage request,int ifrsStocksPrimaryDataId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsStocksPrimaryData ifrsStocksPrimaryData = _IFRS9Service.GetIfrsStocksPrimaryData(ifrsStocksPrimaryDataId);

                // notice no need to create a seperate model object since IfrsStocksPrimaryData entity will do just fine
                response = request.CreateResponse<IfrsStocksPrimaryData>(HttpStatusCode.OK, ifrsStocksPrimaryData);

                return response;
            });
        }

        [HttpGet]
        [Route("availableifrsStocksPrimaryDatas")]
        public HttpResponseMessage GetAvailableIfrsStocksPrimaryDatas(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsStocksPrimaryData[] ifrsStocksPrimaryDatas = _IFRS9Service.GetAllIfrsStocksPrimaryDatas();

                return request.CreateResponse<IfrsStocksPrimaryData[]>(HttpStatusCode.OK, ifrsStocksPrimaryDatas);
            });
        }
    }
}
