using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace enlib.HtmlComponents
{
    public static class ButtonExtension
    {
        public static IHtmlContent Button(this IHtmlHelper htmlHelper)
        {
            return new HtmlString(TagHelper2Html.Get(new TagHelpers.ExtButton()));
        }

        public static IHtmlContent Button(this IHtmlHelper htmlHelper, string text)
        {
            return new HtmlString(TagHelper2Html.Get(new TagHelpers.ExtButton(), text));
        }
    }
}
