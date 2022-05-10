using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class ImageRepo : IImageRepo
    {
        private readonly IHostingEnvironment webHost;

        public ImageRepo(IHostingEnvironment webHost)
        {
            this.webHost = webHost;
        }

        public async Task<bool> StoreImage(string imageName,IFormFile image)
        {
            
            var saveImg = Path.Combine(webHost.WebRootPath, "assets", "img", "product", imageName);
            string imgext = Path.GetExtension(image.FileName);
            if (imgext == ".jpg" || imgext == ".png")
            {
                using (var uploading = new FileStream(saveImg, FileMode.Create))
                {
                    try
                    {
                        await image.CopyToAsync(uploading);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool DeleteImage(string imageName)
        {
            string _imageToBeDeleted = Path.Combine(webHost.WebRootPath, "assets", "img", "product", imageName);
            if ((System.IO.File.Exists(_imageToBeDeleted)))
            {
                System.IO.File.Delete(_imageToBeDeleted);
                return true;
            }
            return false;
        }
    }
}
