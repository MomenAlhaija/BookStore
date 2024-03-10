using SchoolProj.DLL.Model;
using SchoolProj.DLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SchoolProj.Controllers
{
    public class TeacherController : Controller
    {
        MaterialService materialService=new MaterialService();
        TeacherService teacherService=new TeacherService();
        UserService userService=new UserService();  
        public ActionResult Index()
        {
            var teachers = teacherService.GetTeachers();
            return View(teachers);
        }
        public ActionResult AddTeacher()
        {
            var model = new TeacherModel();
            model.Materials=GetMaterialsAsSelectList();
            return View(model);

        }
        [HttpPost]
        public ActionResult AddTeacher(TeacherModel teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userService.GetUsers().Any(user => user.UserName == teacher.UserName))
                        throw new Exception("User Name Is Already Exist");
                    else if (teacherService.GetTeachers().Any(teacherItem => teacherItem.MaterialId == teacher.MaterialId))
                        throw new Exception("The Material Is Already Assigned Into Teacher");
                    teacherService.AddTeacher(teacher);
                    return RedirectToAction(nameof(Index));
                }
                teacher.Materials = GetMaterialsAsSelectList();
                return View(teacher);
            }
            catch (Exception ex)
            {
                teacher.Materials = GetMaterialsAsSelectList();
                ViewBag.ErrorMessage = ex.Message;
                return View(teacher);
            }
        }
        public ActionResult EditTeacher(int teacherId)
        {
            var teacher=teacherService.GetTeacherById(teacherId);
            teacher.Materials=GetMaterialsAsSelectList();
            return View(teacher);
        }
        [HttpPost]
        public ActionResult EditTeacher(TeacherModel teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userService.GetUsers().Any(user => user.UserName == teacher.UserName && user.UserId != teacher.UserId))
                        throw new Exception("User Name is Already Exist");
                    else if (teacherService.GetTeachers().Any(teacherItem => teacherItem.MaterialId == teacher.MaterialId && teacherItem.UserId!=teacher.UserId))
                        throw new Exception("The Material Is Already Assigned Into Teacher");
                    teacherService.UpdateTeacher(teacher);
                    return RedirectToAction(nameof(Index));
                }
                teacher.Materials = GetMaterialsAsSelectList();
                return View(teacher);   
            }
            catch (Exception ex)
            {
                teacher.Materials = GetMaterialsAsSelectList();
                ViewBag.ErrorMessage = ex.Message;
                return View(teacher);
            }
        }
        [HttpPost] public void Delete(int teacherId, int userId)=> teacherService.DeleteTeacher(teacherId, userId);
        #region Private Method
        private List<SelectListItem> GetMaterialsAsSelectList()
        {
            return materialService.GetMaterials().Select(p=>new SelectListItem
            {
                Text=p.MaterialNameAr.ToString(),
                Value=p.Id.ToString(),
            }).ToList();
        }
        #endregion

    }
}