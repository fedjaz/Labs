using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEB_953501_YURETSKI.Data;
using WEB_953501_YURETSKI.Entities;
using WEB_953501_YURETSKI.Models;
using WEB_953501_YURETSKI.Extensions;

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

        public IActionResult GetPager(int pageNo, int pages, string category)
        {
            PagerData pagerData = new PagerData() { Current = pageNo, Pages = pages, Category = category };
            return PartialView("_PagerPartial", pagerData);
        }

        public IActionResult Index(int pageNo = 1, string category = "Все")
        {
            List<Food> foods;
            if(category == "Все")
            {
                foods = dbContext.Foods.ToList();
            }
            else
            {
                Category cat = dbContext.Categories.FirstOrDefault(c => c.Name == category);
                foods = dbContext.Foods.Where(f => f.CategoryId == cat.Id).ToList();
            }
            ListModelView<Food> page = ListModelView<Food>.CreatePage(foods, itemsPerPage, pageNo);

            foreach(Food food in page)
            {
                food.Category = dbContext.Categories.FirstOrDefault(c => c.Id == food.CategoryId);
            }

            ViewData["Category"] = category;
            ViewData["Categories"] = GetCategories();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", page);
            }
            else
            {
                return View(page);
            }
        }

        public IActionResult GetImage(int imageId)
        {
            string base64Image = dbContext.Images.FirstOrDefault(i => i.Id == imageId).Base64Image;
            return File(new MemoryStream(Convert.FromBase64String(base64Image)), "image/png");
        }

        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            foreach(Category category in dbContext.Categories)
            {
                categories.Add(category.Name);
            }
            return categories;
        }

        string FileToBase64(string file)
        {
            Bitmap bitmap = new Bitmap(file);
            byte[] bytes = (byte[])new ImageConverter().ConvertTo(bitmap, typeof(byte[]));

            return Convert.ToBase64String(bytes);
        }
    }
}
