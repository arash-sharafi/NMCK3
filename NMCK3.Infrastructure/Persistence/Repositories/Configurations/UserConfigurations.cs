using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMCK3.Domain.Entities;
using NMCK3.Domain.ValueObjects;

namespace NMCK3.Infrastructure.Persistence.Repositories.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("AspNetUsers");

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasConversion(e => e.Value,
                    value => Email.Create(value).Value);
        }
    }
}
