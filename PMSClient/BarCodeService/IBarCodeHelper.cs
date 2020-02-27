using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.BarCodeService
{
    public interface IBarCodeHelper
    {
        string CreateBarCodeImage(string s);
    }
}
