using CusomerCareModule.BAL;
using CusomerCareModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace CusomerCareModule.Controllers
{
    public class CustomerCareController : Controller
    {
        private readonly ICustomerCareService customerCareService;

        public CustomerCareController(ICustomerCareService _customerCareService)
        {
            this.customerCareService = _customerCareService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterComplaint()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterComplaint(ComplaintViewModel complaintViewModel)
        {
            if(ModelState.IsValid == true)
            {
                customerCareService.RegisterComplaint(complaintViewModel);
            }

            return View();
        }
    }
}
