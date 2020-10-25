using System;
using System.Collections.Generic;

namespace Rosseti.Models
{
    public partial class SprErrorOscillograms
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public double Inaccuracy { get; set; }
        public int SprErrorId { get; set; }

        public virtual SprErrors SprError { get; set; }
    }
}
