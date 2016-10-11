using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class VHPRecordSheet
    {
        public Guid ID { get; set; }
        public List<VHPRecordItem> VHPRecords { get; set; }
        public string ExtraInformation { get; set; }
    }
}
