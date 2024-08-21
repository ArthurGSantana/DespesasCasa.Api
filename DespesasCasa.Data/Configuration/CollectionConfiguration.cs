using DespesasCasa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesasCasa.Data.Configuration;

public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.ToTable("Collection");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasMany(x => x.Expanses)
            .WithOne()
            .HasForeignKey(x => x.CollectionId);

        builder.HasQueryFilter(x => x.Active);
    }
}
