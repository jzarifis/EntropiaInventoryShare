using EntropiaInventoryShareWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EntropiaInventoryShareWeb.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<InventorySharedItem> SharedItems { get; set; }

        public DbSet<Avatar> Avatars { get; set; }

        public DbSet<Item> Items { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<InventorySharedItem>()
                .HasOne(e => e.Avatar)
                .WithMany(e => e.SharedItems)
                .HasForeignKey(e => e.AvatarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InventorySharedItem>()
                .HasOne(e => e.Item)
                .WithMany(e => e.SharedItems)
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
