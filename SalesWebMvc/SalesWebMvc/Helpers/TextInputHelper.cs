using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SalesWebMvc.Helpers
{

    [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]
    public class TextInputHelper : TagHelper
    {
        public string cust_value { get; set; }
        public string cust_name { get; set; }
        public bool cust_readonly { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.TagMode = TagMode.StartTagAndEndTag;

            if (cust_readonly)
            {
                output.Attributes.SetAttribute("readonly", "required");
            }
        }
    }
}
