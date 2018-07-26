using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.TagHelpers
{
    [HtmlTargetElement("form", Attributes = "ajax-success")]
    public class AjaxSuccessTagHelper : TagHelper
    {
        public string AjaxSuccess { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("data-ajax", "true");
            output.Attributes.SetAttribute("data-ajax-method", "post");
            output.Attributes.SetAttribute("data-ajax-mode", "replace");
            output.Attributes.SetAttribute("data-ajax-success", AjaxSuccess);
        }
    }
}
