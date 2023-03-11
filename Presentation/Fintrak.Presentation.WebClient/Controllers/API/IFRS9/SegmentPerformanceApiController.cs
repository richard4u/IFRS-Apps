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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/segmentperformance")]
    [UsesDisposableService]
    public class SegmentPerformanceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SegmentPerformanceApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatesegmentperformance")]
        public HttpResponseMessage UpdateSegmentPerformance(HttpRequestMessage request, [FromBody]SegmentPerformance segmentperformanceModel)
        {
            return GetHttpResponse(request, () =>
            {
                var segmentperformance = _IFRS9Service.UpdateSegmentPerformance(segmentperformanceModel);

                return request.CreateResponse<SegmentPerformance>(HttpStatusCode.OK, segmentperformance);
            });
        }

        [HttpPost]
        [Route("deletesegmentperformance")]
        public HttpResponseMessage DeleteSegmentPerformance(HttpRequestMessage request, [FromBody]int SegmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SegmentPerformance segmentperformance = _IFRS9Service.GetSegmentPerformance(SegmentId);

                if (segmentperformance != null)
                {
                    _IFRS9Service.DeleteSegmentPerformance(SegmentId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No segmentperformance found under that Id.");

                return response;
            });
        }

        [HttpGet]
        [Route("getsegmentperformance/{segmentId}")]
        public HttpResponseMessage GetSegmentPerformance(HttpRequestMessage request,int segmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SegmentPerformance segmentperformance = _IFRS9Service.GetSegmentPerformance(segmentId);

                // notice no need to create a seperate model object since SegmentPerformance entity will do just fine
                response = request.CreateResponse<SegmentPerformance>(HttpStatusCode.OK, segmentperformance);

                return response;
            });
        }
        

        [HttpGet]
        [Route("availablesegmentperformances")]
        public HttpResponseMessage GetAvailableSegmentPerformances(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SegmentPerformance[] segmentperformances = _IFRS9Service.GetAllSegmentPerformances();

                return request.CreateResponse<SegmentPerformance[]>(HttpStatusCode.OK, segmentperformances);
            });
        }
    }
}
