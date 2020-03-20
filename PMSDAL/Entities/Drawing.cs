using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// Drawing
    ///存放图纸
    /// </summary>
    public class Drawing : ModelBase
    {
        public string DrawingName { get; set; }
        public string DrawingType { get; set; }
        public string Customer { get; set; }
        public string MainDimension { get; set; }
        public string ExtraDimension { get; set; }
        public string Remark { get; set; }

        public string FileName { get; set; }
        public Guid DrawID { get; set; }
    }
}
