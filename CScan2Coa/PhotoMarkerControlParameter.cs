using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportTargetPhotoIntoReport
{
    public class PhotoMarkerControlParameter
    {
        public bool HasProductID { get; set; } = true;
        public bool HasComposition { get; set; } = true;
        public bool HasWeldingRation { get; set; } = true;

    }
}
