using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDAL;

namespace DBTransferFromOldToNew
{
    public class DBTransfer:IDisposable
    {
        private PMSDbContext newDb;
        private dbnewEntities oldDb;
        public DBTransfer()
        {
            newDb = new PMSDbContext();
            oldDb = new dbnewEntities();
        }

        public void Dispose()
        {
            newDb.Dispose();
            oldDb.Dispose();
        }

        public void TransferOrder()
        {
            var oldOrder = oldDb.tb_Order.ToList();
            foreach (var order in oldOrder)
            {
                var orderId = Guid.NewGuid();

                var newOrder = new PMSOrder();
                newOrder.ID = orderId;
                newOrder.CustomerName = order.Customer;
                newOrder.PO = order.PO;
                newOrder.PMIWorkingNumber = order.PMIWorkNumber;
                newOrder.CompositionStandard = order.ProductName;
                newOrder.CompositionOriginal = order.ProductName;
                newOrder.CompositoinAbbr = "";
                newOrder.ProductType = order.ProductType;
                newOrder.Purity = order.Purity;
                newOrder.Quantity = order.Number??0;
                newOrder.QuantityUnit = order.Unit;
                newOrder.Dimension = order.Dimension;
                newOrder.DimensionDetails = "Normal";
                newOrder.SampleNeed = "";
                newOrder.DeadLine = order.SendDateNeed??new DateTime(2016,12,12);
                newOrder.MinimumAcceptDefect = "Normal";
                newOrder.Remark = order.OrderMemo;
                newOrder.Priority = 1;
                newOrder.State = 1;
                newOrder.StateRemark = "";
                newOrder.CreateTime = order.OrderDate;
                newOrder.Reviewer = "yr.hu";
                newOrder.ReviewPassed = true;
                newOrder.ReviewDate = order.OrderDate;
                newOrder.Creator= "yr.hu";
                newOrder.PolicyType = "VHP";
                newOrder.PolicyContent = "";
                newOrder.PolicyMaker = "yr.hu";
                newOrder.PolicyMakeDate = order.OrderDate;

                newDb.Orders.Add(newOrder);
                //获取对应OrderID的Plans
                #region Plan
                var plans = oldDb.tb_Plan.Where(p => p.OrderID == order.OrderID).ToList();
                foreach (var plan in plans)
                {
                    var planId = Guid.NewGuid();
                    var newPlan = new PMSPlanVHP();
                    newPlan.ID = planId;
                    newPlan.CreateTime = plan.VHPTimePlan;
                    newPlan.Creator = "f.liang";
                    newPlan.OrderID = orderId;
                    newPlan.PlanDate = plan.VHPTimePlan;
                    newPlan.VHPDeviceCode = plan.DeviceType;
                    newPlan.CurrentMold = "GM";
                    newPlan.MoldDiameter = plan.MoldMD ?? 0;
                    newPlan.Thickness = plan.PressThick ?? 0;
                    newPlan.CalculationDensity = plan.DensityCal ?? 0;
                    newPlan.Quantity = plan.PressNum ?? 0;
                    newPlan.FillingRequirement = "Normal";
                    newPlan.MillingRequirement = "Normal";
                    newPlan.GrainSize = "-200";
                    newPlan.PowderWeight = plan.WeightAll ?? 0;
                    newPlan.RoomTemperature = 25;
                    newPlan.RoomHumidity = 65;
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
                    newPlan.LaterProcess = "";
                    newPlan.LaterProcessDetails = "";
                    newPlan.State = 1;

                    newDb.VHPPlans.Add(newPlan);
                }
                #endregion
            }
            //保存一次
            newDb.SaveChanges();
            Console.WriteLine("Order And Plan Transfer Completed");
        }



    }
}
