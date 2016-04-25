using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Developer:xs.zhou@outlook.com
    CreateTime:2016/4/25 11:51:33
*/
namespace PMSDAL.Model
{
    /// <summary>
    /// 用来存放上传的配料计算项名称
    /// </summary>
    public class CalculationCondition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Creater { get; set; }
        public int DownCount { get; set; }

    }
}
