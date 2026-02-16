using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Services
{
    public interface IImageSaveService
    {
        public Task<List<string>> SaveImgae(IFormFileCollection files,string src);

        public Task DeleteImage(string src);
    }
}
