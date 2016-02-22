using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MainPlanService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 MainPlanService.svc 或 MainPlanService.svc.cs，然后开始调试。
    public class MainPlanService : IMainPlanService
    {
        private ProductionManagementModel dbcontext;
        public MainPlanService()
        {
            dbcontext = new ProductionManagementModel();
        }

        public List<MainPlan> GetMainPlansByMainOrderId(Guid id)
        {
            var query = from p in dbcontext.MainPlans
                        where p.MainOrderId == id
                        select new MainPlan()
                        {
                            MainPlanId=p.MainPlanId,
                            MainOrderId=p.MainOrderId,
                            VHPTime=p.VHPTime,
                            DeviceType=p.DeviceType,
                            ProcessCode=p.ProcessCode,
                            MoldDiameter=p.MoldDiameter,
                            Thickness=p.Thickness,
                            VHPQuantity=p.VHPQuantity,
                            Pressure=p.Pressure,
                            Temperature=p.Temperature,
                            Vaccum=p.Vaccum,
                            DensityCal=p.DensityCal,
                            PersonInCharge=p.PersonInCharge,
                            Remark=p.Remark
                        };
            return query.ToList();
        }

        public bool AddMainPlan(MainPlan plan)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMainPlan(MainPlan plan)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMainPlan(MainPlan plan)
        {
            throw new NotImplementedException();
        }

    }
}
