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
using PalyavalsztoV4.Data;
using PalyavalsztoV4.Models.v4_1;
using PalyavalsztoV4.Models.V41;
using Newtonsoft.Json;

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

        job j = new job();
        RadzenImage image;

        async Task SaveJob()
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrEmpty(j.JobTitle))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkakör megnevezése kötelezõ.");
            }

            if (string.IsNullOrEmpty(j.JobRequirements))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkakövetelmények megnevezése kötelezõ.");
                validationErrors.Add("A munkakövetelmények megnevezése kötelezõ.");
            }

            if (string.IsNullOrEmpty(j.Extras))
            {
            }

            if (string.IsNullOrEmpty(j.JobDescription))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkakör leírását megadni kötelezõ.");
                validationErrors.Add("A munkakör leírását megadni kötelezõ.");
            }

            if (string.IsNullOrEmpty(j.JobLocation))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munka helymeghatározását megadni kötelezõ.");
                validationErrors.Add("A munka helymeghatározását megadni kötelezõ.");
            }

            if (string.IsNullOrEmpty(j.MinSalary))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkakör minimum bérét megadni kötelezõ.");
                validationErrors.Add("A munkakör minimum bérét megadni kötelezõ.");
            }

            if (string.IsNullOrEmpty(j.MaxSalary))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkakör maximum bérét megadni kötelezõ.");
                validationErrors.Add("A munkakör maximum bérét megadni kötelezõ.");
            }


            if (validationErrors.Any())
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "Valamilyen adatot nem megfelelõen adott meg.");
                validationErrors.Add("Valamilyen adatot nem megfelelõen adott meg.");

                return; // A mentés megszakítása
            }

            using (NetworkClient c = new NetworkClient())
            {
                APIResponse r = await c.PostAsync<job, APIResponse>("/api/Job", j, TokenStore.Token);
                if (r.StatusCode == 0)
                {
                    // siker esetén
                }
                else
                {
                    // sikertelenség esetén
                }
            }
        }

        async Task PictureUploaded(UploadCompleteEventArgs upload)
        {
            UploadedFile f = JsonConvert.DeserializeObject<UploadedFile>(upload.RawResponse);
            image.Path = $"data:{f.ContentType};base64,{f.Content}";
        }
    }
}