using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTransferFromOldToNew
{
    public class DBTransfer:IDisposable
    {
        private PMSEntities newDb;
        private db_newEntities oldDb;
        private ProductsEntities1 productDb;

        public DBTransfer()
        {
            newDb = new PMSEntities();
            oldDb = new db_newEntities();
            productDb = new ProductsEntities1();
        }

        public void Dispose()
        {
            newDb.Dispose();
            oldDb.Dispose();
            productDb.Dispose();
        }

        public void TransferOrderPlan()
        {
            var oldOrder = oldDb.tb_Order.ToList();
            foreach (var order in oldOrder)
            {
                var orderId = Guid.NewGuid();

                var newOrder = new PMSOrders();
                newOrder.ID = orderId;
                newOrder.CustomerName = order.Customer;
                newOrder.PO = order.PO;
                newOrder.PMINumber = order.PMIWorkNumber;
                newOrder.CompositionStandard = order.ProductName;
                newOrder.CompositionOriginal = order.ProductName;
                newOrder.CompositionAbbr= "";
                newOrder.ProductType = order.ProductType;
                newOrder.Purity = order.Purity;
                newOrder.Quantity = order.Number??0;
                newOrder.QuantityUnit = order.Unit;
                newOrder.Dimension = order.Dimension;
                newOrder.DimensionDetails = "无";
                newOrder.SampleNeed = "";
                newOrder.DeadLine = order.SendDateNeed??new DateTime(2017,4,16);
                newOrder.MinimumAcceptDefect = "通常";
                newOrder.Remark = order.OrderMemo;
                newOrder.Priority = "普通";
                newOrder.State =order.IsFinished==true?"完成":"未完成";
                newOrder.StateRemark = "";
                newOrder.CreateTime = order.OrderDate;
                newOrder.Reviewer = "yr.hu";
                newOrder.ReviewTime = order.OrderDate;
                newOrder.Creator= "yr.hu";
                newOrder.PolicyType = "VHP";

                newDb.PMSOrders.Add(newOrder);
                //获取对应OrderID的Plans
                #region Plan
                var plans = oldDb.tb_Plan.Where(p => p.OrderID == order.OrderID).ToList();
                foreach (var plan in plans)
                {
                    var planId = Guid.NewGuid();
                    var newPlan = new PMSPlanVHPs();
                    newPlan.ID = planId;
                    newPlan.OrderID = orderId;
                    newPlan.CreateTime = plan.VHPTimePlan;
                    newPlan.Creator = "f.liang";
                    newPlan.PlanDate = plan.VHPTimePlan;
                    newPlan.PlanLot = 1;
                    newPlan.VHPDeviceCode = plan.DeviceType;
                    newPlan.MoldType = "高强";
                    newPlan.MoldDiameter = plan.MoldMD ?? 0;
                    newPlan.Thickness = plan.PressThick ?? 0;
                    newPlan.CalculationDensity = plan.DensityCal ?? 0;
                    newPlan.Quantity = plan.PressNum ?? 0;
                    newPlan.VHPRequirement = "通常";
                    newPlan.FillingRequirement = "通常";
                    newPlan.MillingRequirement = "通常";
                    newPlan.MachineRequirement = "通常";
                    newPlan.GrainSize = "-200";
                    newPlan.SingleWeight = plan.WeightS ?? 0;
                    newPlan.AllWeight = plan.WeightAll ?? 0;
                    newPlan.RoomTemperature = 25;
                    newPlan.RoomHumidity = 65;
                    newPlan.Vaccum = 0;
                    newPlan.GrainSize = "-200";
                    newPlan.PreTemperature = 0;
                    newPlan.PrePressure = 0;
                    double press = 0;
                    if (double.TryParse(plan.HighestPressure, out press))
                    {
                        newPlan.Pressure = press;
                    }
                    else
                    {
                        newPlan.Pressure = 0;
                    }
                    double temp = 0;
                    if (double.TryParse(plan.HighestTemp, out temp))
                    {
                        newPlan.Temperature = temp;
                    }
                    else
                    {
                        newPlan.Temperature = 0;
                    }

                    newPlan.KeepTempTime = 0;
                    newPlan.SpecialRequirement = plan.PlanMemo;
                    newPlan.State = "已核验";

                    newDb.PMSPlanVHPs.Add(newPlan);
                }
                #endregion
            }
            //保存一次
            newDb.SaveChanges();
            Console.WriteLine("Order And Plan Transfer Completed");
        }

        public void TransferDensity()
        {
            var oldDensity = oldDb.tb_Density.ToList();
            foreach (var item in oldDensity)
            {
                //var compound = new BDCompound();
                //compound.ID = Guid.NewGuid();
                //compound.MaterialName = item.Material;
                //compound.Density = item.Density??0;
                //compound.InformationSource = item.Memo;
                //compound.Creator = "xs.zhou";
                //compound.CreateTime = DateTime.Now;


                //newDb.Compounds.Add(compound);
            }
            newDb.SaveChanges();

        }

        public void Product()
        {
            var targets = productDb.Targets.ToList();
            targets.ForEach(t =>
            {
                var product = new RecordTests()
                {
                    ID = t.Id,
                    TestType="靶材",
                    ProductID = t.Lot,
                    Composition = t.Material,
                    CompositionAbbr = t.MaterialAbbr,
                    CompositionXRF = t.XRFComposition,
                    Weight = t.Weight,
                    Resistance=t.Resistance,
                    Density = t.Density,
                    Remark = t.Remark,
                    CreateTime = t.CreateDate,
                    Customer = t.Customer,
                    PO = t.PO,
                    Dimension = t.Dimension,
                    DimensionActual = t.Size,
                    Defects="无",
                    Sample="无",
                    State = "已核验",
                    Creator = "xs.zhou"
                };



                newDb.RecordTests.Add(product);
            });

            newDb.SaveChanges();



        }

    }
}
