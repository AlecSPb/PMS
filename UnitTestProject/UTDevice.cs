using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfService;

namespace UnitTestProject
{
    [TestClass]
    public class UTDevice
    {
        private DeviceService service;

        [TestInitialize]
        public void Initial()
        {
            service = new DeviceService();
        }


        [TestMethod]
        public void TestGetAllDevices()
        {
            var devices = service.GetAllDevices();
            Assert.IsNotNull(devices);
            Assert.IsTrue(devices.Count > 0);
        }

        [TestMethod]
        public void TestAddDevice()
        {
            int deviceCountBeforeAdd = service.GetAllDevices().Count;

            WcfService.Model.Device device = new WcfService.Model.Device();
            device.DeviceId =Guid.NewGuid();
            device.DeviceName = "TestDevice";
            device.DeviceCode = "TestDeviceCode";
            device.TopTemperature = 100;
            device.TopPressure = 23;
            device.TopDiameter = 440;
            device.Remark = "TestRemark";

            bool result = service.AddDevice(device);

            Assert.IsTrue(result);

            int deviceCountAfterAdd = service.GetAllDevices().Count;
            Assert.IsTrue(deviceCountAfterAdd > deviceCountBeforeAdd);

            service.DeleteDevice(device);

        }


    }
}
