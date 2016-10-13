using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    /// <summary>
    /// 热压过程当中的记录
    /// </summary>
    public class RecordVHP
    {
        public Guid ID { get; set; }
        public List<VHPRecordItem> VHPRecords { get; set; }
        public string ExtraInformation { get; set; }
    }
}
