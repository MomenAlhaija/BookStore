using SchoolPro.shared.Consts;
using SchoolProj.DLL.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolProj.DLL.Model
{
    public class ClassModel
    {
        public int? Id { get; set; }
        [Required]
        [RegularExpression(CommonConst.FormatNameAr,ErrorMessage =CommonConst.ErrorMessageForFormatNameAr)]
        [StringLength(ClassConsts.MaxClassNameLength,MinimumLength = ClassConsts.MinClassNameLength)]
        public string ClassNameAr { get; set; }
        [RegularExpression(CommonConst.FormatNameEn, ErrorMessage = CommonConst.ErrorMessageForFormatNameEn)]
        [StringLength(ClassConsts.MaxClassNameLength, MinimumLength = ClassConsts.MinClassNameLength)]
        public string ClassNameEn { get; set; }
        public List<MaterialModel> Materials { get; set;}
        [Required]
        public List<int?> MaterialsIds { get; set; }
        public IEnumerable<SelectListItem> MaterialItems { get; set; }
    }
}
