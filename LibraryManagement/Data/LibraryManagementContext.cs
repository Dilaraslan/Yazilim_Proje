using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

namespace LibraryManagement.Data
{
    public class LibraryManagementContext : DbContext
    {
        public LibraryManagementContext (DbContextOptions<LibraryManagementContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("LibraryManagementContext-01459d8c-b591-4fb4-9187-7e75e79c4dd7");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignBookViewModel>()
                .HasOne(assign => assign.Customer)
                .WithMany()
                .HasForeignKey(assign => assign.CustomerId);

            modelBuilder.Entity<AssignBookViewModel>()
                .HasOne(assign => assign.Book)
                .WithMany()
                .HasForeignKey(assign => assign.BookId);
        }
        public DbSet<LibraryManagement.Models.LibraryModel> LibraryModel { get; set; } = default!;
        public DbSet<LibraryModel> Books { get; set; }
        public DbSet<LibraryManagement.Models.CustomerModel> CustomerModel { get; set; } = default!;
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<LibraryManagement.Models.AssignBookViewModel> AssignBookViewModel { get; set; } = default!;
        public DbSet<LibraryManagement.Models.AdminModel> AdminModel { get; set; } = default!;
    }
}
