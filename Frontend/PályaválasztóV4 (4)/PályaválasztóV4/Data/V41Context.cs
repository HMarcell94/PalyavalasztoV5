using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PalyavalsztoV4.Models.v4_1;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace PalyavalsztoV4.Data
{
    public partial class v4_1Context : DbContext
    {
        public v4_1Context()
        {
        }

        public v4_1Context(DbContextOptions<v4_1Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1,3306;Database=main;Uid=root;Pwd=;", new MySqlServerVersion(new Version(8, 0, 21)));

        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PalyavalsztoV4.Models.v4_1.application>()
              .HasOne(i => i.employee)
              .WithMany(i => i.applications)
              .HasForeignKey(i => i.EmployeeID)
              .HasPrincipalKey(i => i.EmployeeID);

            builder.Entity<PalyavalsztoV4.Models.v4_1.application>()
              .HasOne(i => i.job)
              .WithMany(i => i.applications)
              .HasForeignKey(i => i.JobID)
              .HasPrincipalKey(i => i.JobID);

            builder.Entity<PalyavalsztoV4.Models.v4_1.employee>()
              .HasOne(i => i.user)
              .WithMany(i => i.employees)
              .HasForeignKey(i => i.UserID)
              .HasPrincipalKey(i => i.UserID);

            builder.Entity<PalyavalsztoV4.Models.v4_1.employer>()
              .HasOne(i => i.user)
              .WithMany(i => i.employers)
              .HasForeignKey(i => i.UserID)
              .HasPrincipalKey(i => i.UserID);

            builder.Entity<PalyavalsztoV4.Models.v4_1.job>()
              .HasOne(i => i.employer)
              .WithMany(i => i.jobs)
              .HasForeignKey(i => i.EmployerID)
              .HasPrincipalKey(i => i.EmployerID);

            builder.Entity<PalyavalsztoV4.Models.v4_1.user>()
              .HasOne(i => i.role)
              .WithMany(i => i.users)
              .HasForeignKey(i => i.RoleID)
              .HasPrincipalKey(i => i.RoleID);

            builder.Entity<PalyavalsztoV4.Models.v4_1.job>()
              .Property(p => p.MinSalary)
              .IsRequired();

            builder.Entity<PalyavalsztoV4.Models.v4_1.job>()
              .Property(p => p.MaxSalary)
              .IsRequired();
        }

         public DbSet<PalyavalsztoV4.Models.v4_1.application> applications { get; set; }

        public DbSet<PalyavalsztoV4.Models.v4_1.employee> employees { get; set; }

        public DbSet<PalyavalsztoV4.Models.v4_1.employer> employers { get; set; }

        public DbSet<PalyavalsztoV4.Models.v4_1.job> jobs { get; set; }

        public DbSet<PalyavalsztoV4.Models.v4_1.role> roles { get; set; }

        public DbSet<PalyavalsztoV4.Models.v4_1.user> users { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}
