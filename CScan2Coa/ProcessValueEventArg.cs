using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportTargetPhotoIntoReport
{
    public class ProcessValueEventArg:EventArgs
    {
        public int Progress { get; set; }
    }
}
