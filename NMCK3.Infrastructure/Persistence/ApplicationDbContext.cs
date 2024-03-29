﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NMCK3.Domain.Entities;
using NMCK3.Infrastructure.Persistence.Models;

namespace NMCK3.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamReservation> ExamReservations { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "admin"
                },
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "user"
                });
            base.OnModelCreating(builder);
        }


    }
}
