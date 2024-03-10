
namespace SchoolProj.DLL.Shared
{
    public class CommonConst
    {
        public const string FormatNameAr = @"^[\u0600-\u06FF0-9\s\p{P}]+$";
        public const string ErrorMessageForFormatNameAr = "Input must contain only Arabic characters  and numbers";
        public const string FormatNameEn = @"^[A-Za-z\d\s]+$";
        public const string ErrorMessageForFormatNameEn = "Input must contain only English characters and numbers";
    }
}
