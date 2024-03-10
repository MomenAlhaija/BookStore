using SchoolPro.shared.Consts;
using SchoolProj.DLL.Shared;
using System.ComponentModel.DataAnnotations;

namespace SchoolProj.DLL.Model
{
    public class MaterialModel
    {
        public int? Id { get; set; }
        [Required]
        [RegularExpression(CommonConst.FormatNameAr,ErrorMessage =CommonConst.ErrorMessageForFormatNameAr)]
        [StringLength(ClassConsts.MaxClassNameLength, MinimumLength = ClassConsts.MinClassNameLength)]

        public string MaterialNameAr { get; set; }
        [StringLength(ClassConsts.MaxClassNameLength, MinimumLength = ClassConsts.MinClassNameLength)]
        [RegularExpression(CommonConst.FormatNameEn, ErrorMessage = CommonConst.ErrorMessageForFormatNameEn)]
        public string MaterialNameEn { get; set; }
    }
}
