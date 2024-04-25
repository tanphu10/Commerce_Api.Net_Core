using AutoMapper;
using Commerce.Core.Domain.Content;
using Commerce.Core.Models;
using Commerce.Core.Models.Content;
using Commerce.Core.Repositories;
using Commerce.Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Data.Repositories
{
    public class CategoryRepository:RepositoryBase<Category,Guid>,ICategoryRepository
    {
        private readonly CommerceContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(CommerceContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> HasPost(Guid categoryId)
        {
            return await _context.Products.AnyAsync(x => x.CategoryId == categoryId);
        }
        public async Task<PagedResult<CategoryDto>> GetCategoryPagingAsync(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
            var totalRow = await query.CountAsync();
            query = query.OrderByDescending(x => x.DateCreated).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new PagedResult<CategoryDto>
            {
                Results = await _mapper.ProjectTo<CategoryDto>(query).ToListAsync(),
                RowCount = totalRow,
                CurrentPage = pageIndex,
                PageSize = pageSize

            };

        }

    }
}
