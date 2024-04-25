using Commerce.Core.Domain.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Commerce.Core.Models.Content
{
    public class ProductDto
    {
        public Guid Id { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        public string Slug { get; set; }
        public bool? IsFeatured { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public Guid CategoryId { set; get; }
        public Guid AuthorUserId { get; set; }
        public required string CategorySlug { set; get; }
        public required string CategoryName { set; get; }
        public string AuthorUserName { set; get; }
        public DateTime? PaidDate { get; set; }
        public bool IsPaid { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Product, ProductDto>();
            }
        }
    }
}
