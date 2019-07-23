using SmartOrder.Models;
using System.Web.Mvc;
namespace SmartOrder.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel register )
        {
            return View();
        }
    }
}