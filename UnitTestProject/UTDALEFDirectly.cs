using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLibrary;
using System.Linq;
namespace UnitTestProject
{
    /// <summary>
    /// 直接测试DataAccessLibraryEF
    /// </summary>
    [TestClass]
    public class UTDALEFDirectly
    {
        private ProductionManagementModel db;

        public UTDALEFDirectly()
        {
            db = new ProductionManagementModel();
        }

        [TestMethod]
        public void TestEFMainOrder()
        {
            int dataCount = db.MainOrders.Count();
            Assert.IsTrue(dataCount>0);
        }

        [TestMethod]
        public void TestEFMainPlan()
        {
            int dataCount = db.MainPlans.Count();
            Assert.IsTrue(dataCount > 0);
        }
    }
}
