using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Model
{
    /// <summary>
    /// 材料库
    /// </summary>
    public class Material
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string MaterialName { get; set; }
        [Required]
        public double Density { get; set; }
        public double MeltingPoint { get; set; }
        public double BoilingPoint { get; set; }
        public string SpecialProperty { get; set; }//特别性质
        public DateTime UpdateTime { get; set; }//最后更新时间
        public string InformationSource { get; set; }//信息来源
        public string Remark { get; set; }

    }
}
