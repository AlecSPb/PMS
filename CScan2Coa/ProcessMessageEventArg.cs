using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportTargetPhotoIntoReport
{
    public class ProcessMessageEventArg:EventArgs
    {
        public string Message { get; set; }
    }
}
