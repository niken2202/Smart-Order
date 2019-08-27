using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/cartdetail")]
    public class CartDetailController : ApiControllerBase
    {
        private ICartDetailService cartDetailService;

        public CartDetailController(ICartDetailService cartDetailService, IErrorService errorService, IHistoryService _historyService) : base(errorService, _historyService)
        {
            this.cartDetailService = cartDetailService;
        }
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, CartDetail cartDetail)
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
                    cartDetailService.Update(cartDetail);
                    cartDetailService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
        [Route("add"),HttpPost]
        public HttpResponseMessage Add(HttpRequestMessage request, CartDetail cartDetail)
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
                    var cd = cartDetailService.Add(cartDetail);
                    cartDetailService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, cd);
                }
                return response;
            });
        }
        [Route("delete"), HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var cd = cartDetailService.Delete(id);
                    cartDetailService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, cd);
                }
                return response;
            });
        }
        [Route("getall")]
        //   [Authorize(Roles = "Guest, Cashier")]
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
                    var cartDetail = cartDetailService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, cartDetail);
                }
                return response;
            });
        }
    }
}