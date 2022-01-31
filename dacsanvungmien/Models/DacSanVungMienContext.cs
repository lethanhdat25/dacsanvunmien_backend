using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using dacsanvungmien.Dtos;

#nullable disable

namespace dacsanvungmien.Models
{
    public partial class DacSanVungMienContext : DbContext
    {
        public DacSanVungMienContext(DbContextOptions<DacSanVungMienContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<Region> Region { get; set; }

        //Config data trong Startup
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DATLEE\\SQLEXPRESS;Database=DacSanVungMien;Trusted_Connection=True;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<dacsanvungmien.Dtos.ProductDto> ProductDto { get; set; }

        public DbSet<dacsanvungmien.Dtos.CartDto> CartDto { get; set; }

        public DbSet<dacsanvungmien.Dtos.BillDto> BillDto { get; set; }

        public DbSet<dacsanvungmien.Dtos.RegionDto> RegionDto { get; set; }

        public DbSet<dacsanvungmien.Dtos.ProductImageDto> ProductImageDto { get; set; }
    }
}
