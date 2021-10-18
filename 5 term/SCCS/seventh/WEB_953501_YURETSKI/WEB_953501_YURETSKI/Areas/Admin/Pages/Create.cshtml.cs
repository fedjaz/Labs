using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_953501_YURETSKI.Data;
using WEB_953501_YURETSKI.Entities;

namespace WEB_953501_YURETSKI.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WEB_953501_YURETSKI.Data.ApplicationDbContext _context;

        public CreateModel(WEB_953501_YURETSKI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
        ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Food Food { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Foods.Add(Food);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
