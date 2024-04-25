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
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkak�r megnevez�se k�telez�.");
            }

            if (string.IsNullOrEmpty(j.JobRequirements))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkak�vetelm�nyek megnevez�se k�telez�.");
                validationErrors.Add("A munkak�vetelm�nyek megnevez�se k�telez�.");
            }

            if (string.IsNullOrEmpty(j.Extras))
            {
            }

            if (string.IsNullOrEmpty(j.JobDescription))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkak�r le�r�s�t megadni k�telez�.");
                validationErrors.Add("A munkak�r le�r�s�t megadni k�telez�.");
            }

            if (string.IsNullOrEmpty(j.JobLocation))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munka helymeghat�roz�s�t megadni k�telez�.");
                validationErrors.Add("A munka helymeghat�roz�s�t megadni k�telez�.");
            }

            if (string.IsNullOrEmpty(j.MinSalary))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkak�r minimum b�r�t megadni k�telez�.");
                validationErrors.Add("A munkak�r minimum b�r�t megadni k�telez�.");
            }

            if (string.IsNullOrEmpty(j.MaxSalary))
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "A munkak�r maximum b�r�t megadni k�telez�.");
                validationErrors.Add("A munkak�r maximum b�r�t megadni k�telez�.");
            }


            if (validationErrors.Any())
            {
                this.NotificationService.Notify(NotificationSeverity.Error, "Hiba", "Valamilyen adatot nem megfelel�en adott meg.");
                validationErrors.Add("Valamilyen adatot nem megfelel�en adott meg.");

                return; // A ment�s megszak�t�sa
            }

            using (NetworkClient c = new NetworkClient())
            {
                APIResponse r = await c.PostAsync<job, APIResponse>("/api/Job", j, TokenStore.Token);
                if (r.StatusCode == 0)
                {
                    // siker eset�n
                }
                else
                {
                    // sikertelens�g eset�n
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