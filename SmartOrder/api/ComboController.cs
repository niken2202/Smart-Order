using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/combo")]
    public class ComboController : ApiControllerBase
    {
        IComboService comboService;

        public ComboController(IErrorService errorService, IComboService comboService, IHistoryService historyService) : base(errorService, historyService)
        {
            this.comboService = comboService;
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, Combo combo)
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
                    var result = comboService.Add(combo);
                    comboService.SaveChanges();
                    SaveHistory("Add Combo has ID " + combo.ID);
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
                    var listDish = comboService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, listDish);
                }
                return response;
            });
        }
        [Route("getbyid")]
        public HttpResponseMessage Get(HttpRequestMessage request,int comboId)
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
                    var combo = comboService.GetById(comboId);
                    response = request.CreateResponse(HttpStatusCode.OK, combo);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, Combo combo)
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
                    comboService.Update(combo);
                    SaveHistory("Update Combo has ID " + combo.ID);
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
                    Combo combo = comboService.Delete(id);
                    comboService.SaveChanges();
                    SaveHistory("Delete Combo has ID " + combo.ID);
                    response = request.CreateResponse(HttpStatusCode.OK, combo);
                }
                return response;
            });
        }
    }
}
