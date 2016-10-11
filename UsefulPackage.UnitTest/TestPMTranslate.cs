using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UsefulPackage.UnitTest
{
    [TestClass]
    public class TestPMTranslate
    {
        [TestMethod]
        public void TargetLotWith3Parameter()
        {
            DateTime vhpDate = new DateTime(2016, 4, 5);
            string vhpDeviceCode = "M";
            int vhpCount = 2;

            string expected = "160405-M-1,2";
            string actual = UsefulPackage.PMTranslate.GetTargetLot(vhpDate, vhpDeviceCode, vhpCount);

            Assert.AreEqual(expected, actual);



        }

        [TestMethod]
        public void TargetLotWith2Parameter()
        {
            DateTime vhpDate = new DateTime(2016, 4, 5);
            string vhpDeviceCode = "M";
            string expected = "160405-M-1";
            string actual= UsefulPackage.PMTranslate.GetTargetLot(vhpDate, vhpDeviceCode);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TargetLotWith1Parameter()
        {
            DateTime vhpDate = new DateTime(2016, 4, 5);
            string expected = "160405-unknown-1";
            string actual = UsefulPackage.PMTranslate.GetTargetLot(vhpDate);

            Assert.AreEqual(expected, actual);
        }

    }
}
