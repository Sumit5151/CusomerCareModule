using CusomerCareModule.DAL;
using CusomerCareModule.Models;

namespace CusomerCareModule.BAL
{
    public class CustomerCareService: ICustomerCareService
    {
        private readonly CustomerCareDbContext db;
        public CustomerCareService(CustomerCareDbContext _db )
        {
            this.db = _db;
        }   

        public string RegisterComplaint(ComplaintViewModel complaintViewModel)
        {
            Complaint complaint = new Complaint();
            complaint.Name = complaintViewModel.Name;
            complaint.Email = complaintViewModel.Email;
            complaint.MobileNumber = complaintViewModel.MobileNumber;   
            complaint.Description = complaintViewModel.Description;
            complaint.DateOfRegistration = DateTime.Now;
            complaint.ActionDate = DateTime.Now;
            complaint.StatusId = 1;

            db.Complaints.Add(complaint);
            db.SaveChanges();


            return "";
        }
    }
}
