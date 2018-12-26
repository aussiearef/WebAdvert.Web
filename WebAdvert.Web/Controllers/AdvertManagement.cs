using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAdvert.Web.Controllers
{
    public class AdvertManagement : Controller
    {
        private readonly IFileUploader _fileUploader;

        public AdvertManagement(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        public IActionResult Create(CreateAdvertViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertViewModel model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var id = "11111";
                // You must make a call to Advert Api, create the advertisement in the database and return Id

                var fileName = "";
                if (imageFile != null)
                {
                    fileName = !string.IsNullOrEmpty(imageFile.FileName) ? Path.GetFileName(imageFile.FileName) : id;
                    var filePath = $"{id}/{fileName}";

                    try
                    {
                        using (var readStream = imageFile.OpenReadStream())
                        {
                            var result = await _fileUploader.UploadFileAsync(filePath, readStream)
                                .ConfigureAwait(false);
                            if (!result)
                                throw new Exception(
                                    "Could not upload the image to file repository. Please see the logs for details.");
                        }

                        // Call Advert Api and confirm the advertisement

                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception e)
                    {
                        // Call Advert Api and cancel the advertisement
                        Console.WriteLine(e);
                    }
                }
            }

            return View(model);
        }
    }
}