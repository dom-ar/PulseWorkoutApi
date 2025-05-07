using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "194389e9-a5a2-4938-bf24-1ce49b316cbd",
                Name = "Member",
                NormalizedName = "MEMBER"
            },
            new IdentityRole
            {
                Id = "a6be59e9-0002-47b8-8123-01b95883ed20",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });
    }
}