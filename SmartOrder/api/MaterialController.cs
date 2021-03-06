﻿using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/material")]
    public class MaterialController : ApiControllerBase
    {
        IMaterialServie materialService;
        public MaterialController(IErrorService errorService, IMaterialServie materialService, IHistoryService historyService) : base(errorService, historyService)
        {
            this.materialService = materialService;
        }
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, Material material)
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
                    var result = materialService.Add(material);
                    materialService.SaveChanges();
                    SaveHistory("Add new material with ID: " + result.ID);
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                return response;
            });
        }


        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request, int pageIndex, int pageSize, int totalRow)
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
                    var listDish = materialService.GetAll(pageIndex,  pageSize, out totalRow);
                    response = request.CreateResponse(HttpStatusCode.OK, listDish);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, Material material)
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
                    materialService.Update(material);
                    SaveHistory("Update material with ID: " + material.ID);

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
                    Material material = materialService.Delete(id);
                    materialService.SaveChanges();
                    SaveHistory("Delete material with ID: " + material.ID);
                    response = request.CreateResponse(HttpStatusCode.OK, material);
                }
                return response; 
                
            });
        }
    }
}
