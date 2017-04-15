using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient
{
    /// <summary>
    /// 导航token
    /// 用来表示要显示的视图的token
    /// </summary>
    public enum VToken
    {
        Navigation,
        LogIn,
        Order,
        OrderEdit,
        OrderRefresh,
        OrderSelect,
        OrderCheck,
        OrderCheckRefresh,
        OrderCheckEdit,
        Misson,
        MissonRefresh,
        Plan,
        PlanEdit,
        PlanSelectForTest,
        RecordTest,
        RecordTestEdit,
        RecordTestSelect,
        Delivery,
        DeliveryEdit,
        DeliveryItem,
        DeliveryItemEdit,
        RecordMilling,
        RecordMillingEdit,
        RecordVHP,
        RecordVHPEdit,
        RecordDeMold,
        RecordDeMoldEdit,
        RecordMachine,
        RecordMachineEdit,
        RecordBonding,
        RecordBondingEdit,
        MaterialOrder,
        MaterialOrderEdit,
        MaterialOrderItemEdit,
        MaterialNeed,
        MaterialNeedEdit,
        MaterialNeedEdit2,
        MaterialNeedSelect,
        MaterialInventory,
        MaterialInventoryEdit,
        MaterialNeedRefresh,
        MaterialOrderRefresh,
        RecordTestRefresh,
        DeliveryRefresh,
        PlanSelectForVHP,
        RecordVHPRefresh,
        RecordVHPQuickEdit,
        SetRecordVHPQuickEditSelectIndex,
        MaterialOrderItemRefresh,
        DeliveryItemRefresh
    }
}
