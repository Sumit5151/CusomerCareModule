using CusomerCareModule.Models;

namespace CusomerCareModule.BAL
{
    public interface IManagerService
    {
        ManagerDashboardViewModel GetDashBoardData();
        List<ComplaintViewModel> GetComplaints(int status);
    }
}
