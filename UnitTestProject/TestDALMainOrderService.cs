using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLibrary;

namespace UnitTestProject
{
    [TestClass]
    public class TestDALMainOrderService
    {
        private IMainOrderService mainOrderService;
        public TestDALMainOrderService()
        {
            mainOrderService = new MainOrderServcie();
        }

        [TestMethod]
        public void TestGetAllMainOrders()
        {
            int dataAccount = mainOrderService.GetAllMainOrders().Count;
            Assert.IsTrue(dataAccount > 0);
        }

        [TestMethod]
        public void TestGetMainOrderById()
        {
            //这个guid是从数据库里直接复制的第一个数据项的id
            var obj = mainOrderService.GetOneMainOrderById(
                new Guid("9bfdcbb7-7fed-4dc0-a8bb-21842f58e659"));
            Assert.IsNotNull(obj);
        }

    }
}
