using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMCK3.Domain.Entities;
using NMCK3.Domain.ValueObjects;

namespace NMCK3.Infrastructure.Persistence.Repositories.Configurations
{
    public class ExamConfigurations : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasMaxLength(300);

            builder.Property(e => e.Capacity)
                .IsRequired();


            builder.Property(e => e.ExamDate)
                .HasColumnName(nameof(ExamDate))
                .HasConversion(ed => ed.Value,
                    value => ExamDate.Create(value, new PersianDate(value)).Value);


            builder.OwnsMany(x => x.ExamReservations, er =>
            {
                er.ToTable(nameof(ExamReservation));

                er.HasKey("Id", "ExamId");

                er.Property(x => x.Id)
                    .ValueGeneratedNever();

                
                er.Property(s => s.ScoreSubmitDate)
                    .HasMaxLength(8)
                    .IsRequired(false);
            });

            builder.Navigation(e => e.ExamReservations).Metadata.SetField("_examReservations");
            builder.Navigation(e => e.ExamReservations).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
