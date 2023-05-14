using CusomerCareModule.Models;

namespace CusomerCareModule.BAL
{
    public interface IAccountService
    {
        UserViewModel GetUser(LoginViewModel loginViewModel);
    }
}
