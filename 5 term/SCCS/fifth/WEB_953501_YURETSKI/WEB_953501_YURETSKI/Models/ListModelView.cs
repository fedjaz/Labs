using System;
using System.Collections.Generic;
using System.Linq;

namespace WEB_953501_YURETSKI.Models
{
    public class ListModelView<T> : List<T> where T : class, new()
    {
        public int Pages { get; set; }
        public int Current {  get; set; }

        public static ListModelView<T> CreatePage(List<T> items, int itemsPerPage, int page)
        {
            ListModelView<T> output = new ListModelView<T>();
            output.Pages = (int)Math.Ceiling((double)items.Count / itemsPerPage);

            page = Math.Max(page, 1);
            page = Math.Min(page, output.Pages);
            output.Current = page;


            output.AddRange(items.Skip((page - 1) * itemsPerPage).Take(itemsPerPage));

            return output;
        }
    }
}
