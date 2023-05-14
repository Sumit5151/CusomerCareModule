using CusomerCareModule.DAL;
using CusomerCareModule.Models;

namespace CusomerCareModule.BAL
{
    public class AccountService : IAccountService
    {

        private readonly CustomerCareDbContext db;

        public AccountService(CustomerCareDbContext _db)
        {
            this.db = _db;
        }


        public UserViewModel GetUser(LoginViewModel loginViewModel)
        {
            

            var user = db.Users.FirstOrDefault(user => user.Email == loginViewModel.Email
                                     && user.Password == loginViewModel.Password);
            var userViewModel = new UserViewModel();
            if (user != null)
            {
                userViewModel.Name = user.Name;
                userViewModel.Email = user.Email;
                userViewModel.RoleId = user.RoleId;
            }           
            return userViewModel;

        }
    }
}
