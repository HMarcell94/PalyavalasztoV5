using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using PalyavalsztoV4.Models.V41;
using Lib;

namespace PalyavalsztoV4.Components.Pages
{
    public partial class Registration
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

        UserRegistrationDto userreg = new UserRegistrationDto();
        string passwordAgain;

        private void NavigateToMain() //Fõoldalra
        {
            NavigationManager.NavigateTo(" ");
        }
        private void NavigateToRegistrationCo() //Fõoldalra
        {
            NavigationManager.NavigateTo("registrationco");
        }

        private void NavigateToRegistrationUser() //Fõoldalra
        {
            NavigationManager.NavigateTo("registration-user");
        }

        async Task _Registration()
        {
            if (userreg.Password != passwordAgain)
            {
                // hiba
            } 
            else
            {
                using(NetworkClient c = new NetworkClient())
                {
                  /*  APIResponse rs = await c.GetAsync<APIResponse>("/UserRegistrationLogin/register");
                    if (rs.StatusCode == 1)
                    {
                        rs.Data.
                    } */

                    APIResponse r = await c.PostAsync<UserRegistrationDto, APIResponse>("/UserRegistrationLogin/register", userreg);
                    if (r == null)
                    {
                        if (userreg.Role)
                        {
                            NavigateToRegistrationCo();
                        }
                        else
                        {
                            NavigateToRegistrationUser();
                        }
                    }
                    else
                    {
                        //hiba kiírása r.Message -bõl
                    }
                }
            }
        }
    }
}