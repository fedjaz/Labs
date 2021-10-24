using Microsoft.AspNetCore.Http;

namespace WEB_953501_YURETSKI.Models
{
    public class RegisterViewModel
    {
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
        public string Email {  get; set; }
        public string Password {  get; set; }
        public string PasswordConfirm {  get; set; }
        public IFormFile Avatar {  get; set; }
    }
}
