using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<CustomerLead> CustomerLeads { get; set; } = null!;
        public DbSet<CustomerLeadImage> CustomerLeadImages { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<CustomerLead>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).HasMaxLength(250);
                b.HasMany(x => x.Images).WithOne(i => i.CustomerLead).HasForeignKey(i => i.CustomerLeadId).OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<CustomerLeadImage>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Base64Data).IsRequired();
                b.Property(x => x.CreatedAt).IsRequired();
            });
        }
    }
}
