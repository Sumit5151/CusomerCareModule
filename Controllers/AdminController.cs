using Microsoft.AspNetCore.Mvc;

namespace CusomerCareModule.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
