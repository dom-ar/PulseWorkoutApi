using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class BodyPartConfiguration : IEntityTypeConfiguration<BodyPart>
{
    public void Configure(EntityTypeBuilder<BodyPart> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.HasMany(b => b.Exercises)
            .WithOne(e => e.BodyPart)
            .HasForeignKey(e => e.BodyPartId);
    }
}