using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    /// <summary>
    /// 热压工艺记录单
    /// 每次热压，每个热压机对应一个记录单
    /// 生成Word格式的热压记录单的时候使用
    /// </summary>
    public class VHPProcessRecordSheet
    {
        public Guid ID { get; set; }
        //热压机编号
        public string DeviceCode { get; set; }
        //当前模具
        public VHPMold CurrentMold { get; set; }
        //当前热压条件
        public VHPCondition CurrentVHPCondition { get; set; }





    }
}
