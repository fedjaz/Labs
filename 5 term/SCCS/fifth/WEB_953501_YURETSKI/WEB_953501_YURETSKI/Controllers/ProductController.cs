using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using WEB_953501_YURETSKI.Data;
using WEB_953501_YURETSKI.Entities;
using WEB_953501_YURETSKI.Models;

namespace WEB_953501_YURETSKI.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext dbContext;
        const int itemsPerPage = 3;

        public ProductController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index(int pageNo = 1, string category = "all")
        {
            List<Food> foods;
            if(category == "all")
            {
                foods = dbContext.Foods.ToList();
            }
            else
            {
                foods = dbContext.Categories.FirstOrDefault(c => c.Name == category).Foods.ToList();
            }
            ListModelView<Food> page = ListModelView<Food>.CreatePage(foods, itemsPerPage, pageNo);

            return View(page);
        }

        string FileToBase64(string file)
        {
            Bitmap bitmap = new Bitmap(file);
            byte[] bytes = (byte[])new ImageConverter().ConvertTo(bitmap, typeof(byte[]));

            return Convert.ToBase64String(bytes);
        }
    }
}
