using System;
using System.Collections.Generic;
using System.Drawing;
using WEB_953501_YURETSKI.Data;
using WEB_953501_YURETSKI.Entities;
using System.IO;
using Microsoft.AspNetCore.Identity;
using WEB_953501_YURETSKI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_953501_YURETSKI.Tools
{
    public class DBInitializer
    {
        ApplicationDbContext dbContext;

        public UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DBInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task Initialize()
        {
            if(dbContext.Users.Count() > 0)
            {
                return;
            }

            IdentityRole admin = new IdentityRole("admin");
            IdentityRole user = new IdentityRole("user");

            await roleManager.CreateAsync(admin);
            await roleManager.CreateAsync(user);

            ApplicationUser adminUser = new ApplicationUser()
            {
                Email = "admin@admin.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@admin.com",
                Avatar = ""
            };   

            await userManager.CreateAsync(adminUser, "qwerty");
            await userManager.AddToRoleAsync(adminUser, "admin");

            ApplicationUser defaultUser = new ApplicationUser()
            {
                Email = "user@user.com",
                EmailConfirmed = true,
                FirstName = "User",
                LastName = "User",
                UserName = "user@user.com",
                Avatar = ""
            };

            await userManager.CreateAsync(defaultUser, "qwerty");
            await userManager.AddToRoleAsync(defaultUser, "user");


            Category pizza = new Category() { Name = "Пицца"};
            Category kebab = new Category() { Name = "Шаурма" };
            Category coffee = new Category() { Name = "Кофе" };
            Category pasta = new Category() { Name = "Макароны" };
            Category shake = new Category() { Name = "Коктейли" };

            List<Food> pizzas = new List<Food>()
            {
                new Food 
                {
                    Name = "Пицца сырная",
                    Category = pizza,
                    Description = "Пицца, в которой очень много сыра",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pizza\cheese.png")},
                    Price = 14.99f
                },
                new Food
                {
                    Name = "Пицца Diablo",
                    Category = pizza,
                    Description = "Очень острая пицца",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pizza\diablo.png")},
                    Price = 19.99f
                },
                new Food
                {
                    Name = "Пицца с ветчиной",
                    Category = pizza,
                    Description = "Вкусная пицца с ветчиной",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pizza\ham.png")},
                    Price = 19.99f
                },
                new Food
                {
                    Name = "Пицца гавайская",
                    Category = pizza,
                    Description = "Пицца с ананасами",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pizza\hawaii.png")},
                    Price = 24.99f
                },
                new Food
                {
                    Name = "Пицца маргарита",
                    Category = pizza,
                    Description = "Пицца с маргаритой",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pizza\margarita.png")},
                    Price = 17.99f
                },
                new Food
                {
                    Name = "Пицца пепперони",
                    Category = pizza,
                    Description = "Пицца пепперони)",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pizza\pepperoni.png")},
                    Price = 24.99f
                },
                new Food
                {
                    Name = "Пицца школьная",
                    Category = pizza,
                    Description = "Без комментариев",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pizza\school.png")},
                    Price = 2.49f
                },
            };

            List<Food> kebabs = new List<Food>()
            {
                new Food
                {
                    Name = "Шаурма с сыром",
                    Category = kebab,
                    Description = "Вкусная шаурма с сыром",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\kebab\cheese.png")},
                    Price = 7.99f
                },
                new Food
                {
                    Name = "Обычная шаурма",
                    Category = kebab,
                    Description = "Шаурма(нормальная)",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\kebab\default.png")},
                    Price = 6.99f
                },
                new Food
                {
                    Name = "Шаурма с грибами",
                    Category = kebab,
                    Description = "Шаурма с грибами(не очень вкусная, выглядит так себе и стоит дорого)",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\kebab\mush.png")},
                    Price = 9.99f
                },
            };

            List<Food> coffees = new List<Food>()
            {
                new Food
                {
                    Name = "Кофе эспрессо",
                    Category = coffee,
                    Description = "Черный как смола",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\coffee\espresso.png")},
                    Price = 3.99f
                },
                new Food
                {
                    Name = "Кофе латте",
                    Category = coffee,
                    Description = "Норм кофе",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\coffee\latte.png")},
                    Price = 7.99f
                },
            };

            List<Food> pastas = new List<Food>()
            {
                new Food
                {
                    Name = "Макароны с сыром",
                    Category = pasta,
                    Description = "Все понятно",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pasta\cheese.png")},
                    Price = 6.99f
                },
                new Food
                {
                    Name = "Паста карбонара",
                    Category = pasta,
                    Description = "Вкусно наверное",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pasta\carbonara.png")},
                    Price = 12.99f
                },
                new Food
                {
                    Name = "Лазанья",
                    Category = pasta,
                    Description = "Гарфилд",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pasta\lasagna.png")},
                    Price = 19.99f
                },
                new Food
                {
                    Name = "Спаггети с сыром",
                    Category = pasta,
                    Description = "Я неправильно написал название",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\pasta\spaghetti.png")},
                    Price = 9.99f
                },
            };

            List<Food> shakes = new List<Food>()
            {
                new Food
                {
                    Name = "Шоколадный коктейль",
                    Category = shake,
                    Description = "Черный",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\shake\choc.png")},
                    Price = 6.99f
                },
                new Food
                {
                    Name = "Молочный коктейль",
                    Category = shake,
                    Description = "Белый",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\shake\milk.png")},
                    Price = 7.99f
                },
                new Food
                {
                    Name = "Клубничный коктейль",
                    Category = shake,
                    Description = "Розовый",
                    Image = new Entities.Image() {Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\init\images\shake\straw.png")},
                    Price = 7.99f
                },
            };

            dbContext.Foods.AddRange(pizzas);
            dbContext.Foods.AddRange(pastas);
            dbContext.Foods.AddRange(kebabs);
            dbContext.Foods.AddRange(shakes);
            dbContext.Foods.AddRange(coffees);
            dbContext.SaveChanges();
        }
    }
}
