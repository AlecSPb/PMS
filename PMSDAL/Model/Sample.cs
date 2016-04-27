using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSDAL.Model
{
    /// <summary>
    /// 样品产品
    /// </summary>
    public class Sample
    {

        public Guid Id { get; set; }
        [Required]
        public string Material { get; set; }//材料
        [Required]
        public string Lot { get; set; }//Lot

        public string Customer { get; set; }//客户名称
        public string PO { get; set; }
        public string Weight1 { get; set; }
        public string Weight2 { get; set; }
        public string Weight3 { get; set; }
        public string Weight4{ get; set; }
        public string ForTarget { get; set; }

        public string State { get; set; }

        public DateTime OrderDate { get; set; }//订单日期
        public DateTime CreateDate { get; set; }//报告创建日期
        public string Remark { get; set; }

    }
}