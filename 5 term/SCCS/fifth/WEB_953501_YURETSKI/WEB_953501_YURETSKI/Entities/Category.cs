using System.Collections.Generic;

namespace WEB_953501_YURETSKI.Entities
{
    public class Category
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public List<Food> Foods {  get; set; }
    }
}
