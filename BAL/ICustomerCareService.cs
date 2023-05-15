using CusomerCareModule.Models;

namespace CusomerCareModule.BAL
{
    public interface ICustomerCareService
    {
        string RegisterComplaint(ComplaintViewModel complaintViewModel);
    }
}
