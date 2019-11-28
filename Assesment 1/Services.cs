using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assesment_1
{
    public class Services
    {
        public static int SERVICE_KILOMETER_LIMIT = 10000;

        private int lastServiceOdometerKm = 0;
        protected int serviceCount { get; set; } = 0;
        protected int lastServiceDate { get; set; }

        //public int getLastServiceOdometerKM() Not used in C#

        public void recordSerivce(int distance)
        {
            this.lastServiceOdometerKm = distance;
            this.serviceCount++;
        }

        //public int has been replaced

        public int getTotalScheduledServices()
        {
            //it Doesn't like 2 ints
            double serviceCheck = lastServiceOdometerKm;
            
            return (int) Math.Floor(serviceCheck / SERVICE_KILOMETER_LIMIT);
        }

    }
}
