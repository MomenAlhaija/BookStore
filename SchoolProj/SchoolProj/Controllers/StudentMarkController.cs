using SchoolProj.DLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolProj.Controllers
{
    public class StudentMarkController : Controller
    {
        StudentMarksService studentMarksService=new StudentMarksService();
        public ActionResult Index()
        {
            try
            {
                int? userId = Convert.ToInt32(Session["userId"].ToString());
                if (userId.HasValue && userId.Value>0)
                {
                    var marks = studentMarksService.GetStudentMarks(userId.Value);
                    return View(marks);
                }
                throw new Exception("Not Found The Student");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage=ex.Message;
                return View();
            }
        }
    }
}