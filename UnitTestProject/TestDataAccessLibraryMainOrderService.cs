using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLibrary;

namespace UnitTestProject
{
    [TestClass]
    public class TestDataAccessLibraryMainOrderService
    {
        private IMainOrderService mainOrderService;
        public TestDataAccessLibraryMainOrderService()
        {
            mainOrderService = new MainOrderServcie();
        }

        [TestMethod]
        public void TestGetAllMainOrders()
        {
            int dataAccount = mainOrderService.GetAllMainOrders().Count;
            Assert.IsTrue(dataAccount > 0);
        }


    }
}
