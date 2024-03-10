using SchoolProj.DLL.Model;
using System;
using System.Linq;

namespace SchoolProj.DLL.Services
{
    public class LoginService
    {
        UserService userService = new UserService();
        public UserModel Login(LoginModel loginModel)
        {
            try
            {
                var users = userService.GetUsers();
                if (users.Any(p => p.UserName == loginModel.UserName && p.Password == loginModel.Password))
                {
                    var user = users.FirstOrDefault(p => p.UserName == loginModel.UserName);
                    return new UserModel
                    {
                        UserName = user.UserName,
                        UserType = user.UserType,
                        FullName = user.FullName,
                        UserId = user.UserId
                    };
                }
                throw new Exception("The UserName Or Password is Incorrect");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
     
       

    }
}
