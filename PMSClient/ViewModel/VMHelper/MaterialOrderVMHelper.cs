using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel.VMHelper
{
    public class MaterialOrderVMHelper
    {
        public static ErrorMessage CheckTheComposition(string s)
        {
            if (s.Contains("Se") && s.Contains("As") && s.Contains("Ge"))
            {
                if (s.Contains("Si"))
                {
                    return new ErrorMessage { IsOK = false, Message = "不能订购含Si的SeAsGe类材料，由PMI自行加入" };
                }
                if (s.Contains("B"))
                {
                    return new ErrorMessage { IsOK = false, Message = "不能订购含B的SeAsGe类材料，由PMI自行加入" };
                }
                if (s.Contains("C"))
                {
                    return new ErrorMessage { IsOK = false, Message = "不能订购含C的SeAsGe类材料，由PMI自行加入" };
                }
            }



            return new ErrorMessage { IsOK = true };
        }
    }
}
