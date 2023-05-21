using CusomerCareModule.BAL;
using CusomerCareModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace CusomerCareModule.Controllers
{
    public class CustomerCareController : Controller
    {
        private readonly ICustomerCareService customerCareService;
        private readonly IManagerService managerService;

        public CustomerCareController(ICustomerCareService _customerCareService, IManagerService _managerService)
        {
            this.customerCareService = _customerCareService;
            this.managerService = _managerService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterComplaint()
        {
            @ViewBag.Heading = "Register Complaint";
            ComplaintViewModel complaintViewModel = new ComplaintViewModel();
            return View(complaintViewModel);
        }

        [HttpPost]
        public IActionResult RegisterComplaint(ComplaintViewModel complaintViewModel)
        {

            if (ModelState.IsValid == true)
            {
                var roleId = HttpContext.Session.GetInt32("RoleId");
                var compaintId = HttpContext.Session.GetInt32("complaintId");
                if (compaintId != null)
                {
                    complaintViewModel.Id = compaintId.Value;
                    managerService.UpdateComplaint(complaintViewModel);
                }
                else
                {
                    customerCareService.RegisterComplaint(complaintViewModel);
                }
            }



            @ViewBag.Heading = "Register Complaint";
            return View();
        }
    }
}
