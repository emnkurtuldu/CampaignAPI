using Campaign.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application
{
    public static class CalculatorService
    {
        public static int CalculateManipulatedPrice(Domain.Models.Campaigns.Campaign campaign, Product product)
        {
            var manipulatedPrice = product.Price;
            var systemTime = SystemSettings.SystemTime;
            if (campaign != null && campaign.IsActive)
            {
                if (campaign.Limit >= SystemSettings.SystemTimeLinearHour && systemTime.TotalHours < campaign.Duration)
                {
                    manipulatedPrice -= Convert.ToInt32(SystemSettings.SystemTimeLinearHour);
                }
                    
            }
            return manipulatedPrice;
        }
    }
}
