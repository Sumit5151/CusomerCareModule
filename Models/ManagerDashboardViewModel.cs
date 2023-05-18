namespace CusomerCareModule.Models
{
    public class ManagerDashboardViewModel
    {
        public int RegisteredComplaints { get; set; }
        public int ForwardedComplaints { get; set; }
        public int ResolvedByCCComplaints { get; set; }
        public int ResolvedByManagerComplaints { get; set; }
    }
}
