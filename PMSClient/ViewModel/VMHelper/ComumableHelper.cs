using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel.VMHelper
{
    public class ComumableHelper
    {
        public static bool IsStrEmptyNull(string s,string msg="error")
        {
            if (string.IsNullOrEmpty(s))
            {
                XSHelper.XS.MessageBox.ShowWarning(msg);
                return false;
            }
            return true;
        }
    }
}
