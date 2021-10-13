using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;
using Microsoft.AspNetCore.Routing;

namespace WEB_953501_YURETSKI.TagHelpers
{
    public class PagerTagHelper : TagHelper
    {
        public int Current { get; set; }
        public int Pages { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }

        private Microsoft.AspNetCore.Routing.LinkGenerator linkGenerator;

        public PagerTagHelper(LinkGenerator linkGenerator)
        {
            this.linkGenerator = linkGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("d-flex", HtmlEncoder.Default);
            output.AddClass("justify-content-center", HtmlEncoder.Default);

            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");

            TagBuilder prevPage = CreateButton("Предыдущая", Current - 1, isDisabled: Current == 1);
            ulTag.InnerHtml.AppendHtml(prevPage);

            if(Current > 1)
            {
                TagBuilder firstPage = CreateButton("1", 1);
                ulTag.InnerHtml.AppendHtml(firstPage);
            }

            if(Current > 3)
            {
                TagBuilder empty = CreateButton("..", 1, isDisabled: true);
                ulTag.InnerHtml.AppendHtml(empty);
            }

            if(Current > 2)
            {
                TagBuilder prevNumber = CreateButton((Current - 1).ToString(), Current - 1);
                ulTag.InnerHtml.AppendHtml(prevNumber);
            }

            TagBuilder current = CreateButton(Current.ToString(), Current, isActive: true);
            ulTag.InnerHtml.AppendHtml(current);

            if(Current < Pages - 1)
            {
                TagBuilder nextNumber = CreateButton((Current + 1).ToString(), Current + 1);
                ulTag.InnerHtml.AppendHtml(nextNumber);
            }

            if (Current < Pages - 2)
            {
                TagBuilder empty = CreateButton("..", 1, isDisabled: true);
                ulTag.InnerHtml.AppendHtml(empty);
            }

            if (Current < Pages)
            {
                TagBuilder lastPage = CreateButton(Pages.ToString(), Pages);
                ulTag.InnerHtml.AppendHtml(lastPage);
            }

            TagBuilder nextPage = CreateButton("Следующая", Current + 1, isDisabled: Current == Pages);
            ulTag.InnerHtml.AppendHtml(nextPage);

            output.Content.AppendHtml(ulTag);
        }

        TagBuilder CreateButton(string name, int page, bool isActive = false, bool isDisabled = false)
        {
            TagBuilder liTag = new TagBuilder("li");
            liTag.AddCssClass("page-item");
            if (isActive)
            {
                liTag.AddCssClass("active");
            }

            if (isDisabled)
            {
                liTag.AddCssClass("disabled");
            }

            TagBuilder aTag = new TagBuilder("a");
            aTag.AddCssClass("page-link");

            aTag.MergeAttribute("href", linkGenerator.GetPathByAction(Action, Controller, new { pageNo = page, category = Category }));
            aTag.InnerHtml.AppendHtml(name);
            liTag.InnerHtml.AppendHtml(aTag);
            return liTag;
        }
    }
}
