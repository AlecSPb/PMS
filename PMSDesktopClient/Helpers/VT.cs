using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDesktopClient
{
    /// <summary>
    /// 导航token
    /// </summary>
    public enum VT
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
        MaterialNeedSelect
    }
}
