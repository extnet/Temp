using enlib;
using enlib.HtmlComponents;
using enlib.TagHelpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ent.Pages
{
    public class IndexModel : PageModel
    {
        public HtmlString HtmlCompBtn { get; private set; }
        public HtmlString HtmlCompBtnVal { get; private set; }

        public HtmlString TagHlpBtn { get; private set; }
        public HtmlString TagHlpBtnVal { get; private set; }

        public void OnGet()
        {
            HtmlCompBtn = new HtmlString(ButtonExtension.Button(null).ToString());
            HtmlCompBtnVal = new HtmlString(ButtonExtension.Button(null, "Submit").ToString());

            TagHlpBtn = new HtmlString(TagHelper2Html.Get(new ExtButton()));
            TagHlpBtnVal = new HtmlString(TagHelper2Html.Get(new ExtButton(), "Submit"));
        }
    }
}