using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace enlib.TagHelpers
{
    public class ExtButton : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.Add(new TagHelperAttribute("type", "button"));
            output.Attributes.Add(new TagHelperAttribute("name", context.UniqueId));

            var content = (await output.GetChildContentAsync()).GetContent();
            if (string.IsNullOrWhiteSpace(content))
            {
                output.Attributes.Add("value", "Clickable button!");
            }
            else
            {
                output.Attributes.Add("value", content);
                output.Content.Clear();
            }
        }
    }
}
