using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assesment_1
{
    public class RunningCost
    {
        protected int kilometers { get; set; }
        protected double cost { get; set; }
        protected double KCost { get; set; } = 1.545;

        public void calcCost()
        {
            this.cost = this.kilometers * KCost;
        }
    }
}
