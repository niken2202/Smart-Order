using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SmartOrder.api
{
    [RoutePrefix("api/bill")]
    public class BillController : ApiControllerBase
    {
        private IBillService billService;

        public BillController(IErrorService errorService, IBillService billService, IHistoryService historyService) : base(errorService, historyService)
        {
            this.billService = billService;
        }

        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, Bill bill)
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
                    var result = billService.Add(bill);
                    billService.SaveChanges();
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
                    var listBill = billService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, listBill);
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
                    var bill = billService.GetById(id);
                    SaveHistory("Get Bill by id" + id);
                    response = request.CreateResponse(HttpStatusCode.OK, bill);
                }
                return response;
            });
        }
    }
}