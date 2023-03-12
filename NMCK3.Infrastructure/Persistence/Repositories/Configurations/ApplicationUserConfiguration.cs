using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMCK3.Domain.Entities;
using NMCK3.Infrastructure.Persistence.Models;

namespace NMCK3.Infrastructure.Persistence.Repositories.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.Property(i => i.Email)
                .HasColumnName("Email");

            builder.HasOne<User>().WithOne()
                .HasForeignKey<User>(u => u.Id);
        }
    }
}