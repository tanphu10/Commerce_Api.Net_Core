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
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<bool> IsSlugAlreadyExisted(string slug, Guid? currentId = null);
        Task<PagedResult<ProductInListDto>> GetAllPaging(string? keyword, Guid userId, Guid? categoryId, int pageIndex = 1, int pageSize = 10);
        Task Approve(Guid id, Guid currentUserId);
        Task SendToApprove(Guid id, Guid currentUserId);
        Task ReturnBack(Guid id, Guid currentUserId, string note);
        Task<string> GetReturnReason(Guid id);
        Task<List<ProductActivityLogDto>> GetActivityLogs(Guid id);


    }
}
