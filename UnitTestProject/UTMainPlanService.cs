using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfService;

namespace UnitTestProject
{
    [TestClass]
    public class UTMainPlanService
    {
        private MainPlanService service;

        public UTMainPlanService()
        {
            service = new MainPlanService();
        }

        [TestMethod]
        public void TestGetMainPlansByMainOrderId()
        {
            var mainplans = service.GetMainPlansByMainOrderId(new Guid("d3a51f48-8979-40f1-a786-956896805a47"));
            Assert.IsNotNull(mainplans);
            //Assert.IsTrue(mainplans.Count > 0);
        }
    }
}
