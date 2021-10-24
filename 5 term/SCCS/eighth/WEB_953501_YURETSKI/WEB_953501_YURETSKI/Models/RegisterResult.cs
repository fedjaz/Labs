namespace WEB_953501_YURETSKI.Models
{
    public class RegisterResult
    {
        public string FirstName {  get; set; }
        public string FirstNameError { get; set; } = "";
        public string LastName { get; set; }
        public string LastNameError { get; set; } = "";
        public string Email { get; set; }
        public string EmailError { get; set; } = "";
        public string Password { get; set; }
        public string PasswordError { get; set; } = "";
        public string OldPasswordError { get; set; } = "";
        public string PasswordConfirm { get; set; }
    }
}
