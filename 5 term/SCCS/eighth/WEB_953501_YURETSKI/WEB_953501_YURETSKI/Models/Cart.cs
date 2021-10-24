using System;
using System.Collections.Generic;
using System.Linq;
using WEB_953501_YURETSKI.Entities;

namespace WEB_953501_YURETSKI.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }

        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }

        public int Count
        {
            get
            {
                return Items.Sum(i => i.Value.Count);
            }
        }

        public double Price
        {
            get
            {
                return Items.Sum(i => i.Value.Item.Price * i.Value.Count);
            }
        }

        public virtual void AddToCart(Food item)
        {
            if (Items.ContainsKey(item.Id))
            {
                Items[item.Id].Count++;
            }
            else
            {
                Items.Add(item.Id, new CartItem() { Count = 1, Item = item});
            }
        }

        public virtual void RemoveFormCart(int id)
        {
            Items.Remove(id);
        }

        public virtual void RemoveFormCart(Food item)
        {
            Items.Remove(item.Id);
        }

        public virtual void Clear()
        {
            Items.Clear();
        }
    }
}
