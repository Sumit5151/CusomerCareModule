using Microsoft.AspNetCore.Mvc;

namespace CusomerCareModule.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
