using Microsoft.Ajax.Utilities;
using SchoolProj.DLL.Model;
using SchoolProj.DLL.Services;
using SchoolProj.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolProj.Controllers
{
    public class UserController : Controller
    {
        UserService userService=new UserService();
        TeacherService teacherService=new TeacherService(); 
        StudentService studentService=new StudentService(); 
        public ActionResult Index()
        {
            var users=userService.GetUsers();
            return View(users);
        }
        public ActionResult EditUser(int userid)
        {
            try
            {
                var user = userService.GetUserByID(userid);
                if (user != null)
                {
                    return View(user);
                }
                throw new Exception("Not Found The User");
            }
            catch(Exception ex) {

                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public  ActionResult EditUser(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userService.GetUsers().Any(userItem => userItem.UserName == user.UserName && userItem.UserId != user.UserId))
                        throw new Exception("User Name is Already Exist");
                    bool isUpdtaed = userService.AddOrUpdateUser(user);
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(user);  
            }
        }
        public ActionResult AddUser()
        {
            var model = new UserModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddUser(UserModel user) {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userService.GetUsers().Any(userItem => userItem.UserName == user.UserName))
                        throw new Exception("User Name is Already Exist");
                    userService.AddOrUpdateUser(user);
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(user);
            }
        }
        [HttpPost]
        public void  Delete(int userId)
        {
            var user= userService.GetUserByID(userId);
            switch (user.UserType)
            {
                case (int)(UserType.Teacher):
                    var teacher=GetTeacherByUserId(userId);
                    if(teacher!=null)
                        teacherService.DeleteTeacher(teacher.TeacherId,userId);
                    else
                        userService.DeleteUser(userId);
                    break;
                case (int)(UserType.Student):
                    var student=GetStudentByUserId(userId);
                    if (student != null) 
                        studentService.DeleteStudent(student.StudentId.Value);
                    else
                        userService.DeleteUser(userId);
                    break;
                default:
                    userService.DeleteUser(userId);
                    break;
            }
        }
        public ActionResult ResetPassword(int userId)
        {
            var model = new ResetPasswordModel();
            model.UserId = userId;
            return View(model);
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel passwordModel)
        {
            try
            {
                ViewBag.Message = userService.ResetPassword(passwordModel) ? "Reset Password Successfully" : "Failed Reset Password";
            }
            catch(Exception ex)
            {
                ViewBag.Message=ex.Message;
            }
            return View(passwordModel);

        }
        private TeacherModel GetTeacherByUserId(int userId) => teacherService.GetTeacherByUserId(userId);
        private StudentModel GetStudentByUserId(int userId) => studentService.GetStudentByUserId(userId);

    }
}