using CusomerCareModule.BAL;
using Microsoft.AspNetCore.Mvc;

namespace CusomerCareModule.Controllers
{
    public class ManagerController : Controller
    {

        private readonly IManagerService managerService;

        public ManagerController(IManagerService _managerService)
        {
            this.managerService = _managerService;
        }


        public IActionResult Index()
        {
          var managerDashboardViewModel  = managerService.GetDashBoardData();
            return View(managerDashboardViewModel);
        }

        public IActionResult ComplaintList(int status)
        {
            var complaintViewModels = managerService.GetComplaints(status);
            return View(complaintViewModels);
        }



        
    }
}
