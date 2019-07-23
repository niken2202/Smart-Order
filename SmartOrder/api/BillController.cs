using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/bill"), Authorize]
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

        [HttpGet]
        [Route("gettimerange")]
        public HttpResponseMessage GetTimeRange(HttpRequestMessage request, DateTime fromDate, DateTime toDate)
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
                    var listBill = billService.GetTimeRange(fromDate, toDate);
                    response = request.CreateResponse(HttpStatusCode.OK, listBill);
                }
                return response;
            });
        }

        [Route("getlast7day"), HttpGet]
        public HttpResponseMessage GetBillLast7Days(HttpRequestMessage request)
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
                    var listBill = billService.GetBillLast7Days();
                    response = request.CreateResponse(HttpStatusCode.OK, listBill);
                }
                return response;
            });
        }

        [Route("getlastmonth"), HttpGet]
        public HttpResponseMessage GetLastMonth(HttpRequestMessage request)
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
                    var listBill = billService.GetBillLastMonth();
                    response = request.CreateResponse(HttpStatusCode.OK, listBill);
                }
                return response;
            });
        }

        [Route("gettoday"), HttpGet]
        public HttpResponseMessage GetBillToday(HttpRequestMessage request)
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
                    var listBill = billService.GetBillToday();
                    response = request.CreateResponse(HttpStatusCode.OK, listBill);
                }
                return response;
            });
        }

        [Route("getbilldetail"), HttpGet, Authorize]
        public HttpResponseMessage GetBillDetail(HttpRequestMessage request,int id)
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
                    var bill = billService.GetBillDetail(id);
                    response = request.CreateResponse(HttpStatusCode.OK, bill);
                }
                return response;
            });
        }

        [Route("getrevenue"), HttpGet, Authorize]
        public HttpResponseMessage GetRevenue(HttpRequestMessage request, DateTime fromDate, DateTime toDate)
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
                    var revenue = billService.GetRevenueStatistic(fromDate, toDate);
                    response = request.CreateResponse(HttpStatusCode.OK, revenue);
                }
                return response;
            });
        }
    }
}