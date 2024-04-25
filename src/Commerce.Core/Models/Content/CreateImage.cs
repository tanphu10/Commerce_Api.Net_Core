using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Models.Content
{
    public class CreateImage
    {
        public IFormFile ThumbnailImage { get; set; }

    }
}
