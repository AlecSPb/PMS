using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSDAL.Model
{
    /// <summary>
    /// 靶材产品
    /// </summary>
    public class Target
    {

        public Guid Id { get; set; }
        [Required]
        public string Material { get; set; }//材料名称
        [Required]
        public string Lot { get; set; }//Lot
        public string MaterialAbbr { get; set; }//材料缩写

        public string Size { get; set; }//靶材要求尺寸
        public string Customer { get; set; }//客户名称
        public string PO { get; set; }//PO
        public string Density { get; set; }//密度
        public string Weight { get; set; }//靶材重量
        public string Resistance { get; set; }//电阻率
        public string XRFComposition { get; set; }//XRF成分
        public string Dimension { get; set; }//尺寸
        public DateTime CreateTime { get; set; }//创建日期
        public DateTime OrderTime { get; set; }//订单日期
        public string State { get; set; }//订单状态，显示，隐藏，删除
        public string Remark { get; set; }

    }
}