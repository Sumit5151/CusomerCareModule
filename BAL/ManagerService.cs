﻿using CusomerCareModule.DAL;
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

            if (complaint != null)
            {
                complaint.Description = complaintViewModel.DescriptionByManager;
                complaint.ActionDate = DateTime.Now;
                complaint.UserId = iHttpContextAccessor.HttpContext.Session.GetInt32("UserId");
                complaint.StatusId = 4;
                db.Complaints.Update(complaint);
                db.SaveChanges();



                ComplaintHistory complaintHistory = new ComplaintHistory();
                complaintHistory.ComplaintId = complaintViewModel.Id;
                complaintHistory.CurrentStatus = 4;
                complaintHistory.Description = complaintViewModel.DescriptionByManager;
                complaintHistory.ActionDate = DateTime.Now;
                db.ComplaintHistories.Add(complaintHistory);
                db.SaveChanges();
            }
        }

    }
}
