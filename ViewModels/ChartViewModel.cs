using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosseti.ViewModels
{
    public class ChartViewModel
    {
        public List<double> dataMax { get; set; }
        public List<double> dataMin { get; set; }
        public double[] dataVal { get; set; }
    }
}
