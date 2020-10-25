using System;
using System.Collections.Generic;

namespace Rosseti.Models
{
    public partial class DateOscillograms
    {
        public DateOscillograms()
        {
            Oscillograms = new HashSet<Oscillograms>();
        }

        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int SprOscillogramId { get; set; }

        public virtual SprOscillograms SprOscillogram { get; set; }
        public virtual ICollection<Oscillograms> Oscillograms { get; set; }
    }
}
