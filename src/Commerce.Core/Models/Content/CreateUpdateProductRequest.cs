using AutoMapper;
using Commerce.Core.Domain.Content;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Models.Content
{
    public class CreateUpdateProductRequest
    {
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string Slug { get; set; }
        public string SeoAlias { get; set; }
        public bool? IsFeatured { get; set; }
        public Guid CategoryId { get; set; }
        public string ThumbnailImage { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<CreateUpdateProductRequest, Product>();
            }
        }

    }
}
