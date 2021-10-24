using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WEB_953501_YURETSKI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WEB_953501_YURETSKI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Entities.Category> Categories {  get; set; }
        public DbSet<Entities.Food> Foods {  get; set; }
        public DbSet<Entities.Image> Images {  get; set; } 
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
