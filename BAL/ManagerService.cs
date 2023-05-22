using CusomerCareModule.DAL;
using CusomerCareModule.Models;
using Microsoft.EntityFrameworkCore;

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
            managerDashboardViewModel.RegisteredComplaints = ComplaintsStatus.Count(x => x == Convert.ToInt32(Status.Registered));
            managerDashboardViewModel.ForwardedComplaints = ComplaintsStatus.Count(x => x == Convert.ToInt32(Status.Forwarded));
            managerDashboardViewModel.ResolvedByCCComplaints = ComplaintsStatus.Count(x => x == Convert.ToInt32(Status.ResolvedByCustomerCare));
            managerDashboardViewModel.ResolvedByManagerComplaints = ComplaintsStatus.Count(x => x == Convert.ToInt32(Status.ResolvedByManager));
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
            var complaintdto = db.Complaints
                                 .Include(x => x.ComplaintHistories)
                                 .ThenInclude(x=>x.User)
                                 .FirstOrDefault(x => x.Id == complaintId);

            ComplaintViewModel complaintViewModel = new ComplaintViewModel();

            if (complaintdto != null)
            {
                complaintViewModel.Id = complaintdto.Id;
                complaintViewModel.Name = complaintdto.Name;
                complaintViewModel.Email = complaintdto.Email;
                complaintViewModel.MobileNumber = complaintdto.MobileNumber;
                complaintViewModel.DescriptionByCC = complaintdto.Description;
                complaintViewModel.StatusId = complaintdto.StatusId;


                //Converted history table
                if (complaintdto.ComplaintHistories != null && complaintdto.ComplaintHistories.Count() > 0)
                {
                    foreach (var history in complaintdto.ComplaintHistories)
                    {
                        ComplaintHistory complaintHistory = new ComplaintHistory();
                        complaintHistory.Id= history.Id;
                        complaintHistory.ComplaintId= history.ComplaintId;
                        complaintHistory.Description= history.Description;
                        complaintHistory.ActionDate= history.ActionDate;
                        complaintHistory.CurrentStatus = history.CurrentStatus;
                        complaintHistory.UserId= history.UserId;

                        complaintViewModel.ComplaintHistories.Add(complaintHistory);
                    }
                }

                //convert user table

                //if (complaintdto.u != null && complaintdto.ComplaintHistories.Count() > 0)
                //{
                //    foreach (var history in complaintdto.ComplaintHistories)
                //    {
                //        ComplaintHistory complaintHistory = new ComplaintHistory();
                //        complaintHistory.Id = history.Id;
                //        complaintHistory.ComplaintId = history.ComplaintId;
                //        complaintHistory.Description = history.Description;
                //        complaintHistory.ActionDate = history.ActionDate;
                //        complaintHistory.CurrentStatus = history.CurrentStatus;
                //        complaintHistory.UserId = history.UserId;

                //        complaintViewModel.ComplaintHistories.Add(complaintHistory);
                //    }
                //}

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

                if (roleId != null && roleId == Convert.ToInt32(Roles.Manager))
                {
                    complaint.Description = complaintViewModel.DescriptionByManager;
                    complaint.StatusId = Convert.ToInt32(Status.ResolvedByManager);
                    //Complaint History
                    complaintHistory.Description = complaintViewModel.DescriptionByManager;
                    complaintHistory.CurrentStatus = Convert.ToInt32(Status.ResolvedByManager);
                }

                else if (roleId != null && roleId == Convert.ToInt32(Roles.CustomerCareAssociate))
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
