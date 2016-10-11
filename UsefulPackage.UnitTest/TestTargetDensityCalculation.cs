using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UsefulPackage.UnitTest
{
    [TestClass]
    public class TestTargetDensityCalculation
    {
        [TestMethod]
        public void TargetDensityArchemedesWayNormalWay()
        {
            double w1 = 120;
            double w2 = 90;
            double theoryDensity = 5.67;

            TargetDensityResult result = TargetDensityCalculation.GetDensityInArchimedesWay(w1, w2, theoryDensity);

            double expected1 = 4;
            double expected2 = 0.70546737;

            Assert.AreEqual(expected1, result.CalculateDensity, 0.0002);
            Assert.AreEqual(expected2, result.RelativeDensity, 0.0002);
        }

        [TestMethod]
        public void TargetDensityArchemedesWayNoTheoryDensity()
        {
            double w1 = 120;
            double w2 = 90;

            TargetDensityResult result = TargetDensityCalculation.GetDensityInArchimedesWay(w1, w2);

            double expected1 = 4;
            double expected2 = 0.8;

            Assert.AreEqual(expected1, result.CalculateDensity, 0.0002);
            Assert.AreEqual(expected2, result.RelativeDensity, 0.0002);
        }

        [TestMethod]
        public void TargetDensityCalculationWayWithoutPaper()
        {
            TargetDensityCalculationItem item = new TargetDensityCalculationItem();
            item.TargetWeight = 950;
            item.PaperThickness = 0;
            item.PaperWeight = 0;
            item.OD = new double[] { 230.02, 230.00 };
            item.H = new double[] { 4.00, 3.98, 4.00, 4.02 };
            item.TheoryDensity = 5.75;

            TargetDensityResult result = TargetDensityCalculation.GetDensityInCalculationWay(item);

            Assert.AreEqual(5.71584, result.CalculateDensity, 0.02);
            Assert.AreEqual(0.99406, result.RelativeDensity, 0.02);
        }

        [TestMethod]
        public void TargetDensityCalculationWayWithPaper()
        {
            TargetDensityCalculationItem item = new TargetDensityCalculationItem();
            item.TargetWeight = 982;
            item.PaperThickness = 0.5;
            item.PaperWeight = 32;
            item.OD = new double[] { 230.02, 230.00 };
            item.H = new double[] { 4.50, 4.48, 4.50, 4.52 };
            item.TheoryDensity = 5.75;

            TargetDensityResult result = TargetDensityCalculation.GetDensityInCalculationWay(item);

            Assert.AreEqual(5.71584, result.CalculateDensity, 0.02);
            Assert.AreEqual(0.99406, result.RelativeDensity, 0.02);
        }

    }
}
