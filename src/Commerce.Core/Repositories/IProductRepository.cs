using Commerce.Core.Domain.Content;
using Commerce.Core.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Repositories
{
    public interface IProductRepository : IRepository<Product,Guid>
    {
    }
}
