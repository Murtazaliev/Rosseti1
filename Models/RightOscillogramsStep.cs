using System;
using System.Collections.Generic;

namespace Rosseti.Models
{
    public partial class RightOscillogramsStep
    {
        public int Id { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public int SprOscillogramId { get; set; }

        public virtual SprOscillograms SprOscillogram { get; set; }
    }
}
