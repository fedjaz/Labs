using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_953501_YURETSKI.Data;
using WEB_953501_YURETSKI.Entities;

namespace WEB_953501_YURETSKI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WEB_953501_YURETSKI.Data.ApplicationDbContext _context;

        public DetailsModel(WEB_953501_YURETSKI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Food Food { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Food = await _context.Foods
                .Include(f => f.Category)
                .Include(f => f.Image).FirstOrDefaultAsync(m => m.Id == id);

            if (Food == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
