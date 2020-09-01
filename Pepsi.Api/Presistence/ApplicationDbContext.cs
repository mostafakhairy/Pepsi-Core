using Coupons.PepsiKSA.Api.Core.Models;
using Coupons.PepsiKSA.Api.Presistence.EntityConfigurations;
using Coupons.PepsiKSA.Api.Presistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.PepsiKSA.Api.Presistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :
            base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserTransactionConfiguration());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTransaction> UserTransactions { get; set; }
    }

}
