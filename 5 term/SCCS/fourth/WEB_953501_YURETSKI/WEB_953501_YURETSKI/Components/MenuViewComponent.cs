using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_953501_YURETSKI.Models;

namespace WEB_953501_YURETSKI.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private List<MenuItem> menus = new List<MenuItem> 
        {
            new MenuItem{Controller = "Home", Action = "Lab1", Text = "Лр 1"},
            new MenuItem{Controller = "Home", Action = "Lab2", Text = "Лр 2"},
            new MenuItem{Controller = "Home", Action = "Lab3", Text = "Лр 3"},
            new MenuItem{Controller = "Account", Action = "Index", Text = "Лр 4"},
            new MenuItem{Controller = "Product", Action = "Index", Text = "Каталог"},
            new MenuItem{IsPage = true, Area = "Admin", Page = "/Index", Text = "Администрирование"}
        };

        public IViewComponentResult Invoke()
        {
            string controller = (string)ViewContext.RouteData.Values["controller"];
            string action = (string)ViewContext.RouteData.Values["action"];
            string page = (string)ViewContext.RouteData.Values["page"];
            string area = (string)ViewContext.RouteData.Values["area"];
            foreach (MenuItem menuItem in menus)
            {
                if((menuItem.Controller == controller && menuItem.Action == action) ||
                    (menuItem.Area == area && menuItem.Page == page))
                {
                    menuItem.Active = "active";
                    break;
                }
            }
            return View(menus);
        }

    }
}
