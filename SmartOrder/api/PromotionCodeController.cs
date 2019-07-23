using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/promotioncode"), Authorize]
    public class PromotionCodeController : ApiControllerBase
    {
        private IPromotionCodeService promotionCodeService;
        public PromotionCodeController(IPromotionCodeService promotionCodeService,IErrorService errorService, IHistoryService _historyService) : base(errorService, _historyService)
        {
            this.promotionCodeService = promotionCodeService;
        }
        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, PromotionCode code)
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
                    var result = promotionCodeService.Add(code);
                    promotionCodeService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
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
                    var listCode = promotionCodeService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, listCode);
                }
                return response;
            });
        }

        [Route("getbyid")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
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
                    var code = promotionCodeService.GetById(id);
                    SaveHistory("Get Bill by id" + id);
                    response = request.CreateResponse(HttpStatusCode.OK, code);
                }
                return response;
            });
        }
    }
}