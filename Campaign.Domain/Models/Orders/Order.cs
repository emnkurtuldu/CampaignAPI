using Campaign.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Campaign.Domain.Models.Orders
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; internal set; }
        public DateTime CreatedDate { get; set; }
        public int Quantity { get; set; }
        public int SalesPrice { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        [JsonIgnore]
        public virtual Campaigns.Campaign Campaign { get; set; }

    }
}
