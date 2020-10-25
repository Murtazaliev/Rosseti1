using System;
using System.Collections.Generic;

namespace Rosseti.Models
{
    public partial class Oscillograms
    {
        public int Id { get; set; }
        public double Time { get; set; }
        public double Value { get; set; }
        public int DateOscillogramId { get; set; }

        public virtual DateOscillograms DateOscillogram { get; set; }
    }
}
