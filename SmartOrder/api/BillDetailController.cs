using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/billdetail")]
    public class BillDetailController : ApiControllerBase
    {
        private IBillDetailService billDetailService;

        public BillDetailController(IErrorService errorService, IBillDetailService billDetailService) : base(errorService)
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
    }
}