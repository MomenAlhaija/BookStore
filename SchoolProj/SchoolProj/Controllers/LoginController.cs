using SchoolProj.DLL.Model;
using SchoolProj.DLL.Services;
using SchoolProj.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SchoolProj.Controllers
{
    public class LoginController : Controller
    {
        LoginService loginService=new LoginService();   
        UserService userService=new UserService();
        public ActionResult login()
        {
            var model=new LoginModel(); 
            return View(model);
        }
      
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            try
            {
                var user = loginService.Login(login);
                if (user != null)
                {
                    Session["userId"] = user.UserId;
                    switch(user.UserType)
                    {
                        case (int)(UserType.Admin):
                            return RedirectToAction("Index", "Admin");
                        case (int)(UserType.Teacher):
                            return RedirectToAction("Index", "ClassTeacher");
                        case (int)(UserType.Student):
                            return RedirectToAction("Index", "StudentMark");
                        default:
                            return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View(login);
        }
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            if (Session["userId"] != null)
            {
                model.UserType = userService.GetUserByID(int.Parse(Session["userId"].ToString())).UserType;
                model.UserId = int.Parse(Session["userId"].ToString());
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel passwordModel) 
        {
            try
            {
                if (Session["userId"] != null && ModelState.IsValid)
                {
                    var user = userService.GetUserByID(int.Parse(Session["userId"].ToString()));
                    if (user != null)
                    {

                        if (user.Password == passwordModel.OldPassword)
                        {
                            if (passwordModel.NewPassword == passwordModel.ConfirmNewPassword)
                            {
                                bool result = userService.ResetPassword(new ResetPasswordModel
                                {
                                    UserId = user.UserId,
                                    ConfirmNewPassword = passwordModel.NewPassword,
                                    NewPassword = passwordModel.NewPassword,
                                        
                                });
                                ViewBag.Message = result ? "Changed Password Correctly" : "Failed Change Password";
                            }
                            else
                            {
                                throw new Exception("The New Password and Confirm New Password Doesn't Match");
                            }
                        }
                        else
                        {
                            throw new Exception("The Old Password doesn't Correct");
                        }
                    }
                }
                return View(passwordModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(passwordModel);
            }
        }
    }
}