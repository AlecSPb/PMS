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
                                    OrderQuantity=o.Quantity
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
                                orderby p.PlanDate descending, p.PlanLot descending, p.VHPDeviceCode descending, p.CreateTime descending
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





    }
}