using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Lib;
using PalyavalsztoV4.Models.V41;
using PalyavalsztoV4.Models.v4_1;

namespace PalyavalsztoV4.Components
{
    public partial class AddCard
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

        [Parameter]
        public int id { get; set; }

        job UjAd = new job();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Adcards();
        }
        private async Task Adcards()
        {
            using (NetworkClient c = new NetworkClient())
            {
                APIResponse<job> r = await c.PostAsync<job, APIResponse<job>>($"/Job/{id}", UjAd);
                if (r == null)
                {
                    Console.WriteLine("Hiba történt a Hírdetés feltöltésekor.");
                }
                else 
                {
                    UjAd = r.Data;
                }
            }
        }
    }
}
