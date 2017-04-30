using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient
{
    /// <summary>
    /// 权限字符串，方便统一修改
    /// </summary>
    public static class AccessString
    {
        public static string ReadOrder { get; set; } = "浏览订单";
        public static string EditOrder { get; set; } = "编辑订单";
        public static string ReadOrderCheck { get; set; } = "浏览订单核验";
        public static string EditOrderCheck { get; set; } = "编辑订单核验";
        public static string ReadMisson { get; set; } = "浏览任务";
        public static string ReadPlan { get; set; } = "浏览计划";
        public static string EditPlan { get; set; } = "编辑计划";
        public static string ReadMaterialNeed { get; set; } = "浏览材料需求";
        public static string EditMaterialNeed { get; set; } = "编辑材料需求";
        public static string ReadMaterialOrder { get; set; } = "浏览材料订单";
        public static string EditMaterialOrder { get; set; } = "编辑材料订单";
        public static string EditMaterialOrderItem { get; set; } = "编辑材料订单项";

        public static string ReadStatisticOrder { get; set; } = "浏览订单统计";
        public static string ReadStatisticPlan { get; set; } = "浏览计划统计";
        public static string ReadStatisticDelivery { get; set; } = "浏览发货统计";
    }
}
