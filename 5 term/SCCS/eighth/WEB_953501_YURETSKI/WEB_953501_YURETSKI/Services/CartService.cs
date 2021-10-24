using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using WEB_953501_YURETSKI.Models;
using WEB_953501_YURETSKI.Extensions;
using Newtonsoft.Json;
using WEB_953501_YURETSKI.Entities;

namespace WEB_953501_YURETSKI.Services
{
    
    public class CartService : Cart
    {
        [JsonIgnore]
        ISession Session { get; set; }

        public static Cart GetCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()
                .HttpContext
                .Session;

            CartService cart = session.Get<CartService>("cart");

            cart.Session = session;
            return cart;
        }

        public override void AddToCart(Food item)
        {
            base.AddToCart(item);
            Session.Set("cart", this);
        }

        public override void RemoveFormCart(Food item)
        {
            base.RemoveFormCart(item);
            Session.Set("cart", this);
        }

        public override void RemoveFormCart(int id)
        {
            base.RemoveFormCart(id);
            Session.Set("cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Set("cart", this);
        }
    }
}
