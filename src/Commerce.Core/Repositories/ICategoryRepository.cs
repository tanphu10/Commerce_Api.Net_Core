using Commerce.Core.Domain.Content;
using Commerce.Core.Models;
using Commerce.Core.Models.Content;
using Commerce.Core.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task<bool> HasPost(Guid categoryId);
        Task<PagedResult<CategoryDto>> GetCategoryPagingAsync(string keyword, int pageIndex = 1, int pageSize = 10);

    }
}
