using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_953501_YURETSKI.Models
{
    public class ListDemo
    {
        public int ListItemValue { get; set; }
        public string ListItemText {  get; set; }
        public ListDemo(int listItemValue, string listItemText)
        {
            ListItemValue = listItemValue;
            ListItemText = listItemText;
        }
    }
}
