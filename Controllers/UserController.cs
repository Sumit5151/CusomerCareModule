using Microsoft.AspNetCore.Mvc;

namespace CusomerCareModule.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
