namespace WEB_953501_YURETSKI.Models
{
    public class LoginResult
    {
        public string Email {  get; set; }
        public string EmailError { get; set; } = "";
        public string PasswordError { get; set; } = "";
    }
}
