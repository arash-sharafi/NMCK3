using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMCK3.Domain.Entities;
using NMCK3.Domain.ValueObjects;

namespace NMCK3.Infrastructure.Persistence.Repositories.Configurations
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.Property(v => v.Id)
                .ValueGeneratedNever();

            builder.Property(v => v.VoucherCode)
                .HasColumnName(nameof(VoucherCode))
                .HasConversion(vc => vc.Value,
                    value => VoucherCode.Create(value).Value);

            builder.Property(v => v.PurchaseDate)
                .HasColumnName("PurchaseDate")
                .HasConversion(ed => ed.Value,
                    value => VoucherPurchaseDate.Create(value, new PersianDate(value)).Value);
        }
    }
}
