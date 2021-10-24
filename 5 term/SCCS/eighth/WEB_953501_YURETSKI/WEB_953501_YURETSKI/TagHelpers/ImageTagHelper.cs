using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace WEB_953501_YURETSKI.TagHelpers
{
    [HtmlTargetElement(tag: "img", Attributes = "img-action, img-controller")]
    public class ImageTagHelper : TagHelper
    {
        public string ImgAction { get; set; }
        public string ImgController { get; set; }
        private LinkGenerator linkGenerator;

        public ImageTagHelper(LinkGenerator linkGenerator)
        {
            this.linkGenerator = linkGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("src", linkGenerator.GetPathByAction(ImgAction, ImgController));
        }
    }
}
