using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WEB_953501_YURETSKI.Data;
using WEB_953501_YURETSKI.Entities;
using WEB_953501_YURETSKI.Models;

namespace WEB_953501_YURETSKI.Controllers
{
    public class CartController : Controller
    {
        Cart Cart { get; set; }
        ApplicationDbContext Context { get; set; }

        public CartController(Cart cart, ApplicationDbContext context)
        {
            Cart = cart;
            Context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> items = Cart.Items.Values.ToList();

            return View(items);
        }

        public IActionResult Add(int id, string returnUrl)
        {
            Food food = Context.Foods.FirstOrDefault(f => f.Id == id);
            if(food.Id == id)
            {
                food.Category = Context.Categories.FirstOrDefault(c => c.Id == food.CategoryId);
                Cart.AddToCart(food);
            }
            return Redirect(returnUrl);
        }

        public IActionResult Delete(int id)
        {
            Cart.RemoveFormCart(id);

            return RedirectToAction("Index");
        }
    }
}
