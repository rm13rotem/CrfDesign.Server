#pragma checksum "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0d411dc10aded7ba955e213f311601ff2f95cb6b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\_ViewImports.cshtml"
using CrfDesign.Server.WebAPI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\_ViewImports.cshtml"
using CrfDesign.Server.WebAPI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\Home\Index.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0d411dc10aded7ba955e213f311601ff2f95cb6b", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4ca2ebedfc9ad24988b984ad1b4d7538802c5725", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n");
#nullable restore
#line 10 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\Home\Index.cshtml"
     if (SignInManager.IsSignedIn(User))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h1 class=\"display-4\">Welcome ");
#nullable restore
#line 12 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\Home\Index.cshtml"
                                 Write(User.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n        <p>Design <a>");
#nullable restore
#line 13 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\Home\Index.cshtml"
                Write(Html.ActionLink("CRF Pages", "Index", "CrfPageComponent"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>.</p>\r\n        <p>View and Define <a>");
#nullable restore
#line 14 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\Home\Index.cshtml"
                         Write(Html.ActionLink("CRF Pages", "Index", "CrfPage"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>.</p>\r\n");
#nullable restore
#line 15 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\Home\Index.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h1>Welcome</h1>\r\n        <p>Please sign in or Register</p>\r\n");
#nullable restore
#line 20 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<IdentityUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<IdentityUser> SignInManager { get; private set; }
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
