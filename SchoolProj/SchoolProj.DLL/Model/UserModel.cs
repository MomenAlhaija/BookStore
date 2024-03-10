using SchoolPro.shared.Consts;
using System.ComponentModel.DataAnnotations;

namespace SchoolProj.DLL.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(PersonConsts.MaxFullNameLength,MinimumLength =PersonConsts.MinFullNameLength)]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(PersonConsts.EmailFormat,ErrorMessage =PersonConsts.ErrorEmailMessage)]
        [StringLength(PersonConsts.MaxUserNameLength, MinimumLength = PersonConsts.MinUserNameLength)]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(PersonConsts.PasswordFormat,ErrorMessage =PersonConsts.PasswordErrorMessage)]
        [StringLength(PersonConsts.MaxPasswordLength, MinimumLength = PersonConsts.MinPasswordLength)]
        public string Password { get; set; }
        [Required]
        public int UserType { get; set; }
    }
}
