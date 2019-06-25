using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SmartOrder.api
{
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
       private IStatisticService statisticService;
        public StatisticController(IStatisticService statisticService, IErrorService errorService) : base(errorService)
        {
            this.statisticService = statisticService;
        }

        [Route("a")]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request, string fromeDate, string toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = "hele";
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}
