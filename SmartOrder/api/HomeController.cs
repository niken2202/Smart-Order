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
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        IErrorService errorService;
        public HomeController(IErrorService errorService, IHistoryService historyService) : base(errorService,historyService)
        {
            this.errorService = errorService;
        }
        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Dang nhap thanh cong";
        }

    }
}
