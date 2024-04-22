using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace PalyavalsztoV4.Components.Pages
{
    public partial class NewHirdetes
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
    }
    namespace PalyavalsztoV4Data
    {
        public class Jobs
        {
            public string JobTitle { get; set; }
            public string JobDescription { get; set; }
            public int MinSalary { get; set; }
            public int MaxSalary { get; set; }
            public string JobLocation { get; set; }
            public string Picture { get; set; }
        }
    }
}