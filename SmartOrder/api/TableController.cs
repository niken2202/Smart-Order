﻿using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/table")]
    public class TableController : ApiControllerBase
    {
        private ITableService tableService;

        public TableController(IErrorService errorService, ITableService tableService, IHistoryService historyService) : base(errorService, historyService)
        {
            this.tableService = tableService;
        }

        [Route("add"), Authorize]
        public HttpResponseMessage Post(HttpRequestMessage request, Table table)
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
                    var result = tableService.Add(table);
                    tableService.SaveChanges();
                    SaveHistory("Đã thêm bàn mới có ID: " + result.ID);
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, Table table)
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
                    tableService.Update(table);
                    SaveHistory("Đã cập nhật bàn có ID: " + table.ID);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [Route("applyimei")]
        public HttpResponseMessage ApplyEmei(HttpRequestMessage request, int tableID, string imei)
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
                   var table = tableService.SaveIMEI(tableID, imei);
                    tableService.SaveChanges();
                   SaveHistory("Đã cập nhật IMEI bàn có ID: " + table.ID);
                    response = request.CreateResponse(HttpStatusCode.OK,table);
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
                    var listTable = tableService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, listTable);
                }
                return response;
            });
        }

        [Route("getvariable")]
        public HttpResponseMessage GetVariableTable(HttpRequestMessage request)
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
                    var listTable = tableService.GetVariableTable();
                    response = request.CreateResponse(HttpStatusCode.OK, listTable);
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
                    Table table = tableService.Delete(id);
                    tableService.SaveChanges();
                    SaveHistory("Đã xóa bàn có id : " + table.ID);
                    response = request.CreateResponse(HttpStatusCode.OK, table);
                }
                return response;
            });
        }
    }
}