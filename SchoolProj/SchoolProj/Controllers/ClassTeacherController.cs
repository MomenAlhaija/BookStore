using SchoolPro.shared.Consts;
using SchoolProj.DLL.Model;
using SchoolProj.DLL.Services;
using SchoolProj.DLL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolProj.Controllers
{
    public class ClassTeacherController : Controller
    {
        ClassTeacherService classTeacher=new ClassTeacherService();
        MarkService markService = new MarkService();    
        public ActionResult Index()
        {
            try
            {
                int? userId = Convert.ToInt32(Session["userId"].ToString());
                if (userId != 0 & userId != null)
                {
                    var classes = classTeacher.GetClassesForTeacher(userId.Value);
                    return View(classes);
                }
                throw new Exception("Not Found The Teacher");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage=ex.Message;
                return View();
            }
        }
        public ActionResult StudentsInClass(int classId,int materialId)
        {
            var students = classTeacher.GetStudentsinClass(classId);
            ViewBag.MaterialId= materialId;
            foreach (var student in students)
            {
                student.Grade = markService.GetMarkInMaterialForStudent(new GetMarkInMaterialModel
                {
                    StudentId = student.StudentId,
                    MaterialId =materialId,
                    ClassId = student.ClassId,
                });
            }
            return View(students);
        }
        public void UpdateGrade(MarkModel mark)
        {
            if (mark.Grade >= (decimal)GradeConst.MinGrade && mark.Grade <= (decimal)(GradeConst.MaxGrade))
                markService.AddOrUpdateMark(mark);
            else
                throw new Exception($"The Mark Must Between {GradeConst.MinGrade} and {GradeConst.MaxGrade}");
        }

    }
}