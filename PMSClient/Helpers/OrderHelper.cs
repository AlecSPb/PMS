using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Helpers
{
    public class OrderHelper
    {
        public static bool NeedSample(string sample)
        {
            return !(string.IsNullOrEmpty(sample) || sample.Contains("无需样品"));
        }
    }
}
