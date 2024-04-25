using Commerce.Core.Domain.Identity;
using Commerce.Core.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Repositories
{
    public interface IUserRepository : IRepository<AppUser, Guid>
    {
        Task RemoveUserFromRoles(Guid userId, string[] roles);
    }
}
