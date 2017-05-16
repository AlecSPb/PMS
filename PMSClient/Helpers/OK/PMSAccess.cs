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
    public static class PMSAccess
    {
        public static string ReadOrder { get; set; } = "浏览订单";
        public static string EditOrder { get; set; } = "编辑订单";
        public static string ReadOrderCheck { get; set; } = "浏览订单核验";
        public static string EditOrderCheck { get; set; } = "编辑订单核验";

        public static string ReadCustomer { get; set; } = "浏览客户记录";
        public static string EditCustomer { get; set; } = "编辑客户记录";

        public static string ReadOutSource { get; set; } = "浏览外购记录";
        public static string EditOutSource { get; set; } = "编辑外购记录";

        public static string ReadMaterialNeed { get; set; } = "浏览原料需求";
        public static string EditMaterialNeed { get; set; } = "编辑原料需求";

        public static string ReadMaterialOrder { get; set; } = "浏览原料订单";
        public static string EditMaterialOrder { get; set; } = "编辑原料订单";
        public static string EditMaterialOrderItem { get; set; } = "编辑原料订单项";

        public static string ReadMaterialInventoryIn { get; set; } = "浏览原料库存";
        public static string EditMaterialInventoryIn { get; set; } = "编辑原料库存";

        public static string ReadMaterialInventoryOut { get; set; } = "浏览原料库存";
        public static string EditMaterialInventoryOut { get; set; } = "编辑原料库存";

        public static string ReadPlate { get; set; } = "浏览背板记录";
        public static string EditPlate { get; set; } = "编辑背板记录";

        public static string ReadMisson { get; set; } = "浏览任务";
        public static string ReadPlan { get; set; } = "浏览计划安排";
        public static string EditPlan { get; set; } = "编辑计划安排";


        public static string ReadRecordMilling { get; set; } = "浏览制粉记录";
        public static string EditRecordMilling { get; set; } = "编辑制粉记录";

        public static string ReadRecordVHP { get; set; } = "浏览热压记录";
        public static string EditRecordVHP { get; set; } = "编辑热压记录";

        public static string ReadRecordMachine { get; set; } = "浏览加工记录";
        public static string EditRecordMachine { get; set; } = "编辑加工记录";

        public static string ReadRecordDeMold { get; set; } = "浏览取模记录";
        public static string EditRecordDeMold { get; set; } = "编辑取模记录";

        public static string ReadRecordTest { get; set; } = "浏览测试记录";
        public static string EditRecordTest { get; set; } = "编辑测试记录";

        public static string ReadRecordBonding { get; set; } = "浏览绑定记录";
        public static string EditRecordBonding { get; set; } = "编辑绑定记录";

        public static string ReadProduct { get; set; } = "浏览成品记录";
        public static string EditProduct { get; set; } = "编辑成品记录";

        public static string ReadDelivery { get; set; } = "浏览发货记录";
        public static string EditDelivery { get; set; } = "编辑发货记录";
        public static string EditDeliveryItem { get; set; } = "编辑发货项记录";

        public static string ReadStatisticOrder { get; set; } = "浏览订单统计";
        public static string ReadStatisticPlan { get; set; } = "浏览计划统计";
        public static string ReadStatisticDelivery { get; set; } = "浏览发货统计";
        public static string ReadStatisticProduct { get; set; } = "浏览产品统计";

        public static string CanDocDeliverySheet { get; set; } = "生成发货单标签";
        public static string CanDocRecordTest { get; set; } = "生成测试报告";


        public static string ReadMaintenance { get; set; } = "浏览维护计划";

        public static string CanOutput { get; set; } = "数据导出";
        public static string ReadFeedback { get; set; } = "浏览客户反馈";
        public static string EditFeedback { get; set; } = "编辑客户反馈";

        public static string CanDebug { get; set; } = "调试";










    }
}
