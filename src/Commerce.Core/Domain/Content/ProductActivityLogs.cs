using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Domain.Content
{
    [Table("ProductActivityLogs")]
    public class ProductActivityLog
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public PostStatus FromStatus { set; get; }

        public PostStatus ToStatus { set; get; }

        public DateTime DateCreated { get; set; }

        [MaxLength(500)]
        public string? Note { set; get; }
        public string? UserName { set; get; }


        public Guid UserId { get; set; }

    }
}
