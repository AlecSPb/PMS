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
    public static class AccessDictionary
    {
        public static string ReadOrder { get; set; } = "浏览订单";
        public static string EditOrder { get; set; } = "编辑订单";
        public static string ReadOrderCheck { get; set; } = "浏览订单核验";
        public static string EditOrderCheck { get; set; } = "编辑订单核验";

        public static string ReadCustomer { get; set; } = "浏览材料需求";
        public static string EditCustomer { get; set; } = "编辑材料需求";

        public static string ReadOutSource{ get; set; } = "浏览材料需求";
        public static string EditOutSource { get; set; } = "编辑材料需求";

        public static string ReadMaterialNeed { get; set; } = "浏览材料需求";
        public static string EditMaterialNeed { get; set; } = "编辑材料需求";

        public static string ReadMaterialOrder { get; set; } = "浏览材料订单";
        public static string EditMaterialOrder { get; set; } = "编辑材料订单";
        public static string EditMaterialOrderItem { get; set; } = "编辑材料订单项";

        public static string ReadMaterialInventoryIn { get; set; } = "浏览材料入库";
        public static string EditMaterialInventoryIn { get; set; } = "编辑材料入库";

        public static string ReadMaterialInventoryOut { get; set; } = "浏览材料出库";
        public static string EditMaterialInventoryOut { get; set; } = "编辑材料出库";

        public static string ReadPlate { get; set; } = "浏览材料需求";
        public static string EditPlate { get; set; } = "编辑材料需求";

        public static string ReadMisson { get; set; } = "浏览任务";
        public static string ReadPlan { get; set; } = "浏览计划";
        public static string EditPlan { get; set; } = "编辑计划";


        public static string ReadMillingRecord { get; set; } = "浏览制粉记录";
        public static string EditMillingRecord { get; set; } = "编辑制粉记录";

        public static string ReadVHPRecord { get; set; } = "浏览制粉记录";
        public static string EditVHPRecord { get; set; } = "编辑制粉记录";

        public static string ReadMachineRecord { get; set; } = "浏览制粉记录";
        public static string EditMachineRecord { get; set; } = "编辑制粉记录";

        public static string ReadDeMoldRecord { get; set; } = "浏览制粉记录";
        public static string EditDeMoldRecord { get; set; } = "编辑制粉记录";

        public static string ReadTestRecord { get; set; } = "浏览制粉记录";
        public static string EditTestRecord { get; set; } = "编辑制粉记录";

        public static string ReadBondingRecord { get; set; } = "浏览制粉记录";
        public static string EditBondingRecord { get; set; } = "编辑制粉记录";

        public static string ReadProduct { get; set; } = "浏览制粉记录";
        public static string EditProduct { get; set; } = "编辑制粉记录";

        public static string ReadDelivery { get; set; } = "浏览制粉记录";
        public static string EditDelivery { get; set; } = "编辑制粉记录";
        public static string EditDeliveryItem { get; set; } = "编辑制粉记录";

        public static string ReadStatisticOrder { get; set; } = "浏览订单统计";
        public static string ReadStatisticPlan { get; set; } = "浏览计划统计";
        public static string ReadStatisticDelivery { get; set; } = "浏览发货统计";
        public static string ReadStatisticProduct { get; set; } = "浏览产品统计";


        public static string ReadFeedback{ get; set; } = "浏览制粉记录";
        public static string EditFeedback { get; set; } = "编辑制粉记录";












    }
}
