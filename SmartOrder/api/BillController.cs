using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;

namespace SmartOrder.api
{
    public class BillController : ApiControllerBase
    {
        private IBillService billService;

        public BillController(IErrorService errorService, IBillService billService) : base(errorService)
        {
            this.billService = billService;
        }

        public HttpResponseMessage Create(HttpRequestMessage request, Bill bill)
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
                    var result = billService.Add(bill);
                    billService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                return response;
            });
        }
    }
}