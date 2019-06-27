using Service;
using SmartOrder.Infrastructure;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SmartOrder.api
{
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
       private IStatisticService statisticService;
        public StatisticController( IErrorService errorService, IStatisticService statisticService, IHistoryService historyService) : base(errorService, historyService)
        {
            this.statisticService = statisticService;
        }

        [Route("getRevenue")]
        public HttpResponseMessage Get(HttpRequestMessage request, DateTime fromDate, DateTime toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = statisticService.GetRevenueStatistic(fromDate,toDate);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}
