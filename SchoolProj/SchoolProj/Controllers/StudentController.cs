using Microsoft.Ajax.Utilities;
using SchoolProj.DLL.Model;
using SchoolProj.DLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolProj.Controllers
{
    public class StudentController : Controller
    {
        StudentService StudendtService = new StudentService();
        ClassService ClassService = new ClassService();
        UserService UserService = new UserService();    
        public ActionResult Index()
        {
            var students=StudendtService.GetStudents();
            return View(students);
        }
        public ActionResult AddStudent() 
        { 
            var student=new StudentModel();
            student.Classes = GetClassesAsSelectList();
            return View(student);
        }
        [HttpPost]
        public ActionResult AddStudent(StudentModel student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (UserService.GetUsers().Any(user => user.UserName == student.UserName))
                        throw new Exception("User Name is Already Exist");
                    StudendtService.AddStudent(student);
                    return RedirectToAction(nameof(Index));
                }
                student.Classes = GetClassesAsSelectList();
                return View(student);
            }
            catch (Exception ex)
            {
                student.Classes = GetClassesAsSelectList();
                ViewBag.ErrorMessage = ex.Message;
                return View(student);
            }
        }
        public ActionResult Edit(int studentId)
        {
            var student= StudendtService.GetStudentByID(studentId);
            student.Classes= GetClassesAsSelectList();
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(StudentModel student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (UserService.GetUsers().Any(user => user.UserName == student.UserName && user.UserId != student.UserId))
                        throw new Exception("User Name is Already Exist");
                    StudendtService.UpdateStudent(student);
                    return RedirectToAction(nameof(Index));
                }
                student.Classes = GetClassesAsSelectList();
                return View(student);
            }
            catch (Exception ex)
            {
                student.Classes = GetClassesAsSelectList();
                ViewBag.ErrorMessage = ex.Message;
                return View(student);
            }
        }
        [HttpPost] public void Delete(int studentId)=> StudendtService.DeleteStudent(studentId);
 
        #region Private Method
        private List<SelectListItem> GetClassesAsSelectList()
        {
            return ClassService.GetClasses().Select(p => new SelectListItem
            {
                Text = p.ClassNameAr.ToString(),
                Value = p.Id.ToString()
            }).ToList();
        }
        #endregion
    }
}