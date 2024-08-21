using DespesasCasa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesasCasa.Data.Configuration;

public class ExpanseConfiguration : IEntityTypeConfiguration<Expanse>
{
    public void Configure(EntityTypeBuilder<Expanse> builder)
    {
        builder.ToTable("Expanse");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(250);
        builder.Property(x => x.Value)
            .IsRequired();
        builder.Property(x => x.PaymentDate)
            .IsRequired();
        builder.Property(x => x.Observation)
            .HasMaxLength(500);

        builder.HasOne(x => x.Collection)
            .WithMany(x => x.Expanses)
            .HasForeignKey(x => x.CollectionId);

        builder.HasQueryFilter(x => x.Active);
    }
}
