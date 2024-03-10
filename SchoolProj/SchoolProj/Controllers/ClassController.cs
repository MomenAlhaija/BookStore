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
    public class ClassController : Controller
    {
        ClassService classService=new ClassService();
        MaterialService matrialService=new MaterialService();
        public ActionResult Index()
        {
            var classes=classService.GetClasses();
            return View(classes);
        }
        public ActionResult Edit(int? classId) 
        {
            try
            {
                if (classId is null)
                    throw new Exception("The Class Id Can't By Null");
                var classFromDb = classService.GetClassById(classId.Value);
                classFromDb.MaterialItems = GetMaterialAsSelectList();
                classFromDb.MaterialsIds = classFromDb.Materials.Select(p => p.Id).ToList();
                return View(classFromDb);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(ClassModel classToEdit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (classService.GetClasses().Any(p => (p.ClassNameAr.Trim().ToUpper() == classToEdit.ClassNameAr.Trim().ToUpper()
                        ||(classToEdit.ClassNameEn!=null&& p.ClassNameEn.Trim().ToUpper() == classToEdit.ClassNameEn.Trim().ToUpper())) 
                        && p.Id != classToEdit.Id))
                        throw new Exception("The Class Is Already Found");
                    classService.UpdateClassMaterial(classToEdit);
                    return RedirectToAction(nameof(Index));
                }
                classToEdit.MaterialItems = GetMaterialAsSelectList();
                return View(classToEdit);
            }
            catch (Exception ex)
            {
                classToEdit.MaterialItems = GetMaterialAsSelectList();
                ViewBag.ErrorMessage = ex.Message;
                return View(classToEdit);
            }
        }
        public ActionResult AddClass()
        {
            var model=new ClassModel();
            model.MaterialItems = GetMaterialAsSelectList();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddClass(ClassModel classToAdd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (classService.GetClasses().Any(p => p.ClassNameAr. Trim().ToUpper() == classToAdd.ClassNameAr.Trim().ToUpper()
                                                          ||(classToAdd.ClassNameEn!=null&& p.ClassNameEn.Trim().ToUpper() == classToAdd.ClassNameEn.Trim().ToUpper())))
                         throw new Exception("The Class Is Already Found");
                    classService.AddClass(classToAdd);
                    return RedirectToAction(nameof(Index));
                }
                classToAdd.MaterialItems = GetMaterialAsSelectList();
                return View(classToAdd);
            } 
            catch (Exception ex)
            {
                classToAdd.MaterialItems = GetMaterialAsSelectList();
                ViewBag.ErrorMessage=ex.Message;
                return View(classToAdd);
            }
        }
        [HttpPost] public void Delete(int classId)=>classService.DeleteClass(classId);
        #region private Method
        private IEnumerable<SelectListItem> GetMaterialAsSelectList()
        {
            return matrialService.GetMaterials().Select(p => new SelectListItem
            {
                Text = p.MaterialNameAr.ToString(),
                Value = p.Id.ToString()
            });
        }

        #endregion
    }
}