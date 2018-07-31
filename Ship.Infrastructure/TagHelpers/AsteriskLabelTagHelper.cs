using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.TagHelpers
{
    [HtmlTargetElement("label", Attributes = "asterisk")]
    public class AsteriskLabelTagHelper: TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PostContent.SetHtmlContent("<span class='required' aria-required='true'>*</span>");
        }
    }
}
