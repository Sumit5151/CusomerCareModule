using Microsoft.AspNetCore.Mvc;

namespace CusomerCareModule.Controllers
{
    public class CustomerCareController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
