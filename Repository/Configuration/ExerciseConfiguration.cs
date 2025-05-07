using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        // Primary key
        builder.HasKey(e => e.Id);

        // Value generated on add
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
    }
}