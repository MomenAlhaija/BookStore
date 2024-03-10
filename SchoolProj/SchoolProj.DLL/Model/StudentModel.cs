using SchoolPro.shared.Consts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolProj.DLL.Model
{
    public class StudentModel:PersonModel
    {
        public int? StudentId { get; set; }
        [Required]
        [Range(PersonConsts.MinAge,PersonConsts.MaxAge)]
        public int? Age { get; set; }
        [Required]
        public int? ClassId { get; set; }
        public string ClassName { get; set; }
        public List<SelectListItem> Classes { get; set; }
    }
}
