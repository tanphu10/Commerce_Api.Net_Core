using AutoMapper;
using Commerce.Core.Domain.Content;
using Commerce.Core.Domain.Identity;
using Commerce.Core.Models;
using Commerce.Core.Models.Content;
using Commerce.Core.Repositories;
using Commerce.Core.SeedWorks.Constants;
using Commerce.Data.SeedWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Commerce.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product, Guid>, IProductRepository
    {
        private readonly CommerceContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public ProductRepository(CommerceContext context, IMapper mapper, UserManager<AppUser> userManager) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task Approve(Guid id, Guid currentUserId)
        {
            var post = await _context.Products.FindAsync(id);
            if (post == null)
            {
                throw new Exception("Không tồn tại bài viết");
            }
            var user = await _context.Users.FindAsync(currentUserId);
            await _context.ProductActivityLogs.AddAsync(new ProductActivityLog
            {
                Id = Guid.NewGuid(),
                FromStatus = post.Status,
                ToStatus = PostStatus.Published,
                UserId = currentUserId,
                UserName = user.UserName,
                ProductId = id,
                Note = $"{user?.UserName} duyệt bài"
            });
            post.Status = PostStatus.Published;
            _context.Products.Update(post);
        }

        public async Task<PagedResult<ProductInListDto>> GetAllPaging(string? keyword, Guid userId, Guid? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("Không tồn tại user");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var canApprove = false;
            if (roles.Contains(Roles.Admin))
            {
                canApprove = true;
            }
            else
            {
                canApprove = await _context.RoleClaims.AnyAsync(x => roles.Contains(x.RoleId.ToString())
                           && x.ClaimValue == Permissions.Products.Approve);
            }
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }

            if (!canApprove)
            {
                query = query.Where(x => x.AuthorUserId == userId);
            }

            var totalRow = await query.CountAsync();

            query = query.OrderByDescending(x => x.DateCreated)
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize);

            return new PagedResult<ProductInListDto>
            {
                Results = await _mapper.ProjectTo<ProductInListDto>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }

        public Task<bool> IsSlugAlreadyExisted(string slug, Guid? currentId=null)
        {
            if (currentId.HasValue)
            {
                return _context.Products.AnyAsync(x => x.Slug == slug && x.Id != currentId.Value);
            }
            return _context.Products.AnyAsync(x => x.Slug == slug);
        }

      

        public async Task SendToApprove(Guid id, Guid currentUserId)
        {
            var post = await _context.Products.FindAsync(id);
            if (post == null)
            {
                throw new Exception("Không tồn tại bài viết");
            }
            var user = await _userManager.FindByIdAsync(currentUserId.ToString());
            if (user == null)
            {
                throw new Exception("Không tồn tại user");
            }
            await _context.ProductActivityLogs.AddAsync(new ProductActivityLog
            {
                FromStatus = post.Status,
                ToStatus = PostStatus.WaitingForApproval,
                UserId = currentUserId,
                ProductId = post.Id,
                UserName = user.UserName,
                Note = $"{user.UserName} gửi bài chờ duyệt"
            });

            post.Status = PostStatus.WaitingForApproval;
            _context.Products.Update(post);
        }
        public async Task ReturnBack(Guid id, Guid currentUserId, string note)
        {
            var post = await _context.Products.FindAsync(id);
            if (post == null)
            {
                throw new Exception("Không tồn tại bài viết");
            }

            var user = await _userManager.FindByIdAsync(currentUserId.ToString());
            await _context.ProductActivityLogs.AddAsync(new ProductActivityLog
            {
                FromStatus = post.Status,
                ToStatus = PostStatus.Rejected,
                UserId = currentUserId,
                UserName = user.UserName,
                ProductId = post.Id,
                Note = note
            });

            post.Status = PostStatus.Rejected;
            _context.Products.Update(post);
        }
        public async Task<string> GetReturnReason(Guid id)
        {
            var activity = await _context.ProductActivityLogs
                .Where(x => x.ProductId == id && x.ToStatus == PostStatus.Rejected)
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefaultAsync();
            return activity?.Note;
        }
        public async Task<List<ProductActivityLogDto>> GetActivityLogs(Guid id)
        {
            var query = _context.ProductActivityLogs.Where(x => x.ProductId == id)
                .OrderByDescending(x => x.DateCreated);
            return await _mapper.ProjectTo<ProductActivityLogDto>(query).ToListAsync();
        }

    }
}
