using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Developer:xs.zhou@outlook.com
    CreateTime:2016/4/25 11:47:32
*/
namespace PMSDAL.Model
{
    /// <summary>
    /// 热压报告记录
    /// </summary>
    public class RecordVHP
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid MainPlanId { get; set; }//要记录的热压计划Id
        public DateTime RecordTime { get; set; }//记录日期
        public string RecordPerson { get; set; }//记录人

        /// <summary>
        /// 温度
        /// </summary>
        public string Temperature1 { get; set; }
        public string Temperature2 { get; set; }
        public string Temperature3 { get; set; }

        public string Pressure { get; set; }//压力
        public string Vacumm { get; set; }//真空度

        public string Remark { get; set; }//备注
    }
}
