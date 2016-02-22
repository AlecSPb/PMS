using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfService;

namespace UnitTestProject
{
    [TestClass]
    public class UTMainOrderService
    {
        private MainOrderService service;

        [TestInitialize]
        public void Initial()
        {
            service = new MainOrderService();
        }

        [TestMethod]
        public void TestGetAllMainOrders()
        {
            var mainOrderList = service.GetAllMainOrders();
            Assert.IsNotNull(mainOrderList);
            Assert.IsTrue(mainOrderList.Count >0);
        }

    }
}
