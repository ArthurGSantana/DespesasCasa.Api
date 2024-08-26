using DespesasCasa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesasCasa.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(512);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasQueryFilter(x => x.Active);
    }
}
