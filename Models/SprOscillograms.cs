using System;
using System.Collections.Generic;

namespace Rosseti.Models
{
    public partial class SprOscillograms
    {
        public SprOscillograms()
        {
            DateOscillograms = new HashSet<DateOscillograms>();
            RightOscillogramsStep = new HashSet<RightOscillogramsStep>();
            SprErrors = new HashSet<SprErrors>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Step { get; set; }

        public virtual ICollection<DateOscillograms> DateOscillograms { get; set; }
        public virtual ICollection<RightOscillogramsStep> RightOscillogramsStep { get; set; }
        public virtual ICollection<SprErrors> SprErrors { get; set; }
    }
}
