using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using WEB_953501_YURETSKI.Models;

namespace WEB_953501_YURETSKI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private Services.EmailConfimation emailConfimation;
        private IWebHostEnvironment environment;
        private RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Services.EmailConfimation emailConfimation, IWebHostEnvironment environment, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailConfimation = emailConfimation;
            this.environment = environment;
            this.roleManager = roleManager;

        }

        public async Task<IActionResult> Index()
        {

            if (!signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login");
            }

            ApplicationUser user = await userManager.FindByEmailAsync(User.Identity.Name);
            RegisterResult result = new RegisterResult()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile Avatar)
        {
            string base64Avatar = Tools.ImageConverter.ImageToBase64(Avatar, true);
            if(base64Avatar == "" || !signInManager.IsSignedIn(User))
            {
                return await Index();
            }

            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name); 
            user.Avatar = base64Avatar;
            await userManager.UpdateAsync(user);
            return await Index();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string OldPassword, string Password, string PasswordConfirm)
        {
            if (!signInManager.IsSignedIn(User))
            {
                return await Index();
            }

            ApplicationUser user = await userManager.FindByEmailAsync(User.Identity.Name);
            RegisterResult result = new RegisterResult()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            bool hasErrors = false;
            if(OldPassword == null)
            {
                hasErrors = true;
                result.OldPasswordError = "Введите старый пароль";
            }
            if(Password == null)
            {
                hasErrors = true;
                result.PasswordError = "Введите новый пароль";
            }
            else if(Password != PasswordConfirm)
            {
                hasErrors = true;
                result.PasswordError = "Пароли не совпадают";
            }

            if(OldPassword != null && !await userManager.CheckPasswordAsync(user, OldPassword))
            {
                hasErrors = true;
                result.OldPasswordError = "Вы ввели неверный пароль";
            }

            if (!hasErrors)
            {
                await userManager.ChangePasswordAsync(user, OldPassword, Password);
                return View();
            }
            else
            {
                return View("Index", result);
            }
            
        }

        public IActionResult Login()
        {
            return View(new LoginResult());
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            LoginResult loginResult = new LoginResult()
            {
                Email = Email
            };

            bool hasErrors = false;
            if (Email == null)
            {
                hasErrors = true;
                loginResult.EmailError = "Введите email";
            }

            if(Password == null)
            {
                hasErrors = true;
                loginResult.PasswordError = "Введите пароль";
            }
            if (hasErrors)
            {
                return View(loginResult);
            }

            ApplicationUser user = await userManager.FindByEmailAsync(Email);

            if(user == null || !await userManager.CheckPasswordAsync(user, Password))
            {
                loginResult.Email = Email;
                loginResult.PasswordError = "Проверьте email или пароль";
                return View(loginResult);
            }

            if (!user.EmailConfirmed)
            {
                loginResult.Email = Email;
                loginResult.EmailError = "Email не подтвержден";
                return View(loginResult);
            }

            else
            {
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Register()
        {
            return View(new RegisterResult());
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetAvatar()
        {
            FileStreamResult defaultAvatar = File(environment.WebRootFileProvider.GetFileInfo("/images/defaultAvatar.png").CreateReadStream(), "image/png");
            if (signInManager.IsSignedIn(User))
            {
                ApplicationUser user = await userManager.FindByEmailAsync(User.Identity.Name);

                string base64Avatar = user.Avatar;
                if(base64Avatar == "")
                {
                    return defaultAvatar;
                }
                return File(Tools.ImageConverter.Base64ToImage(base64Avatar), "image/png");
            }
            else
            {
                return defaultAvatar;
            }
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            bool hasErrors = false;
            RegisterResult registerResult = new RegisterResult
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PasswordConfirm = model.PasswordConfirm,
            };

            if(model.FirstName == null)
            {
                hasErrors = true;
                registerResult.FirstNameError = "Введите ваше имя";
            } 
            else if(model.FirstName.Contains(" "))
            {
                hasErrors = true;
                registerResult.FirstNameError = "В имени не должно быть пробелов";
            }

            if (model.LastName == null)
            {
                hasErrors = true;
                registerResult.LastNameError = "Введите вашу фамилию";
            }
            else if (model.LastName.Contains(" "))
            {
                hasErrors = true;
                registerResult.LastNameError = "В фамилии не должно быть пробелов";
            }

            if(await userManager.FindByEmailAsync(model.Email) != null)
            {
                hasErrors = true;
                registerResult.EmailError = "Пользователь с таким email уже зарегистрирован";
            }

            if(model.Password == null)
            {
                hasErrors = true;
                registerResult.PasswordError = "Введите пароль";
            }
            else if(model.Password != model.PasswordConfirm)
            {
                hasErrors = true;
                registerResult.PasswordError = "Пароли не совпадают";
            }   

            if (hasErrors)
            {
                return View(registerResult);
            }
            else
            {
                string base64avatar = Tools.ImageConverter.ImageToBase64(model.Avatar, true);

                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    Avatar = base64avatar
                };

                user.EmailConfirmationCode = emailConfimation.BeginConfirmation(model.Email);
                await userManager.CreateAsync(user, model.Password);
                await userManager.AddToRoleAsync(user, "user");
                return View("ConfirmMessage", model.Email);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Confirm(string email, string code)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return View((object)"Пользователь не найден.");
            }

            if (user.EmailConfirmed)
            {
                return View((object)"Email уже подтвержден.");
            }

            if(user.EmailConfirmationCode != code)
            {
                return View((object)"Код подтверждения не совпадает.");
            }

            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);

            return View((object)"Email подтвержден");
        }
    }
}
