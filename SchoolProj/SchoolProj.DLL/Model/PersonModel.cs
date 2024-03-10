using SchoolPro.shared.Consts;
using SchoolProj.DLL.Shared;
using System.ComponentModel.DataAnnotations;

namespace SchoolProj.DLL.Model
{
    public class PersonModel
    {

        [Required]
        [RegularExpression(CommonConst.FormatNameAr,ErrorMessage =CommonConst.ErrorMessageForFormatNameAr)]
        [StringLength(PersonConsts.MaxFullNameLength,MinimumLength =PersonConsts.MinFullNameLength)]
        public string FullNameAr { get; set; }
        [RegularExpression(CommonConst.FormatNameEn, ErrorMessage = CommonConst.ErrorMessageForFormatNameEn)]
        [StringLength(PersonConsts.MaxFullNameLength, MinimumLength = PersonConsts.MinFullNameLength)]

        public string FullNameEn { get; set; } = null;
        public int UserId { get; set; }
        [Required]
        public int Gender { get; set; }
        public int UserType { get; set; }
        [Required]
        [RegularExpression(PersonConsts.PasswordFormat, ErrorMessage = PersonConsts.PasswordErrorMessage)]
        [StringLength(PersonConsts.MaxPasswordLength,MinimumLength =PersonConsts.MinPasswordLength)]
        public string Password { get; set; }
        [Required]
        [RegularExpression(PersonConsts.EmailFormat, ErrorMessage = PersonConsts.ErrorEmailMessage)]
        [StringLength(PersonConsts.MaxUserNameLength, MinimumLength = PersonConsts.MinUserNameLength)]
        public string UserName { get; set; }

    }
}
