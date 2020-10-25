using System;
using System.Collections.Generic;

namespace Rosseti.Models
{
    public partial class SprErrors
    {
        public SprErrors()
        {
            SprErrorOscillograms = new HashSet<SprErrorOscillograms>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SprOscillogramId { get; set; }

        public virtual SprOscillograms SprOscillogram { get; set; }
        public virtual ICollection<SprErrorOscillograms> SprErrorOscillograms { get; set; }
    }
}
