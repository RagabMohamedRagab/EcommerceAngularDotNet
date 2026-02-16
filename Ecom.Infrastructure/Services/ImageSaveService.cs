using Ecom.core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Services
{
    public class ImageSaveService : IImageSaveService
    {
        private readonly IFileProvider _fileProvider;

        public ImageSaveService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task DeleteImage(string src)
        {
            var Path = _fileProvider.GetFileInfo(src);
            File.Delete(Path.PhysicalPath);
        }

        public async Task<List<string>> SaveImgae(IFormFileCollection files, string src)
        {
            List<string> images = new List<string>();
            var FullPath = Path.Combine("wwwroot", "Image",src);

            if(!Directory.Exists(FullPath))
            {
                Directory.CreateDirectory(FullPath);
            }
            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    var ImageName = item.FileName;
                    var ImagePath = Path.Combine(FullPath, ImageName);

                    using (var stream = new FileStream(ImagePath, FileMode.Create))
                    {
                      await  item.CopyToAsync(stream);
                    }
                    images.Add(Path.Combine(src, ImageName));
                }
            }
            return images;
        }
    }
}
