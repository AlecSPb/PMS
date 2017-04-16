using PMSClient.BasicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient
{
    /// <summary>
    /// 这里读取从服务而来的基础数据
    /// </summary>
    public static class BasicData
    {
        public static List<DcBDCustomer> Customers
        {
            get
            {
                using (var service = new CustomerServiceClient())
                {
                    return service.GetCustomer().OrderBy(i => i.CustomerName).ToList();
                }
            }
        }
        public static List<DcBDCompound> Compounds
        {
            get
            {
                using (var service = new CompoundServiceClient())
                {
                    return service.GetAllCompounds().OrderBy(i => i.MaterialName).ToList();
                }
            }
        }
        public static List<DcBDVHPDevice> VHPDevices
        {
            get
            {
                using (var service = new VHPDeviceServiceClient())
                {
                    return service.GetVHPDevice().OrderBy(i => i.CodeName).ToList();
                }
            }
        }
        public static List<DcBDVHPProcess> VHPProcesses
        {
            get
            {
                using (var service = new VHPProcessServiceClient())
                {
                    return service.GetVHPProcess().OrderBy(i => i.CodeName).ToList();
                }
            }
        }
        public static List<DcBDVHPMold> VHPMolds
        {
            get
            {
                using (var service = new VHPMoldServiceClient())
                {
                    return service.GetVHPMold().OrderBy(i => i.InnerDiameter).ToList();
                }
            }
        }
    }
}
