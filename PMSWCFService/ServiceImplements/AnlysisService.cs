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

        public List<DcStatistic> GetStatistic(int year_start, int month_start, int year_end, int month_end)
        {
            List<DcStatistic> dcStatistics = new List<DcStatistic>();
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

                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "订购原料kg",
                        Value = total_material_order
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "入库原料kg",
                        Value = total_material_in
                    });

                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "制粉投入kg",
                        Value = total_powder_in / 1000
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "日均制粉投入",
                        Value = total_powder_in / days_total / 1000
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "制粉产出kg",
                        Value = total_powder_out / 1000
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "日均制粉产出",
                        Value = total_powder_out / days_total / 1000
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "加工片数",
                        Value = count_machine
                    });

                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "日均加工片数",
                        Value = (double)count_machine / days_total
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "测试片数",
                        Value = count_test
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "日均测试片数",
                        Value = (double)count_test / days_total
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "绑定片数",
                        Value = count_bonding
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "日均绑定片数",
                        Value = (double)count_bonding / days_total
                    });


                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "热压机次",
                        Value = vhp_count
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "日均热压机次",
                        Value = (double)vhp_count / days_total
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "产品热压机次",
                        Value = vhp_product_count
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "日均产品热压机次",
                        Value = (double)vhp_product_count / days_total
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "产品热压机次/总热压机次",
                        Value = (double)vhp_product_count / (double)vhp_count
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "总热压计划产品毛坯数目",
                        Value = vhp_blank_count
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "日均热压计划产品毛坯数目",
                        Value = (double)vhp_blank_count / days_total
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "总发货靶材片数",
                        Value = delivery_target_count
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "日均发货靶材片数",
                        Value = (double)delivery_target_count / days_total
                    });
                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "总发货靶材片数/总热压计划产品毛坯数目",
                        Value = (double)vhp_blank_count / (double)delivery_target_count
                    });

                    dcStatistics.Add(new DcStatistic
                    {
                        Key = "总发货靶材片数/总产品热压机次",
                        Value = (double)vhp_blank_count / (double)vhp_product_count
                    });

                }

            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
            return dcStatistics;
        }
    }
}