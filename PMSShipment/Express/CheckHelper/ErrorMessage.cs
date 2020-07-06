using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSShipment.Express.CheckHelper
{
    public class ErrorMessage
    {
        public ErrorMessage()
        {
            Errors = new List<string>();
        }
        public string Item { get; set; }
        public List<string> Errors { get; set; }
    }
}
