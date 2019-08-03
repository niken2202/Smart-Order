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
    }
}