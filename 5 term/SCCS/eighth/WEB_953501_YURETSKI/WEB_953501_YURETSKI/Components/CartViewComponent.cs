using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_953501_YURETSKI.Models;
using WEB_953501_YURETSKI.Extensions;

namespace WEB_953501_YURETSKI.Components
{
    public class CartViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
            Cart cart = HttpContext.Session.Get<Cart>("cart");
            return View(cart);
        }
    }
}
