#pragma checksum "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\RenderCrfComponent\Date.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "65820c17a70634ee56dbc3798f1c0f9e38ced68f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RenderCrfComponent_Date), @"mvc.1.0.view", @"/Views/RenderCrfComponent/Date.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"65820c17a70634ee56dbc3798f1c0f9e38ced68f", @"/Views/RenderCrfComponent/Date.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4ca2ebedfc9ad24988b984ad1b4d7538802c5725", @"/Views/_ViewImports.cshtml")]
    public class Views_RenderCrfComponent_Date : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CrfPageComponentViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\RenderCrfComponent\Date.cshtml"
  
    // property Name of CrfPageComponent

    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"form-group\">\r\n    <label class=\"control-label\"");
            BeginWriteAttribute("for", " for=\"", 167, "\"", 184, 1);
#nullable restore
#line 10 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\RenderCrfComponent\Date.cshtml"
WriteAttributeValue("", 173, Model.Name, 173, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 10 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\RenderCrfComponent\Date.cshtml"
                                              Write(Model.QuestionText);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n    <input class=\"form-control\" type=\"datetime-local\" data-val=\"");
#nullable restore
#line 11 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\RenderCrfComponent\Date.cshtml"
                                                           Write(Model.IsRequired);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n           data-val-required=\"This field is required.\"");
            BeginWriteAttribute("id", "\r\n           id=\"", 353, "\"", 381, 1);
#nullable restore
#line 13 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\RenderCrfComponent\Date.cshtml"
WriteAttributeValue("", 370, Model.Name, 370, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("name", " name=\"", 382, "\"", 400, 1);
#nullable restore
#line 13 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\RenderCrfComponent\Date.cshtml"
WriteAttributeValue("", 389, Model.Name, 389, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    <span class=\"text-danger field-validation-valid\" data-valmsg-for=\"");
#nullable restore
#line 14 "C:\Users\rm13r\source\repos\CrfDesign.Server\CrfDesign.Server\CrfDesign.Server.WebAPI\Views\RenderCrfComponent\Date.cshtml"
                                                                 Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"></span>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CrfPageComponentViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
