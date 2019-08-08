using Service;
using SmartOrder.Infrastructure;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        private IErrorService errorService;

        public HomeController(IErrorService errorService, IHistoryService historyService) : base(errorService, historyService)
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