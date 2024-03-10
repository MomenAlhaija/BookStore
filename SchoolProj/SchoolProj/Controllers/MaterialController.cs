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
    public class MaterialController : Controller
    {
        MaterialService matreialService = new MaterialService();
        public ActionResult Index()
        {
            var materials=matreialService.GetMaterials();
            return View(materials);
        }
        public ActionResult EditMaterial(int materialId) 
        {
            var material=matreialService.GetMaterial(materialId);
            return View(material);  
        }
        [HttpPost]
        public ActionResult EditMaterial(MaterialModel material)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string materialNameEn=material.MaterialNameEn!=null?material.MaterialNameEn: "";

                    if (matreialService.GetMaterials().Any(m =>
                       ((string.Equals(m.MaterialNameAr.Trim().ToUpper(), material.MaterialNameAr.Trim().ToUpper()) ||
                       string.Equals(m.MaterialNameEn.Trim().ToUpper(), materialNameEn.Trim().ToUpper()))
                       && m.Id != material.Id)))
                    {
                        throw new Exception("The Material Is Already Found");
                    }
                    matreialService.UpdateMaterial(material);
                    return RedirectToAction(nameof(Index));
                }
                return View(material);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(material);
            }
        }
        public ActionResult AddMaterial()
        {
            var model = new MaterialModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMaterial(MaterialModel material)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string materialNameEn = material.MaterialNameEn != null ? material.MaterialNameEn : "";
                    if(matreialService.GetMaterials().Any(m =>
                       ((string.Equals(m.MaterialNameAr.Trim().ToUpper(), material.MaterialNameAr.Trim().ToUpper()) ||
                       string.Equals(m.MaterialNameEn.Trim().ToUpper(), materialNameEn.Trim().ToUpper())))))
                      {
                        throw new Exception("The Material Is Already Found");
                      }
                    matreialService.AddMaterial(material);
                    return RedirectToAction(nameof(Index));
                }
                return View(material);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(material);  
            }
        }
        [HttpPost] public void Delete(int materialId)=>matreialService.DeleteMaterial(materialId);
    }
}