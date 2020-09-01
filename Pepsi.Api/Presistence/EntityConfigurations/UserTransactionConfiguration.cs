using Coupons.PepsiKSA.Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coupons.PepsiKSA.Api.Presistence.EntityConfigurations
{
    public class UserTransactionConfiguration : IEntityTypeConfiguration<UserTransaction>
    {
        public void Configure(EntityTypeBuilder<UserTransaction> builder)
        {
            builder.Property(c => c.UserEmail).IsRequired().HasMaxLength(100);
            builder.HasOne(c => c.User).WithMany(c => c.UserTransactions)
                .HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade); ;
            builder.Property(c => c.Date).HasDefaultValueSql("getdate()");
            builder.Property(c => c.ProductId).HasMaxLength(30).HasDefaultValue("Pepsi");
            builder.Property(c => c.PromoCode).HasMaxLength(50);
            builder.Property(c => c.CampaignId).HasMaxLength(50).HasDefaultValue("KSAPepsiPromo2020");
            builder.Property(c => c.Sku).HasMaxLength(50).HasDefaultValue("330ml");

        }
    }
}
