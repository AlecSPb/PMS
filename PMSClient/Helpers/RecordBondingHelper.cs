using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.Helpers
{
    public class RecordBondingHelper
    {
        public int CheckBondingPlateUsedTime(string platelot)
        {
            string platelotWithoutPostfix = platelot.TrimEnd(new char[] { 'A' });
            using (var service=new RecordBondingServiceClient())
            {
                return service.CheckPlateUsedTimes(platelotWithoutPostfix);
            }
        }
    }
}
