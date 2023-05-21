using CusomerCareModule.Models;

namespace CusomerCareModule.BAL
{
    public interface IManagerService
    {
        DashboardViewModel GetDashBoardData();
        List<ComplaintViewModel> GetComplaints(int status);
        ComplaintViewModel GetComplaint(int complaintId);
        void UpdateComplaint(ComplaintViewModel complaintViewModel);
    }
}
