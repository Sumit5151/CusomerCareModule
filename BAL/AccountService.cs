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
            UserViewModel userViewModel = new UserViewModel();
            if (user != null)
            {
                userViewModel.Id = user.Id;
                userViewModel.Name = user.Name;
                userViewModel.Email = user.Email;
                userViewModel.RoleId = user.RoleId;
                userViewModel.Password = user.Password;
                
            }           
            return userViewModel;

        }
    }
}
