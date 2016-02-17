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
    public class TestDALEFDirectly
    {
        private ProductionManagementModel db;

        public TestDALEFDirectly()
        {
            db = new ProductionManagementModel();
        }

        [TestMethod]
        public void TestDataEFWorking()
        {
            int dataCount = db.V_MainOrder.Count();
            Assert.IsTrue(dataCount>0);
        }

    }
}
