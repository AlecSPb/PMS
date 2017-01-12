using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSCommon
{
    public enum ModelState
    {
        Deleted = -1,
        Paused = 0,
        UnCompleted=1,
        Completed=2
    }
}