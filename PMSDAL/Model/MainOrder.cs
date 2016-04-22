using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PMSDAL.Model
{
    /// <summary>
    /// 主订单
    /// </summary>
    public class MainOrder
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime? OrderDate { get; set; }
        [Required]
        public string Customer { get; set; }
        [Required]
        public string MaterialName { get; set; }//规范订单名称

        public string MaterialNickName { get; set; }//订单内容的补充

        public string PO { get; set; }

        public string PMIWorkNumber { get; set; }//内部工作号，自动生成

        public string Purity { get; set; }//纯度，默认4N5

        public string Size { get; set; }//规范尺寸

        public string SizeDetails { get; set; }//尺寸细节

        public string Quantity { get; set; }//大部分是片，有时候是g或者kg

        public int Priority { get; set; }//订单优先级

        public string SampleRequirement { get; set; }//样品需求

        public string OrderState { get; set; }//订单状态，有效，取消，订单数据不做真实删除，只标记，虚拟删除

        public DateTime? DeliveryDateExpect { get; set; }//预计发货日期，默认设置订单创建日期后的一个月

        public string ConsigneeInformation { get; set; }//收货人信息

        public bool? IsPlanFinished { get; set; }//生产是否完成

        public bool? IsDeliveryFinished { get; set; }//发货是否完成

        public DateTime? DeliveryDateFact { get; set; }//实际的发货日期

        public string Remark { get; set; }

    }
}
