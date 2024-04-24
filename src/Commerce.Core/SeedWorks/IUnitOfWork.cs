using Commerce.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.SeedWorks
{
    public interface IUnitOfWork
    {

        IProductRepository Products { get; }

        Task<int> CompleteAsync();

    }
}
