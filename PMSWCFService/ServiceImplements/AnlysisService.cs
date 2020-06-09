using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using PMSWCFService.ServiceImplements.Helpers;

namespace PMSWCFService
{
    /// <summary>
    /// 数据分析服务
    /// </summary>
    public class AnlysisService : IAnlysisService
    {
        public List<DcPlanTrace> GetPlanTrace(int s, int t, string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);

                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                && (p.PlanType == "加工" || p.PlanType == "其他" || p.PlanType == "外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                && p.SearchCode.Contains(searchCode)
                                && o.CompositionStandard.Contains(searchItem.Item1)
                                && o.CompositionStandard.Contains(searchItem.Item2)
                                && o.CompositionStandard.Contains(searchItem.Item3)
                                && o.CompositionStandard.Contains(searchItem.Item4)
                                && o.PMINumber.Contains(pminumber)
                                orderby p.PlanDate descending, p.PlanLot descending, p.VHPDeviceCode descending, p.CreateTime descending
                                select new DcPlanTrace
                                {
                                    ID = p.ID,
                                    SearchCode = p.SearchCode,
                                    PlanDate = p.PlanDate,
                                    PlanType = p.PlanType,
                                    VHPDeviceCode = p.VHPDeviceCode,
                                    CompositionStd = o.CompositionStandard,
                                    MoldDiameter = p.MoldDiameter,
                                    Quantity = p.Quantity,
                                    Customer = o.CustomerName,
                                    PMINumber = o.PMINumber,
                                    Dimension = o.Dimension,
                                    OrderQuantity = o.Quantity
                                };

                    var result = query.Skip(s).Take(t).ToList();

                    //查询每个进度情况
                    foreach (var item in result)
                    {
                        item.RecordDeMold = AnlysisHelper.CheckRecordDemold(item.SearchCode);
                        item.RecordMachine = AnlysisHelper.CheckRecordMachine(item.SearchCode);
                        item.RecordTest = AnlysisHelper.CheckRecordTest(item.SearchCode);
                        item.RecordBonding = AnlysisHelper.CheckRecordBonding(item.SearchCode);
                        item.RecordDelivery = AnlysisHelper.CheckDelivery(item.SearchCode);
                        item.RecordFailure = AnlysisHelper.CheckFailure(item.SearchCode);
                    }


                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanTraceCount(string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                && (p.PlanType == "加工" || p.PlanType == "其他" || p.PlanType == "外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                && p.SearchCode.Contains(searchCode)
                                && o.CompositionStandard.Contains(searchItem.Item1)
                                && o.CompositionStandard.Contains(searchItem.Item2)
                                && o.CompositionStandard.Contains(searchItem.Item3)
                                && o.CompositionStandard.Contains(searchItem.Item4)
                                && o.PMINumber.Contains(pminumber)
                                select new DcPlanTrace
                                {
                                    ID = p.ID,
                                };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcAnlysis> GetStatistic(int year_start, int month_start, int year_end, int month_end)
        {
            List<DcAnlysis> dcStatistics = new List<DcAnlysis>();
            try
            {
                XS.RunLog();
                DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

                int vhp_count = 0, vhp_product_count = 0, delivery_target_count = 0, vhp_blank_count = 0;

                double total_material_order, total_material_in, total_powder_in = 0, total_powder_out = 0;
                int count_test = 0, count_bonding = 0, count_machine = 0;

                //总天数
                double days_total = (endTime - startTime).TotalDays;

                using (var db = new PMSDbContext())
                {
                    var query_order = from i in db.Orders
                                      where i.State != PMSCommon.OrderState.作废.ToString()
                                      && i.State != PMSCommon.OrderState.取消.ToString()
                                      && i.CreateTime >= startTime
                                      && i.CreateTime <= endTime
                                      select i;
                    var query_order_group_customer = from i in db.Orders
                                                     where i.State != PMSCommon.OrderState.作废.ToString()
                                                     && i.State != PMSCommon.OrderState.取消.ToString()
                                                     && i.CreateTime >= startTime
                                                     && i.CreateTime <= endTime
                                                     group i by i.CustomerName into g
                                                     select new { Key = g.Key, Value = g };

                    var query_vhp = from i in db.VHPPlans
                                    where i.State != PMSCommon.CommonState.作废.ToString()
                                    && i.CreateTime >= startTime
                                    && i.CreateTime <= endTime
                                    select i;
                    //获取热压计划数
                    vhp_count = query_vhp.Count();


                    var query_vhp_product = from i in db.VHPPlans
                                            where i.State != PMSCommon.CommonState.作废.ToString()
                                            && i.PlanType != PMSCommon.VHPPlanType.回收.ToString()
                                            && i.PlanType != PMSCommon.VHPPlanType.烤料.ToString()
                                            && i.PlanType != PMSCommon.VHPPlanType.试机.ToString()
                                            && i.CreateTime >= startTime
                                            && i.CreateTime <= endTime
                                            select i;

                    //获取产品热压计划数
                    vhp_product_count = query_vhp_product.Count();
                    //获取计划产品靶材数
                    vhp_blank_count = query_vhp_product.Sum(i => i.Quantity);

                    var query_delivery = from i in db.DeliveryItems
                                         where i.State != PMSCommon.CommonState.作废.ToString()
                                         && i.ProductType == PMSCommon.ProductType.靶材.ToString()
                                         && i.CreateTime >= startTime
                                         && i.CreateTime <= endTime
                                         select i;
                    //获取所有发货产品靶材数
                    delivery_target_count = query_delivery.Count();

                    var query_materialorder = from i in db.MaterialOrderItems
                                              where i.State != PMSCommon.CommonState.作废.ToString()
                                              && i.CreateTime >= startTime
                                              && i.CreateTime <= endTime
                                              select i;
                    //获取原料订单总数
                    total_material_order = query_materialorder.Sum(i => i.Weight);

                    var query_materialin = from i in db.MaterialInventoryIns
                                           where i.State != PMSCommon.CommonState.作废.ToString()
                                           && i.CreateTime >= startTime
                                           && i.CreateTime <= endTime
                                           select i;
                    //获取原料入库总数
                    total_material_in = query_materialin.Sum(i => i.Weight);


                    var query_milling = from i in db.RecordMillings
                                        where i.State != PMSCommon.CommonState.作废.ToString()
                                        && i.CreateTime >= startTime
                                        && i.CreateTime <= endTime
                                        select i;
                    //获取制粉总数
                    total_powder_in = query_milling.Sum(i => i.WeightIn);
                    total_powder_out = query_milling.Sum(i => i.WeightOut);

                    var query_machine = from i in db.RecordMachines
                                        where i.State != PMSCommon.CommonState.作废.ToString()
                                        && i.CreateTime >= startTime
                                        && i.CreateTime <= endTime
                                        select i;
                    //获取加工总数
                    count_machine = query_machine.Count();

                    var query_test = from i in db.RecordTests
                                     where i.State != PMSCommon.CommonState.作废.ToString()
                                     && i.FollowUps != PMSCommon.TestFollowUps.报废.ToString()
                                     && i.FollowUps != PMSCommon.TestFollowUps.试验.ToString()
                                     && i.FollowUps != PMSCommon.TestFollowUps.返工.ToString()
                                     && i.CreateTime >= startTime
                                     && i.CreateTime <= endTime
                                     select i;
                    //获取检测总数
                    count_test = query_test.Count();


                    var query_bonding = from i in db.RecordBondings
                                        where i.State == PMSCommon.BondingState.最终完成.ToString()
                                        && i.CreateTime >= startTime
                                        && i.CreateTime <= endTime
                                        select i;

                    //获取绑定数目
                    count_bonding = query_bonding.Count();


                    //获取机器使用分组
                    var query_vhp_machine_used = from i in db.VHPPlans
                                                 where i.State != PMSCommon.CommonState.作废.ToString()
                                                 && i.CreateTime >= startTime
                                                 && i.CreateTime <= endTime
                                                 group i by i.VHPDeviceCode into g
                                                 select new { g.Key, UsedCount = g.Count() };
                    //获取报废信息
                    var query_fail = from i in db.Failures
                                     where i.State != PMSCommon.SimpleState.作废.ToString()
                                     && i.CreateTime >= startTime
                                     && i.CreateTime <= endTime
                                     select i;



                    #region 添加统计数字到结果集
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "订单",
                        Key = "订单总数",
                        Value = query_order.Count().ToString(),
                        Remark = "个"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "订单",
                        Key = "订单靶材总数",
                        Value = query_order.Sum(i => i.Quantity).ToString(),
                        Remark = "片"
                    });
                    foreach (var item in query_order_group_customer)
                    {
                        dcStatistics.Add(new DcAnlysis
                        {
                            Group = "订单",
                            Key = $"{item.Key}订单的靶材数目",
                            Value = item.Value.Sum(i => i.Quantity).ToString(),
                            Remark = ""
                        });
                    }

                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "原料",
                        Key = "订购原料",
                        Value = total_material_order.ToString(),
                        Remark = "kg"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "原料",
                        Key = "订购原料-加工费",
                        Value = query_materialorder.Sum(i => i.Weight * i.UnitPrice).ToString("F2"),
                        Remark = "RMB"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "原料",
                        Key = "订购原料-材料费",
                        Value = query_materialorder.Sum(i => i.MaterialPrice).ToString("F2"),
                        Remark = "RMB"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "原料",
                        Key = "订购原料-总费",
                        Value = query_materialorder.Sum(i => i.MaterialPrice + i.Weight * i.UnitPrice).ToString("F2"),
                        Remark = "RMB"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "原料",
                        Key = "入库原料",
                        Value = total_material_in.ToString(),
                        Remark = "kg"
                    });


                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "制粉",
                        Key = "制粉投入",
                        Value = (total_powder_in / 1000).ToString("F2"),
                        Remark = "kg"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "制粉",
                        Key = "日均制粉投入",
                        Value = (total_powder_in / days_total / 1000).ToString("F2"),
                        Remark = "kg"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "制粉",
                        Key = "制粉产出",
                        Value = (total_powder_out / 1000).ToString("F2"),
                        Remark = "kg"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "制粉",
                        Key = "日均制粉产出",
                        Value = (total_powder_out / days_total / 1000).ToString("F2"),
                        Remark = "kg"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "加工",
                        Key = "加工片数",
                        Value = count_machine.ToString()
                    });

                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "加工",
                        Key = "日均加工片数",
                        Value = ((double)count_machine / days_total).ToString("F2")
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "测试",
                        Key = "测试片数",
                        Value = count_test.ToString()
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "测试",
                        Key = "日均测试片数",
                        Value = ((double)count_test / days_total).ToString("F2")
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "绑定",
                        Key = "绑定片数",
                        Value = count_bonding.ToString()
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "绑定",
                        Key = "日均绑定片数",
                        Value = ((double)count_bonding / days_total).ToString("F2")
                    });

                    //热压机次统计
                    foreach (var item in query_vhp_machine_used)
                    {
                        dcStatistics.Add(new DcAnlysis
                        {
                            Group = "热压",
                            Key = $"{item.Key}-机使用次数",
                            Value = item.UsedCount.ToString()
                        });
                    }


                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "热压",
                        Key = "热压机次",
                        Value = vhp_count.ToString()
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "热压",
                        Key = "日均热压机次",
                        Value = ((double)vhp_count / days_total).ToString("F2")
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "热压",
                        Key = "产品热压机次",
                        Value = vhp_product_count.ToString()
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "热压",
                        Key = "日均产品热压机次",
                        Value = ((double)vhp_product_count / days_total).ToString("F2")
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "热压",
                        Key = "产品热压机次/总热压机次",
                        Value = ((double)vhp_product_count / (double)vhp_count).ToString("F2")
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "热压",
                        Key = "总热压计划产品毛坯数目",
                        Value = vhp_blank_count.ToString(),
                        Remark = "存在一次计划1毛坯1产品或4毛坯4产品或1毛坯3产品情况"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "热压",
                        Key = "日均热压计划产品毛坯数目",
                        Value = ((double)vhp_blank_count / days_total).ToString("F2")
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "发货",
                        Key = "总发货靶材片数",
                        Value = delivery_target_count.ToString(),
                        Remark = ""
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "发货",
                        Key = "日均发货靶材片数",
                        Value = ((double)delivery_target_count / days_total).ToString("F2"),
                        Remark = "包含外包靶材"
                    });
                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "发货",
                        Key = "总发货靶材片数/总热压计划产品毛坯数目",
                        Value = ((double)vhp_blank_count / (double)delivery_target_count).ToString("F2"),
                        Remark = ""
                    });

                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "发货",
                        Key = "总发货靶材片数/总产品热压机次",
                        Value = ((double)vhp_blank_count / (double)vhp_product_count).ToString("F2")
                    });


                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "报废",
                        Key = "报废总数",
                        Value = query_fail.Count().ToString(),
                        Remark = "仅统计报废记录填写的"
                    });

                    dcStatistics.Add(new DcAnlysis
                    {
                        Group = "报废",
                        Key = "报废率",
                        Value = ((double)query_fail.Count() / query_vhp_product.Sum(i => i.Quantity)).ToString("F2"),
                        Remark = "报废总数/计划产品毛坯数，仅统计报废记录填写的"
                    });
                    #endregion

                }

            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
            return dcStatistics;
        }

        public List<DcAnlysisCustomer> GetStatisticCustomer(int year_start, int month_start, int year_end, int month_end)
        {

            List<DcAnlysisCustomer> customers = new List<DcAnlysisCustomer>();
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    var query = from c in db.Customers
                                join o in db.Orders on c.CustomerName equals o.CustomerName into g
                                from gr in g.DefaultIfEmpty()
                                where gr.State != PMSCommon.OrderState.作废.ToString()
                                && gr.ProductType == PMSCommon.ProductType.靶材.ToString()
                                && gr.CreateTime >= startTime
                                && gr.CreateTime <= endTime
                                group gr by gr.CustomerName into gp
                                orderby gp.Key, gp.Count(), gp.Sum(i => i.Quantity)
                                select new DcAnlysisCustomer
                                {
                                    CustomerName = gp.Key,
                                    OrderCount = gp.Count(),
                                    TargetQuantity = gp.Sum(i => i.Quantity)
                                };

                    foreach (var item in query)
                    {
                        customers.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
            return customers;
        }
    }
}