using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assesment_1
{
    public class Journey
    {
        protected double kilometers { get; set; }

        public void addKilometers(double kilometers)
        {
            this.kilometers += kilometers;
        }
    }
}
