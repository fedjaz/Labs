using Microsoft.AspNetCore.Identity;

namespace WEB_953501_YURETSKI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string EmailConfirmationCode {  get; set; }
    }
}
