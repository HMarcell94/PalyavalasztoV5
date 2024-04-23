using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using PalyavalsztoV4.Models.V41;

namespace PalyavalsztoV4.Controllers
{
    public partial class UploadController : Controller
    {
        private readonly IWebHostEnvironment environment;

        public UploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        // Single file upload
        [HttpPost]
        public IActionResult Single(IFormFile file)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                ms.Position = 0;
                return Ok(new UploadedFile
                {
                    Content = Convert.ToBase64String(ms.ToArray()),
                    ContentType = file.ContentType,
                    Name = file.Name
                 });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
