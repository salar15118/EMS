using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EMS.Models;

public partial class EcommerceManagementSystemContext : DbContext
{
    public EcommerceManagementSystemContext()
    {
    }

    public EcommerceManagementSystemContext(DbContextOptions<EcommerceManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

//   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//       => optionsBuilder.UseNpgsql("Host=ep-sparkling-lake-a42hxocq-pooler.us-east-1.aws.neon.tech;Database=Ecommerce management system;Username=Ecommerce management system_owner;Password=npg_nKBqTjDi3W1p;SSL Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(8)
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(50)
                .HasColumnName("customer_address");
            entity.Property(e => e.CustomerContact).HasColumnName("customer_contact");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(25)
                .HasColumnName("customer_name");
            entity.Property(e => e.OrderId)
                .HasMaxLength(8)
                .HasColumnName("order_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Customers)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("customer_order_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .HasMaxLength(8)
                .HasColumnName("order_id");
            entity.Property(e => e.OrderDate)
                .HasMaxLength(10)
                .HasColumnName("order_date");

            entity.HasMany(d => d.Products).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("order_product_product_id_fkey"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("order_product_order_id_fkey"),
                    j =>
                    {
                        j.HasKey("OrderId", "ProductId").HasName("order_product_pkey");
                        j.ToTable("order_product");
                        j.IndexerProperty<string>("OrderId")
                            .HasMaxLength(8)
                            .HasColumnName("order_id");
                        j.IndexerProperty<string>("ProductId")
                            .HasMaxLength(8)
                            .HasColumnName("product_id");
                    });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.ProductId)
                .HasMaxLength(8)
                .HasColumnName("product_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(25)
                .HasColumnName("product_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
