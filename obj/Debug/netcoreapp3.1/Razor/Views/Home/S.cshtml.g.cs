#pragma checksum "D:\NumberGo\NumberGo\Views\Home\S.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "676b087f8794a438ff4a40e7a23e982db6b1fd7a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_S), @"mvc.1.0.view", @"/Views/Home/S.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"676b087f8794a438ff4a40e7a23e982db6b1fd7a", @"/Views/Home/S.cshtml")]
    public class Views_Home_S : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            DefineSection("head", async() => {
                WriteLiteral("\r\n    <meta name=\"robots\" content=\"noindex,nofollow\">\r\n    <meta name=\"description\"");
                BeginWriteAttribute("content", " content=\"", 99, "\"", 126, 1);
#nullable restore
#line 4 "D:\NumberGo\NumberGo\Views\Home\S.cshtml"
WriteAttributeValue("", 109, ViewData["Desc"], 109, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <meta property=\"og:url\"");
                BeginWriteAttribute("content", " content=\"", 159, "\"", 185, 1);
#nullable restore
#line 5 "D:\NumberGo\NumberGo\Views\Home\S.cshtml"
WriteAttributeValue("", 169, ViewData["Url"], 169, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <meta property=\"og:image\"");
                BeginWriteAttribute("content", " content=\"", 220, "\"", 248, 1);
#nullable restore
#line 6 "D:\NumberGo\NumberGo\Views\Home\S.cshtml"
WriteAttributeValue("", 230, ViewData["Image"], 230, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <meta property=\"og:type\" content=\"website\" />\r\n    <meta property=\"og:title\"");
                BeginWriteAttribute("content", " content=\"", 334, "\"", 362, 1);
#nullable restore
#line 8 "D:\NumberGo\NumberGo\Views\Home\S.cshtml"
WriteAttributeValue("", 344, ViewData["Title"], 344, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <meta property=\"og:description\"");
                BeginWriteAttribute("content", " content=\"", 403, "\"", 430, 1);
#nullable restore
#line 9 "D:\NumberGo\NumberGo\Views\Home\S.cshtml"
WriteAttributeValue("", 413, ViewData["Desc"], 413, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n");
            }
            );
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