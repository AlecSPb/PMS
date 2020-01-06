using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryOfCScanImage.Service
{
    public class ProcessParameter
    {
        public string ImageFolder { get; set; }
        public string OutputFolder { get; set; }
        public bool OpenTheDocument { get; set; }
        public bool ShowProcessDetails { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
