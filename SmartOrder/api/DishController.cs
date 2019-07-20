﻿using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/dish")]
    [Authorize]
    public class DishController : ApiControllerBase
    {
        private IDishService dishService;

        public DishController(IErrorService errorService, IDishService dishService, IHistoryService historyService) : base(errorService, historyService)
        {
            this.dishService = dishService;
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, Dish dish)
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
                    var result = dishService.Add(dish);
                    dishService.SaveChanges();
                    SaveHistory("Add new Dish with ID: " + result.ID);
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
                    var listDish = dishService.GetAll();
                    int total = dishService.GetDishCount();
                    response = request.CreateResponse(HttpStatusCode.OK, new { listDish, total });
                }
                return response;
            });
        }

        [Route("getbycombo")]
        public HttpResponseMessage GetByCombo(HttpRequestMessage request, int comboId)
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
                    var listDish = dishService.GetAllByComboId(comboId);
                    response = request.CreateResponse(HttpStatusCode.OK, listDish);
                }
                return response;
            });
        }
        [Route("getbyid")]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
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
                    var listDish = dishService.GetById(id);
                    response = request.CreateResponse(HttpStatusCode.OK, listDish);
                }
                return response;
            });
        }
        [Route("getbycategory")]
        public HttpResponseMessage GetByCategory(HttpRequestMessage request, int cateId)
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
                    var listDish = dishService.GetByCategogy(cateId);
                    response = request.CreateResponse(HttpStatusCode.OK, listDish);
                }
                return response;
            });
        }
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, Dish dish)
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
                    dishService.Update(dish);
                    SaveHistory("Add new Dish with ID: " + dish.ID);

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
                    Dish dish = dishService.Delete(id);
                    dishService.SaveChanges();
                    SaveHistory("Add new Dish with ID: " + dish.ID);
                    response = request.CreateResponse(HttpStatusCode.OK, dish);
                }
                return response;
            });
        }
    }
}