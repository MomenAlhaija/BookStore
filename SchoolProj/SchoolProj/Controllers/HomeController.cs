using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SchoolProj.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void ChangeLanguage(string language) { 
        
            if(language!=null)
            {
                Thread.CurrentThread.CurrentCulture=CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture=new CultureInfo(language);
                var cookie = new HttpCookie("Language");
                cookie.Value= language;
                Response.Cookies.Add(cookie);   
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Blog()
        {

            return View();
        }
        public ActionResult Contact()
        {

            return View();
        }
    }
}