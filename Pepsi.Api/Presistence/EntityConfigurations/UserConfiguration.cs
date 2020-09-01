using Coupons.PepsiKSA.Api.Presistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coupons.PepsiKSA.Api.Presistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Property(c => c.MobileNumber).IsRequired().HasMaxLength(20);
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.PasswordHash).IsRequired();
            builder.Property(c => c.PasswordSalt).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.MobileNumber).IsUnique();
            builder.Property(c => c.RegisterDate).HasDefaultValueSql("getdate()");
            builder.Property(c => c.LastModificationDate).HasDefaultValueSql("getdate()");
            builder.Property(c => c.Country).HasDefaultValue("Saudi Arabia").HasMaxLength(20);
            builder.Property(c => c.Language).HasDefaultValue("Arabic").HasMaxLength(10);

        }
    }
}
