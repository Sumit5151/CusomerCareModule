namespace CusomerCareModule.Models
{
    public enum Roles
    {
        User =1,
        CustomerCareAssociate,
        Manager,
        Admin
    }



    public enum Status
    {
        Registered=1,
        Forwarded,
        ResolvedByCustomerCare,
        ResolvedByManager
    }
}
