using CusomerCareModule.DAL;

namespace CusomerCareModule.Models
{
    public class ComplaintViewModel
    {

        public ComplaintViewModel()
        {
            ComplaintHistories = new List<ComplaintHistory>();
            Users = new List<User>();
        }
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? MobileNumber { get; set; }

        public string? DescriptionByCC { get; set; }
        public string? DescriptionByManager { get; set; }

        public int? StatusId { get; set; }

        public DateTime? DateOfRegistration { get; set; }

        public DateTime? ActionDate { get; set; }

        public int? UserId { get; set; }

        public List<ComplaintHistory> ComplaintHistories { get; set; }
        public List<User> Users { get; set; }

    }
}
