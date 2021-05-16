using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace InventoryWebAPI
{
    public partial class ADContext : DbContext
    {
        public ADContext()
        {
        }

        public ADContext(DbContextOptions<ADContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InventoryTag> InventoryTag { get; set; }
        public virtual DbSet<ProductDefinition> ProductDefinition { get; set; }
        public virtual DbSet<ProductInventory> ProductInventory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AveryDennison;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryTag>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SgtinEpc)
                    .IsRequired()
                    .HasColumnName("SgtinEPC")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.InventoryTag)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Tag_Definition");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InventoryTag)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Tag_Product");
            });

            modelBuilder.Entity<ProductDefinition>(entity =>
            {
                entity.HasIndex(e => new { e.CompanyPrefix, e.ItemReference })
                    .HasName("UK_ProdDef_CmpnyPrfx_ItmRef")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.HasKey(e => e.InventoryId)
                    .HasName("PK__ProductI__F5FDE6B3F03B6B48");

                entity.Property(e => e.InventoryId).ValueGeneratedNever();

                entity.Property(e => e.InventoryDate).HasColumnType("datetime");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
