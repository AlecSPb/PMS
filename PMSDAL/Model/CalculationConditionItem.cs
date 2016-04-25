using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Developer:xs.zhou@outlook.com
    CreateTime:2016/4/25 11:51:44
*/
namespace PMSDAL.Model
{
    /// <summary>
    /// 用来存放上传的配料计算项
    /// </summary>
    public class CalculationConditionItem
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string MaterialName { get; set; }
        public double MoleWeight { get; set; }
        public double At { get; set; }

    }
}
