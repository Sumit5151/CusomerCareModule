using CusomerCareModule.DAL;
using CusomerCareModule.Models;

namespace CusomerCareModule.BAL
{
    public class ManagerService : IManagerService
    {

        private readonly CustomerCareDbContext db;

        public ManagerService(CustomerCareDbContext _db)
        {
            this.db = _db;
        }


        public ManagerDashboardViewModel GetDashBoardData()
        {
            var ComplaintsStatus = db.Complaints.Select(c => c.StatusId).ToList();
            ManagerDashboardViewModel managerDashboardViewModel = new ManagerDashboardViewModel();
            managerDashboardViewModel.RegisteredComplaints = ComplaintsStatus.Count(x => x == 1);
            managerDashboardViewModel.ForwardedComplaints = ComplaintsStatus.Count(x => x == 2);
            managerDashboardViewModel.ResolvedByCCComplaints = ComplaintsStatus.Count(x => x == 3);
            managerDashboardViewModel.ResolvedByManagerComplaints = ComplaintsStatus.Count(x => x == 4);
            return managerDashboardViewModel;
        }

        public List<ComplaintViewModel> GetComplaints(int status)
        {
            var complaintsdto = db.Complaints.Where(x => x.StatusId == status).ToList();

            List<ComplaintViewModel> complaintViewModels = new List<ComplaintViewModel>();

            foreach (var complaint in complaintsdto)
            {
                ComplaintViewModel complaintViewModel = new ComplaintViewModel();
                complaintViewModel.Id = complaint.Id;
                complaintViewModel.Name = complaint.Name;
                complaintViewModel.Email = complaint.Email;
                complaintViewModels.Add(complaintViewModel);
            }

            return complaintViewModels;

        }
    }
}
