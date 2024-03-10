using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolProj.DLL.Model
{
    public class TeacherModel:PersonModel
    {
        public int TeacherId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }

        public List<SelectListItem> Materials { get; set; } = new List<SelectListItem>();

    }
}
