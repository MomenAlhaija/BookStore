using SchoolPro.shared.Consts;
using System.ComponentModel.DataAnnotations;

namespace SchoolProj.DLL.Model
{
    public class ChangePasswordModel:ResetPasswordModel
    {
        [StringLength(PersonConsts.MaxPasswordLength, MinimumLength = PersonConsts.MinPasswordLength)]
        [RegularExpression(PersonConsts.PasswordFormat, ErrorMessage = PersonConsts.PasswordErrorMessage)]
        public string OldPassword { get; set; }
        public int? UserType { get; set; }   
    }
}
