using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Domain.Content
{
    [Table("Products")]
    [Index(nameof(Slug), IsUnique = true)]
    public class Product
    {
        [Key]
        public Guid Id { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Slug { get; set; }
        public bool? IsFeatured { get; set; }
        public string Name { set; get; }
        [MaxLength(500)]
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        [Required]
        public Guid CategoryId { set; get; }
        [MaxLength(500)]
        public Guid AuthorUserId { get; set; }
        [Required]
        [Column(TypeName = "varchar(250)")]
        public required string CategorySlug { set; get; }

        [MaxLength(250)]
        [Required]
        public required string CategoryName { set; get; }
        [MaxLength(250)]
        public string AuthorUserName { set; get; }
        public DateTime? PaidDate { get; set; }
        public bool IsPaid { get; set; }
        public PostStatus Status { get; set; }
    }
    public enum PostStatus
    {
        Draft = 0,
        WaitingForApproval = 1,
        Rejected = 2,
        Published = 3
    }
}
