using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Commerce.Data
{
    public class CommerceContextFactory : IDesignTimeDbContextFactory<CommerceContext>
    {
        public CommerceContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var builder = new DbContextOptionsBuilder<CommerceContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new CommerceContext(builder.Options);
        }
    }
}
