using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLibrary;
using System.Linq;
namespace UnitTestProject
{
    [TestClass]
    public class UnitDataAccessLibrary
    {
        private ProductionManagementModel db;
        public UnitDataAccessLibrary()
        {
            db = new ProductionManagementModel();
        }

        [TestMethod]
        public void TestDataAccessOne()
        {
            int dataCount = db.View_MainOrders.Count();
            Assert.IsTrue(dataCount>0);
        }
    }
}
