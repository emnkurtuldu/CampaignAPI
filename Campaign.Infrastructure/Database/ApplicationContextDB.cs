using Campaign.Application.Interfaces;
using Campaign.Domain.Models.Orders;
using Campaign.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Infrastructure.Database
{
    public class ApplicationContextDB : DbContext, IApplicationContextDB
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Domain.Models.Campaigns.Campaign> Campaigns { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationContextDB(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Product>().HasMany(x => x.Campaigns).WithOne(x => x.Product);
            modelBuilder.Entity<Product>().HasMany(x => x.Orders).WithOne(x => x.Product);
            modelBuilder.Entity< Domain.Models.Campaigns.Campaign>().HasMany(x => x.Orders).WithOne(x => x.Campaign);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
