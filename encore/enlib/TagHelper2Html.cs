using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace enlib
{
    public static class TagHelper2Html
    {
        private static string processTagHelper(TagHelper tag, string contents = null)
        {
            var gcTask = new Func<bool, HtmlEncoder, Task<TagHelperContent>>(
                async (bool what, HtmlEncoder encoder) =>
                {
                    var content = new DefaultTagHelperContent();

                    if (contents != null)
                    {
                        content.SetHtmlContent(contents);
                    }

                    return await Task.Run<TagHelperContent>(() => { return content;  });
                }
            );

            var emptyAttrs = new TagHelperAttributeList();
            var output = new TagHelperOutput(null, emptyAttrs, gcTask);

            var context = new TagHelperContext(String.Empty, emptyAttrs, new Dictionary<object, object>(), "myId");

            var task = tag.ProcessAsync(context, output);

            if (task == null)
            {
                throw new Exception("Unable to process tag: ProcessAsync task didn't return a task.");
            }

            var success = task.Wait(30000); // Use a global timeout here!

            if (!success || !task.IsCompleted || task.IsFaulted)
            {
                throw new Exception("Error asynchronously processing task. Task status is: " + task.Status.ToString(), task.Exception);
            }

            var strctn = new StringBuilder();
            output.WriteTo(new StringWriter(strctn), HtmlEncoder.Default);
            return strctn.ToString();
        }

        public static string Get(TagHelper component)
        {
            return processTagHelper(new TagHelpers.ExtButton());
        }

        public static string Get(TagHelper component, string contents)
        {
            return processTagHelper(new TagHelpers.ExtButton(), contents);
        }
    }
}
