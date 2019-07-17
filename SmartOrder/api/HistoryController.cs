using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/history")]
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
    }
}
