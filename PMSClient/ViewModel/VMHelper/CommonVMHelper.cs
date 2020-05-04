using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PMSClient.ViewModel.VMHelper
{
    public class CommonVMHelper
    {
        public static bool CheckPMINumber(string pminumber)
        {
            var regex = new Regex(@"^CD\d{6}-\w{1}$");
            var checkresult = regex.Match(pminumber);
            if (!checkresult.Success)
            {
                XSHelper.XS.MessageBox.ShowWarning("内部号建议最好是CD200405-A格式");
                return false;
            }
            return true;
        }
    }
}
