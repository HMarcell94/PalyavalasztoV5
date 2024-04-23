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
using PalyavalsztoV4.Models.v4_1;
using PalyavalsztoV4.Models.V41;

namespace PalyavalsztoV4.Components.Pages
{
    public partial class RegistrationUser
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

        employee EmployeeReg = new employee();

        private async Task tovabbiregdataUsrs()
        {
            using (NetworkClient c = new NetworkClient())
            {
                APIResponse r = await c.PostAsync<employee, APIResponse>("/employee", EmployeeReg);
                if (r == null)
                {
                    // hiba kezelése
                }
                else
                {
                    // sikeres regisztráció üzenet
                    Console.WriteLine("Sikeres regisztráció!");

                    // továbblépés a fõoldalra
                    NavigationManager.NavigateTo("/mainPage");
                }
            }
        }
    }
}