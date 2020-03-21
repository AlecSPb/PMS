using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    /// <summary>
    /// 这个是尝试新功能的新服务
    /// 旧的服务接口不再变动，保持兼容
    /// 新服务另起炉灶
    /// 慎重变动
    /// </summary>
    [ServiceContract]
    public interface INewService
    {
        #region 订单
        [OperationContract]
        List<DcOrder> GetOrder(int s, int t, string customer, string composition, string pminumber, string state);
        [OperationContract]
        int GetOrderCount(string customer, string composition, string pminumber, string state);

        [OperationContract]
        DateTime GetOrderLastUpdateTime(Guid id);

        [OperationContract]
        DcOrder GetOrderByID(Guid id);

        [OperationContract]
        void AddOrder(DcOrder model, string user);
        [OperationContract]
        void UpdateOrder(DcOrder model, string user);

        #endregion

        #region 任务
        [OperationContract]
        List<DcOrder> GetMisson(int s, int t, string composition, string pminumber, string state);
        [OperationContract]
        int GetMissonCount(string composition, string pminumber, string state);

        [OperationContract]
        List<DcPlanExtra> GetPlan(int s, int t, string composition, string pminumber);
        [OperationContract]
        int GetPlanCount(string composition, string pminumber);

        [OperationContract]
        void AddPlan(DcPlanVHP model, string user);
        [OperationContract]
        void UpdatePlan(DcPlanVHP model, string user);
        #endregion

        #region 原料

        #endregion

        #region 记录

        [OperationContract]
        List<DcRecordTest> GetRecordTest(int s, int t, string composition, string customer, string pminumber);
        [OperationContract]
        int GetRecordTestCount(string composition, string customer, string pminumber);

        [OperationContract]
        DateTime GetRecordTestLastUpdateTime(Guid id);
        #endregion


        #region 发货

        #endregion
    }
}