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
        public ItemContext()
        {
        }

        public ItemContext(DbContextOptions<ItemContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Item> Items { get; set; } = null!;

        public virtual DbSet<BomItem> BomItems { get; set; } = null!;

        public virtual DbSet<SLItem> SLItems { get; set; } = null!;

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

        public virtual Item IdNavigation { get; set; } = null!;
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
        public virtual Item IdNavigation { get; set; } = null!;
    }


}
