using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Required]
        public string Creater { get; set; }
        [Required]
        public int DownCount { get; set; }

    }
}
