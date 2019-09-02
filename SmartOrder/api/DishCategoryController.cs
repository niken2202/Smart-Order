using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/dishcategory")]
    public class DishCategoryController : ApiControllerBase
    {
        IDishCategoryService dishService;
        public DishCategoryController(IDishCategoryService dishService, IErrorService errorService, IHistoryService historyService) : base(errorService, historyService)
        {
            this.dishService = dishService;
        }

        [Route("add"), Authorize(Roles = "Admin")]
        public HttpResponseMessage Post(HttpRequestMessage request, DishCategory category)
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
                    var result = dishService.Add(category);
                    dishService.SaveChanges();
                    SaveHistory("Thêm loại món có ID " + category.ID);
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

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listDishCaterory = dishService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, listDishCaterory);
                }
                return response;
            });
        }

        [Route("update"),Authorize(Roles = "Admin")]
        public HttpResponseMessage Put(HttpRequestMessage request, DishCategory dishCategory)
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
                    dishService.Update(dishCategory);
                    SaveHistory("Cập nhật loại sản phẩm có ID " + dishCategory.ID);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [Route("delete"), Authorize(Roles = "Admin")]
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
                    DishCategory dishcategory = dishService.Delete(id);
                    dishService.SaveChanges();
                    SaveHistory("Xóa sản phẩm có ID " + id);
                    response = request.CreateResponse(HttpStatusCode.OK, dishcategory);
                }
                return response;

            });
        }
    }
}
