using AutoMapper;
using Commerce.Core.Domain.Content;
using Commerce.Core.Repositories;
using Commerce.Data.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
