﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLibrary;
using System.Linq;
namespace UnitTestProject
{
    /// <summary>
    /// 直接测试DataAccessLibraryEF
    /// </summary>
    [TestClass]
    public class TestDataAccessLibraryEFDirectly
    {
        private ProductionManagementModel db;

        public TestDataAccessLibraryEFDirectly()
        {
            db = new ProductionManagementModel();
        }

        [TestMethod]
        public void TestDataEFWorking()
        {
            int dataCount = db.V_MainOrder.Count();
            Assert.IsTrue(dataCount>0);
            string s=db.V_MainOrder.FirstOrDefault().Customer;
            Assert.IsFalse(s.Length==0);
        }

    }
}
