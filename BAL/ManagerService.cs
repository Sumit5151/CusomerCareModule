using CusomerCareModule.DAL;
using CusomerCareModule.Models;

namespace CusomerCareModule.BAL
{
    public class ManagerService : IManagerService
    {

        private readonly CustomerCareDbContext db;
        private readonly IHttpContextAccessor iHttpContextAccessor;

        public ManagerService(CustomerCareDbContext _db, IHttpContextAccessor _ihttpContextAccessor)
        {
            this.db = _db;
            this.iHttpContextAccessor = _ihttpContextAccessor;
        }


        public DashboardViewModel GetDashBoardData()
        {
            var ComplaintsStatus = db.Complaints.Select(c => c.StatusId).ToList();
            DashboardViewModel managerDashboardViewModel = new DashboardViewModel();
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




        public ComplaintViewModel GetComplaint(int complaintId)
        {
            var complaintdto = db.Complaints.FirstOrDefault(x => x.Id == complaintId);
            ComplaintViewModel complaintViewModel = new ComplaintViewModel();

            if (complaintdto != null)
            {
                complaintViewModel.Id = complaintdto.Id;
                complaintViewModel.Name = complaintdto.Name;
                complaintViewModel.Email = complaintdto.Email;
                complaintViewModel.MobileNumber = complaintdto.MobileNumber;
                complaintViewModel.DescriptionByCC = complaintdto.Description;
                complaintViewModel.StatusId = complaintdto.StatusId;
            }
            return complaintViewModel;
        }


        public void UpdateComplaint(ComplaintViewModel complaintViewModel)
        {
            var complaint = db.Complaints.FirstOrDefault(x => x.Id == complaintViewModel.Id);
            ComplaintHistory complaintHistory = new ComplaintHistory();

            if (complaint != null)
            {
                var roleId = iHttpContextAccessor.HttpContext.Session.GetInt32("RoleId");
                var userId = iHttpContextAccessor.HttpContext.Session.GetInt32("UserId");

                if (roleId != null && roleId == 3)
                {
                    complaint.Description = complaintViewModel.DescriptionByManager;
                    complaint.StatusId = 4;
                    //Complaint History
                    complaintHistory.Description = complaintViewModel.DescriptionByManager;
                    complaintHistory.CurrentStatus = 4;
                }

                else if (roleId != null && roleId == 2)
                {
                    complaint.Description = complaintViewModel.DescriptionByCC;
                    complaint.StatusId = complaintViewModel.StatusId;

                    complaintHistory.Description = complaintViewModel.DescriptionByCC;
                    complaintHistory.CurrentStatus = complaintViewModel.StatusId;
                }

                complaint.ActionDate = DateTime.Now;
                complaint.UserId = userId;
                db.Complaints.Update(complaint);
                db.SaveChanges();

                complaintHistory.ComplaintId = complaintViewModel.Id;
                complaintHistory.ActionDate = DateTime.Now;
                complaintHistory.UserId = userId;
                db.ComplaintHistories.Add(complaintHistory);
                db.SaveChanges();
            }
        }

    }
}
