using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDesktopClient
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
        OrderSelect,
        OrderCheck,
        OrderCheckEdit,
        Misson,
        Plan,
        PlanEdit,
        PlanSelect,
        RecordTestResult,
        RecordTestResultEdit,
        RecordTestSelect,
        RecordDelivery,
        RecordDeliveryEdit,
        RecordDeliveryItem,
        RecordDeliveryItemEdit,
        RecordMilling,
        RecordMillingEdit,
        RecordVHP,
        RecordVHPEdit,
        RecordTakeOut,
        RecordTakeOutEdit,
        RecordMachine,
        RecordMachineEdit,
        RecordBonding,
        RecordBondingEdit,
        MaterialOrder,
        MaterialOrderEdit,
        MaterialOrderItemEdit,
        MaterialNeed,
        MaterialNeedEdit,
        MaterialNeedSelect,
        MaterialInventory,
        MaterialInventoryEdit
    }
}
