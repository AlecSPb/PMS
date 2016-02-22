using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;
using DataAccessLibrary;

namespace WcfService
{
    public class DeviceService : IDeviceService
    {
        private ProductionManagementModel dbcontext;
        public DeviceService()
        {
            dbcontext = new ProductionManagementModel();
        }

        public bool AddDevice(Device device)
        {
            try
            {
                DataAccessLibrary.Model.Device tmpdevice = new DataAccessLibrary.Model.Device()
                {
                    DeviceId=device.DeviceId,
                    DeviceName=device.DeviceName,
                    DeviceCode=device.DeviceCode,
                    TopTemperature=device.TopTemperature,
                    TopPressure=device.TopPressure,
                    TopDiameter=device.TopDiameter,
                    Remark=device.Remark
                };
                dbcontext.Devices.Add(tmpdevice);
                dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteDevice(Device device)
        {
            try
            {
                DataAccessLibrary.Model.Device deleteDevice = dbcontext.Devices.Find(device.DeviceId);
                if (deleteDevice==null)
                {
                    return false;
                }
                dbcontext.Devices.Remove(deleteDevice);
                dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Device> GetAllDevices()
        {
            var query = from d in dbcontext.Devices
                        select new Device()
                        {
                            DeviceId=d.DeviceId,
                            DeviceName=d.DeviceName,
                            DeviceCode=d.DeviceCode,
                            TopTemperature=d.TopTemperature,
                            TopPressure=d.TopPressure,
                            TopDiameter=d.TopDiameter,
                            Remark=d.Remark
                        };
            return query.ToList();
        }

        public bool UpdateDevice(Device device)
        {
            throw new NotImplementedException();
        }
    }
}
