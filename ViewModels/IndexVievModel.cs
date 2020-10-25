using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rosseti.Models;

namespace Rosseti.ViewModels
{
    public class IndexVievModel
    {
        public IEnumerable<RightOscillogramsStep> rightOscillogramsSteps { get; set; }
        public IEnumerable<DateOscillograms> DateOscillograms { get; set; }
        public IEnumerable<SprErrors> SprErrors { get; set; }
    }
}
