using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PMSDAL
{
    /// <summary>
    /// 制粉记录
    /// </summary>
    public class RecordMillingHistory
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }
        //需要记录的信息
        public double RoomTemperature { get; set; }
        public double RoomHumidity { get; set; }
        public string PMINumber { get; set; }

        public string Composition { get; set; }
        public string VHPPlanLot { get; set; }
        public string MaterialSource { get; set; }//MaterialSource
        public string Remark { get; set; }
        public string MillingTool { get; set; }
        public string GasProtection { get; set; }
        public string GrainSize { get; set; }
        public double WeightIn { get; set; }
        public double WeightOut { get; set; }
        public double WeightRemain { get; set; }
        public double Ratio { get; set; }
        public string MillingTime { get; set; }
        //2017-10-25
        public string Oxygen { get; set; }
        public string Water { get; set; }

        //2017-12-15
        public string MeltingPoint { get; set; }
        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }


    }
}
