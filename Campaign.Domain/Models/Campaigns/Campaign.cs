using Campaign.Domain.Models.Orders;
using Campaign.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Campaign.Domain.Models.Campaigns
{
    public class Campaign
    {
        public Campaign()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; internal set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Limit { get; set; }
        public int TargetSalesCount { get; set; }
        public bool IsActive { get; set; }
        public int? TotalSales
        {
            get
            {
                return this.Orders != null ? this.Orders.Select(s => s.Quantity).Sum() : 0;
            }
        }
        public int? AverageItemPrice
        {
            get
            {
                var averageItemPrice = (this.Orders?.Select(s => s.SalesPrice / this.Orders.Count).Sum());
                return averageItemPrice != null ? Convert.ToInt32(this.Orders.Select(s => s.SalesPrice / this.Orders.Count).Sum()) : null;
            }
        }

        public int? Turnover 
        {
            get
            {
                return this.Orders != null ? this.Orders.Select(s => this.AverageItemPrice * s.Quantity).Sum() : 0;
            }
        }

        [JsonIgnore]
        public Product Product { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

    }
}
