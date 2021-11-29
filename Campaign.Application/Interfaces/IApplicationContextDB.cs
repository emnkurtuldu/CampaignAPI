using Campaign.Domain.Models.Orders;
using Campaign.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Application.Interfaces
{
    public interface IApplicationContextDB
    {
        DbSet<Product> Products { get; set; }
        DbSet<Domain.Models.Campaigns.Campaign> Campaigns { get; set; }
        DbSet<Order> Orders { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
