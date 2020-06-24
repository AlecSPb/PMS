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
    /// 每个接口都是用GetOrder AddOrder UpdateOrder
    /// Get
    /// Add
    /// Update
    /// Lock
    /// Check
    /// 
    /// 实现的服务
    /// Order
    /// VHPPlan
    /// Misson
    /// </summary>
    [ServiceContract]
    public interface INewService
    {
        #region 订单接口
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
        [OperationContract]
        bool CheckOrderPMINumberExist(string pminumber);

        [OperationContract]
        int GetOrderUnFinishedCount();

        [OperationContract]
        int GetOrderUnFinishedTargetCount();


        [OperationContract]
        DcOrder GetOrderByPMINumber(string pminumber);
        #endregion

        #region 任务计划接口
        [OperationContract]
        List<DcOrder> GetMisson(int s, int t, string composition, string pminumber, string state);
        [OperationContract]
        int GetMissonCount(string composition, string pminumber, string state);

        [OperationContract]
        int GetMissonUnCompletedCount();
        [OperationContract]
        int GetMissonUnVHPTargetCount();
        [OperationContract]
        int GetEmergencyOrderCount();


        [OperationContract]
        int GetProductPlanCountByOrderID(Guid id);

        [OperationContract]
        List<DcPlanVHP> GetPlansByOrderID(Guid id);


        [OperationContract]
        List<DcPlanExtra> GetPlanExtra(int s, int t, string searchCode, string composition, string pminumber);
        [OperationContract]
        int GetPlanExtraCount(string searchCode, string composition, string pminumber);

        //用于统筹追踪
        [OperationContract]
        List<DcPlanExtra> GetPlanExtraForProduct(int skip, int take, string searchCode, string composition, string pminumber);
        [OperationContract]
        int GetPlanExtraForProductCount(string searchCode, string composition, string pminumber);

        [OperationContract]
        void AddPlan(DcPlanVHP model, string user);
        [OperationContract]
        void UpdatePlan(DcPlanVHP model, string user);

        [OperationContract]
        void LockTodayPlans();
        #endregion

    }
}