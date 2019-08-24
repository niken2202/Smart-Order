using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiControllerBase
    {
        private ICartService cartService;
        public CartController(ICartService cartService,IErrorService errorService, IHistoryService _historyService) : base(errorService, _historyService)
        {
            this.cartService = cartService;
        }
        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, Cart cart)
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
                    var result = cartService.Add(cart);
                    cartService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                return response;
            });
        }
        [Route("getbytable")]
        //   [Authorize(Roles = "Guest, Cashier")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int tableId)
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
                    var cart = cartService.GetCartByTable(tableId);
                    response = request.CreateResponse(HttpStatusCode.OK,  cart);
                }
                return response;
            });
        }

        [Route("changetable"), HttpPut]
        public HttpResponseMessage ChangeTable(HttpRequestMessage request, Cart cart)
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
                    cartService.ChangeTable(cart);
                    cartService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, Cart cart)
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
                    cartService.Update(cart);
                    cartService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [Route("delete")]
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
                    Cart cart = cartService.Delete(id);
                    cartService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, cart);
                }
                return response;
            });
        }
    }
}