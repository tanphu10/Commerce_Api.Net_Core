using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Domain.Content
{
    [Table("ProductInCategories")]

    [PrimaryKey(nameof(ProductId), nameof(CategoryId))]

    public class ProductInCategory
    {
        
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
