using AutoMapper;
using Commerce.Core.Domain.Identity;
using Commerce.Core.Repositories;
using Commerce.Core.SeedWorks;
using Commerce.Data;
using Commerce.Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AirBnb.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CommerceContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UnitOfWork(CommerceContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            Products = new ProductRepository(context, mapper, userManager);
            Categories = new CategoryRepository(context, mapper);
        }
        public IProductRepository Products { get; set; }
        public ICategoryRepository Categories { get; set; }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
