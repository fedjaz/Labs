using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WEB_953501_YURETSKI.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_953501_YURETSKI.Controllers
{
    public class HomeController : Controller
    {
        private List<ListDemo> demoList;
        public HomeController()
        {
            demoList = new List<ListDemo>
            {
                new ListDemo(1, "Item 1"),
                new ListDemo(2, "Item 2"),
                new ListDemo(3, "Item 3"),
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Lab1()
        {
            ViewData["lab1"] = "active";
            
            return View();
        }

        public IActionResult Lab2()
        {
            ViewData["lab2"] = "active";
            ViewData["Text"] = "Лабораторная работа 2";
            ViewData["List"] = new SelectList(demoList, "ListItemValue", "ListItemText");

            return View();
        }
    }
}
