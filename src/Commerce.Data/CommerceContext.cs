using Commerce.Core.Domain.Content;
using Commerce.Core.Domain.Identity;
using Commerce.Core.SeedWorks.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Data
{
    public class CommerceContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public CommerceContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Cart> Carts { set; get; }

        public DbSet<Product> Products { set; get; }
        public DbSet<Category> Categories { set; get; }
      
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<ProductActivityLog> ProductActivityLogs { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductInCategory> ProductInCategorys { set; get; }
        public DbSet<Promotion> Promotions { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<Transaction> Transactions { set; get; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId, x.UserId });
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => new { x.UserId });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
               .Entries()
               .Where(e => e.State == EntityState.Added);

            foreach (var entityEntry in entries)
            {
                var dateCreatedProp = entityEntry.Entity.GetType().GetProperty(SystemConsts.DateCreatedField);
                if (entityEntry.State == EntityState.Added
                    && dateCreatedProp != null)
                {
                    dateCreatedProp.SetValue(entityEntry.Entity, DateTime.Now);
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
