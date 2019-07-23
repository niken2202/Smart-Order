using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/billdetail"), Authorize]
    public class BillDetailController : ApiControllerBase
    {
        private IBillDetailService billDetailService;

        public BillDetailController(IErrorService errorService, IBillDetailService billDetailService,IHistoryService historyService) : base(errorService, historyService)
        {
            this.billDetailService = billDetailService;
        }
        [Route("getbybillid")]
        public HttpResponseMessage Get(HttpRequestMessage request, int billId)
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
                    var listDishOfBill = billDetailService.GetByBillId(billId);
                    response = request.CreateResponse(HttpStatusCode.OK, listDishOfBill);
                }
                return response;
            });
        }

        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, BillDetail billdetail)
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
                    var result = billDetailService.Add(billdetail);
                    billDetailService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                return response;
            });
        }
    }
}