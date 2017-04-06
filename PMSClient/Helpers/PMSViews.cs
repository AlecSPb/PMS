using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient
{
    /// <summary>
    /// 新客户端视图Token集合
    /// </summary>
    public enum PMSViews
    {
        LogIn,
        Navigation,
        Order,
        OrderEdit,
        OrderCheck,
        OrderCheckEdit,
        OrderSelect,
        MaterialNeed,
        MaterialNeedEdit,
        MaterialOrder,
        MaterialOrderEdit,
        MaterialOrderItemEdit,
        MaterialInventory,
        MaterialInventoryEdit,
        Misson,
        Plan,
        PlanSelect, 
        PlanEdit,
        RecordMilling,
        RecordMillingEdit,
        RecordVHP,
        RecordVHPQuickEdit,
        RecordDeMold,
        RecordDeMoldEdit,
        RecordMachine,
        RecordMachineEdit,
        RecordTest,
        RecordTestEdit,
        RecordTestSelect,
        RecordDelivery,
        RecordDeliveryEdit,
        RecordDeliveryItemEdit,
        RecordBonding,
        RecordBondingEdit,
        RecordBondingTargetEdit,
        RecordBondingPlateEdit,
        Maintanence,
        MaintanenceEdit
    }
}
