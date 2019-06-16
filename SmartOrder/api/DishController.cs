using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/dish")]
    public class DishController : ApiControllerBase
    {
        private IDishService dishService;

        public DishController(IErrorService errorService, IDishService dishService) : base(errorService)
        {
            this.dishService = dishService;
        }
        public HttpResponseMessage Post(HttpRequestMessage request, Dish dish)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var result = dishService.Add(dish);
                    dishService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                return response;
            });
        }
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listDish = dishService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, listDish);
                }
                return response;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage request, Dish dish)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    dishService.Update(dish);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Dish dish = dishService.Delete(id);
                    dishService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, dish);
                }
                return response;
            });
        }
    }

    
}