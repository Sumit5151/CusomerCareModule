using CusomerCareModule.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CusomerCareModule.Controllers
{
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }
        
    }
}