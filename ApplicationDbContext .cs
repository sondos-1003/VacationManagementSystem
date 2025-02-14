using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyProject1
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<RequestState> RequestStates { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-U8U0203\\SQLEXPRESS;Database=SkyData;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Employee - Department (One-to-Many)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            // Employee - Position (One-to-Many)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId);

            // VacationRequest - Employee (One-to-Many)
            modelBuilder.Entity<VacationRequest>()
                .HasOne(v => v.Employee)
                .WithMany(e => e.VacationRequests)
                .HasForeignKey(v => v.EmployeeNumber)
                .OnDelete(DeleteBehavior.Cascade); // Ensure cascading deletion

            // VacationRequest - VacationType (One-to-Many)
            modelBuilder.Entity<VacationRequest>()
                .HasOne(v => v.VacationType)
                .WithMany(vt => vt.VacationRequests)
                .HasForeignKey(v => v.VacationTypeCode);

            // VacationRequest - RequestState (One-to-Many)
            modelBuilder.Entity<VacationRequest>()
        .HasOne(vr => vr.RequestState)
        .WithMany(rs => rs.VacationRequests)
        .HasForeignKey(vr => vr.RequestStateId)
        .OnDelete(DeleteBehavior.Restrict); // Prevents accidental deletions
        }
    }
}
