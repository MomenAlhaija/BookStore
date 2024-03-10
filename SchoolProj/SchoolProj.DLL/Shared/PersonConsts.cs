

namespace SchoolPro.shared.Consts
{
    public static class PersonConsts
    {
        public const int MaxFullNameLength = 50;
        public const int MinFullNameLength = 3;
        public const int MinAge = 5;
        public const int MaxAge = 100;
        public const int MaxUserNameLength = 20;
        public const int MinUserNameLength = 3;
        public const int MaxPasswordLength= 20;
        public const int MinPasswordLength = 6;
        public const string PasswordFormat = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*_\-]).{8,}$";
        public const string EmailFormat = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
        public const string ErrorEmailMessage = "he email address is not valid. Please enter a valid email address in the format example@example.com";
        public const string PasswordErrorMessage = "The password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character (!@#$%^&*).";
    }
}
