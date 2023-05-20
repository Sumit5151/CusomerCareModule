using CusomerCareModule.DAL;
using CusomerCareModule.Models;

namespace CusomerCareModule.BAL
{
    public class CustomerCareService: ICustomerCareService
    {
        private readonly CustomerCareDbContext db;
        private readonly IHttpContextAccessor iHttpContextAccessor;
        public CustomerCareService(CustomerCareDbContext _db, IHttpContextAccessor _ihttpContextAccessor)
        {
            this.db = _db;
            this.iHttpContextAccessor = _ihttpContextAccessor;
        }   

        public string RegisterComplaint(ComplaintViewModel complaintViewModel)
        {
            Complaint complaint = new Complaint();
            complaint.Name = complaintViewModel.Name;
            complaint.Email = complaintViewModel.Email;
            complaint.MobileNumber = complaintViewModel.MobileNumber;   
            complaint.Description = complaintViewModel.DescriptionByCC;
            complaint.DateOfRegistration = DateTime.Now;
            complaint.ActionDate = DateTime.Now;
            complaint.StatusId = complaintViewModel.StatusId;
            complaint.UserId =  iHttpContextAccessor.HttpContext.Session.GetInt32("UserId");


            db.Complaints.Add(complaint);
            db.SaveChanges();


            ComplaintHistory complaintHistory = new ComplaintHistory();
            complaintHistory.ComplaintId = complaint.Id;
            complaintHistory.Description = complaintViewModel.DescriptionByCC; 
            complaintHistory.CurrentStatus = complaintViewModel.StatusId;
            complaintHistory.ActionDate = DateTime.Now;

            db.ComplaintHistories.Add(complaintHistory);
            db.SaveChanges();

            return "";
        }
    }
}
