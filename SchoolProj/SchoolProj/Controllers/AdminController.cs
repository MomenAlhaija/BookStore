using SchoolProj.DLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolProj.Controllers
{
    public class AdminController : Controller
    {
        StaticsService staticsService=new StaticsService(); 
        UserService userService=new UserService();
        public ActionResult Index()
        {
            var statics= staticsService.GetStatics();
            if (Session["userId"]!=null)
            {
                int userId = Convert.ToInt32(Session["userId"]);
                var user = userService.GetUserByID(userId);
                statics.AdminName = user.FullName;
            }
            return View(statics);
        }
        public ActionResult Profile()
        {
            if (Session["userId"] != null)
            {
                int userId = Convert.ToInt32(Session["userId"]);
                var user = userService.GetUserByID(userId);
                return View(user);
            }
            return View();
        }
    }
}