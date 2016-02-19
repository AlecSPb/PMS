using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    [ServiceContract]
    public interface IDeviceService
    {
        /// <summary>
        /// 得到所有设备信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Device> GetAllDevices();


        [OperationContract]
        bool AddDevice(Device device);
        [OperationContract]
        bool UpdateDevice(Device device);
        [OperationContract]
        bool DeleteDevice(Device device);
    }
}
