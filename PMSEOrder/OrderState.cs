using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSEOrder
{
    public enum OrderState
    {
        Deleted,
        UnFinished,
        UnChecked,
        UnSend,
        Sent,
        Received
    }
}
