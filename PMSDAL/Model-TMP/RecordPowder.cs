using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Developer:xs.zhou@outlook.com
    CreateTime:2016/4/25 12:14:53
*/
namespace PMSDAL.Model
{
    public class RecordPowder
    {
        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }//记录创建日期
        public string RecordPerson { get; set; }
        public string PowderTarget { get; set; }//制粉目标

        public string MaterialSource { get; set; }//材料来源
        public string MaterialProperty { get; set; }//材料特性


        public string PowderCondition { get; set; }//制粉条件

        public double MaterialWeight { get; set; }//原料重量
        public double PowderWeight { get; set; }//粉末重量
        public double LeftMaterial   { get; set; }//剩余原料

        public double Remark { get; set; }//备注

    }
}
