using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackingPlateLotManager.Model
{
    public class PlateLot
    {
        public int Id { get; set; }
        public string Lot { get; set; }
        public DateTime UsedTime { get; set; }
        public bool IsUsed { get; set; }
    }
}
