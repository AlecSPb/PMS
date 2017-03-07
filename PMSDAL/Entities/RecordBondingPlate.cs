using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 背板记录信息
    /// </summary>
    public class RecordBondingPlate
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public string CreateTime { get; set; }
        public string State { get; set; }

        //1.0背板检查
        public string PlateMaterial { get; set; }
        public string PlateLot { get; set; }//铜板ID号
        public string PlateSerialNumber { get; set; }//序列号
        public string PlateBelong { get; set; }//铜板归属
        public string PlateDimension { get; set; }
        public string PlateUseCount { get; set; }//使用次数
        public string PlateHardness { get; set; }//硬度
        public string PlateSuplier { get; set; }//供应商
        public string LastWeldMaterial { get; set; }//上次使用的焊接材料
        public string OtherRecord { get; set; }//其他记录
        public string PlateAppearance { get; set; }//外观情况
        //3.0背板前置处理
        public string PlateProcessRecord { get; set; }//前置处理结果检查记录
    }
}
