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

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddDevice(Device device);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateDevice(Device device);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteDevice(Device device);
    }
}
