#pragma checksum "D:\NumberGo\NumberGo\Views\Error\Error400.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b77b77a258de4c97dc981364dacdc55cd8bd850a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Error_Error400), @"mvc.1.0.view", @"/Views/Error/Error400.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b77b77a258de4c97dc981364dacdc55cd8bd850a", @"/Views/Error/Error400.cshtml")]
    public class Views_Error_Error400 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\NumberGo\NumberGo\Views\Error\Error400.cshtml"
  
    ViewData["Title"] = "Bad request";

#line default
#line hidden
#nullable disable
            DefineSection("head", async() => {
                WriteLiteral("\r\n    <meta name=\"robots\" content=\"noindex,nofollow\">\r\n");
            }
            );
            WriteLiteral(@"<div class=""container-fluid mt-3"">
    <div class=""row"">
        <div class=""col-12"">
            <div class=""text-center"">
                <h4 class=""font-weight-bold text-danger"">Bad request</h4>
            </div>
        </div>
    </div>
</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591