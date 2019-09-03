using Service;
using SmartOrder.Infrastructure;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/history"), Authorize(Roles = "Admin")]
    public class HistoryController : ApiControllerBase
    {
        public HistoryController(IErrorService errorService, IHistoryService _historyService) : base(errorService, _historyService)
        {
           
        }
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listHis = _historyService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, listHis);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("gettimerange")]
        public HttpResponseMessage GetTimeRange(HttpRequestMessage request, DateTime fromDate, DateTime toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listHis = _historyService.GetTimeRange(fromDate, toDate);
                    response = request.CreateResponse(HttpStatusCode.OK, listHis);
                }
                return response;
            });
        }

        [Route("getlast7day"), HttpGet]
        public HttpResponseMessage GetBillLast7Days(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listHis = _historyService.GetHistoryLast7Days();
                    response = request.CreateResponse(HttpStatusCode.OK, listHis);
                }
                return response;
            });
        }

        [Route("getlastmonth"), HttpGet]
        public HttpResponseMessage GetLastMonth(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listHis = _historyService.GetHistoryLastMonth();
                    response = request.CreateResponse(HttpStatusCode.OK, listHis);
                }
                return response;
            });
        }

        [Route("gettoday"), HttpGet]
        public HttpResponseMessage GetHistoryToday(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listHis = _historyService.GetHistoryToday();
                    response = request.CreateResponse(HttpStatusCode.OK, listHis);
                }
                return response;
            });
        }
    }
}
