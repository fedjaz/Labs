using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_953501_YURETSKI.Data;
using WEB_953501_YURETSKI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WEB_953501_YURETSKI.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment environment;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        public IActionResult OnGet()
        {
        ViewData["Category"] = new SelectList(_context.Categories, "Id", "Name");
        ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Food Food { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string base64Image;
            if(Image != null)
            {
                base64Image = Tools.ImageConverter.ImageToBase64(Image);
                
            }
            else
            {
                base64Image = Tools.ImageConverter.ImageToBase64(Path.Combine(environment.WebRootPath, "images/defaultFood.png"));
            }
            Food.Image = new Image() { Base64Image = base64Image };

            _context.Foods.Add(Food);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
