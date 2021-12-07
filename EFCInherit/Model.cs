using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCInherit
{

    public partial class ItemContext : DbContext
    {
        public ItemContext() { }

        public ItemContext(DbContextOptions<ItemContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Item> Items { get; set; } = null!;

        public virtual DbSet<BomItem> BomItems { get; set; } = null!;

        public virtual DbSet<SLItem> SLItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EFCInherit;");
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(128);
            });

            modelBuilder.Entity<BomItem>(entity =>
            {
                entity.ToTable("Items_BomItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Sku).HasMaxLength(128);

                //entity.HasOne(d => d.IdNavigation)
                //    .WithOne(p => p.Items_BomItem)
                //    .HasForeignKey<Items_BomItem>(d => d.Id)
                //    .HasConstraintName("FK_BomItem_inherits_Item");

            });

            modelBuilder.Entity<SLItem>(entity =>
            {
                entity.ToTable("Items_SLItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SerLot).HasMaxLength(256);

                entity.HasOne(d => d.BomItem)
                    .WithMany(p => p.SLItems)
                    .HasForeignKey(d => d.BomItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BomItemSLItem");

                //entity.HasOne(d => d.IdNavigation)
                //    .WithOne(p => p.Items_SLItem)
                //    .HasForeignKey<Items_SLItem>(d => d.Id)
                //    .HasConstraintName("FK_SLItem_inherits_Item");
            });


        }

    }


    public abstract partial class Item
    {

        public Item()
        {
            Name = "Name";
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }

    public partial class BomItem : Item
    {
        public BomItem()
        {
            SLItems = new HashSet<SLItem>();
        }

        public string Sku { get; set; } = null!;
        //public int Id { get; set; }

        //public virtual Item IdNavigation { get; set; } = null!;
        public virtual ICollection<SLItem> SLItems { get; set; }
    }

    public partial class SLItem : Item
    {
        public SLItem()
        {
        }

        public string SerLot { get; set; } = null!;
        public int BomItemId { get; set; }
        //public int Id { get; set; }

        public virtual BomItem BomItem { get; set; } = null!;
        //public virtual Item IdNavigation { get; set; } = null!;
    }


}
