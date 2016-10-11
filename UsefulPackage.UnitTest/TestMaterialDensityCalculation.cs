using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulPackage.UnitTest
{
    [TestClass]
    public class TestMaterialDensityCalculation
    {
        [TestMethod]
        public void TestWeightingDensityCalculation()
        {
            List<MaterialDensityCalculationItem> lists = new List<MaterialDensityCalculationItem>()
            {
                new MaterialDensityCalculationItem() {ElementName="Cu",At=0.5,Density=8,MoleWeight=64 },
                new MaterialDensityCalculationItem() {ElementName="Fe",At=0.5,Density=7 ,MoleWeight=60}
            };
            MaterialDensityCalculation calculation = new MaterialDensityCalculation();

            double result = calculation.WeightingDensity(lists);

            Assert.AreEqual(7.516,result,0.02);
        }
    }
}
