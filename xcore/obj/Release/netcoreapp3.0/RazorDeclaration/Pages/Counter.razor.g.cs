#pragma checksum "E:\workspace\csharp\xcore\xcore\Pages\Counter.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e7bf91bf21a201381ac6aff6e8cd681c693a835"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace xcore.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "E:\workspace\csharp\xcore\xcore\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\workspace\csharp\xcore\xcore\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\workspace\csharp\xcore\xcore\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\workspace\csharp\xcore\xcore\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\workspace\csharp\xcore\xcore\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\workspace\csharp\xcore\xcore\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\workspace\csharp\xcore\xcore\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\workspace\csharp\xcore\xcore\_Imports.razor"
using xcore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "E:\workspace\csharp\xcore\xcore\_Imports.razor"
using xcore.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\workspace\csharp\xcore\xcore\Pages\Counter.razor"
using xcore.Area.Test.Service;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\workspace\csharp\xcore\xcore\Pages\Counter.razor"
using System.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\workspace\csharp\xcore\xcore\Pages\Counter.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\workspace\csharp\xcore\xcore\Pages\Counter.razor"
using System.IO;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/counter")]
    public class Counter : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 51 "E:\workspace\csharp\xcore\xcore\Pages\Counter.razor"
      
    private int start { get; set; }
    private int take { get; set; }
    private List<CommonActivity> activitys { get; set; }
    private List<CommonActivity> displayActivitys { get; set; }
    private string firstActivityPage { get; set; }
    private string name { get; set; }

    public void dllInject()
    {

        this.dllService.injectDll();

    }

    public void getData()
    {
        this.activityService.getAllActivity();
    }

    public void parseSingleParse()
    {
        this.firstActivityPage = this.activityService.getSingleActivityHtmlText();
        var content = this.activityService.parseActivityHtml(this.firstActivityPage);
        Console.WriteLine(this.firstActivityPage);
        Console.WriteLine(content);
    }
    public void parseAllHtml()
    {
        this.activitys = this.activityService.parseJsonFile();
    }

    public void showActivity()
    {
        this.displayActivitys = (from a in this.activitys select a).ToList().Skip(this.start).Take(this.take).ToList();
    }

    public List<KeywordGroup> groups { get; set; } = new List<KeywordGroup>();
    public void groupKeywords()
    {
        Console.WriteLine("keywords");
        foreach(var a in this.activitys)
        {
            var keywords = a.tags.Trim().Replace(",", "").Split(" ");
            foreach(var keyword in keywords)
            {
                var count = (from g in this.groups where keyword==g.keyword select g).Count();
                if (count == 0)
                {
                    this.groups.Add(new KeywordGroup { keyword = keyword,workNum=0,downloadNum=0 });
                }
                else
                {
                    var g=  this.groups.Find(g => g.keyword == keyword);
                    int.TryParse(a.downloadNum, out int downloadNum);
                    g.workNum += 1;
                    g.downloadNum += downloadNum;
                }

            }

        }
        this.groups = (from g in this.groups orderby g.downloadNum descending, g.workNum descending select g).ToList();
        Console.WriteLine(this.groups.Count);

    }



#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ActivityWangService activityService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private DllService dllService { get; set; }
    }
}
#pragma warning restore 1591
