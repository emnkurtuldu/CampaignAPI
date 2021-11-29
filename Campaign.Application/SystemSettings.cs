using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application
{
    public static class SystemSettings
    {
        public static TimeSpan SystemTime { get; set; }

        public static int SystemTimeLinearHour
        {
            get
            {
                return Convert.ToInt32(SystemTime.TotalHours * 5); 
            }
        }
    }
}
