using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(u => u.Name, n =>
        {
            n.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(30).IsRequired();
            n.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(30).IsRequired();
        });

        builder.OwnsOne(u => u.Email, e =>
        {
            e.Property(e => e.Value).HasColumnName("Email").IsRequired();
        });
        builder.OwnsOne(u => u.Phone, p =>
        {
            p.Property(p => p.CountryCode).HasColumnName("CountryCode").IsRequired();
            p.Property(p => p.Number).HasColumnName("PhoneNumber").IsRequired();
        });
    }
}