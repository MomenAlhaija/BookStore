namespace SchoolProj.DLL.Model
{
    public class ResetPasswordModel
    {
        public int? UserId { get; set; }    
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
