using Campaign.Domain.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Campaign.Domain.Models.Products
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; internal set; }
        public string ProductCode { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        [JsonIgnore]
        public ICollection<Campaigns.Campaign> Campaigns { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }
    }
}
