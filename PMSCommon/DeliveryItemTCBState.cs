using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSCommon
{
    /// <summary>
    /// 此状态用于DeliveryItemTCB
    /// </summary>
    public enum DeliveryItemTCBState
    {
        UnKnown,
        Paused,
        Broken,
        Enroute_TCB,
        Arrived_TCB,
        Bonding_TCB,
        Shipped_Otipcraft,
        Shipped_Customer,
    }
}
