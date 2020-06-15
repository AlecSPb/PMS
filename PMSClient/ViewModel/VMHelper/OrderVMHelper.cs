using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.NewService;
using PMSClient.SampleService;

namespace PMSClient.ViewModel.VMHelper
{
    public class OrderVMHelper
    {
        public static void AddSampleFromOrder(DcOrder obj)
        {
            if (obj == null) return;

            if (!(Helpers.OrderHelper.NeedSample(obj.SampleNeed) || Helpers.OrderHelper.NeedSample(obj.SampleForAnlysis)))
            {
                if (!PMSDialogService.ShowYesNo("提醒", "貌似这个订单不需要样品？N=取消,Y=继续添加"))
                {
                    return;
                }
            }

            if (PMSDialogService.ShowYesNo("请问", $"确定要添加[{obj.PMINumber}-{obj.CompositionStandard}]的样品需求到样品管理吗？"))
            {
                int counter = 0;
                try
                {

                    using (var s = new SampleServiceClient())
                    {
                        counter = s.GetSampleByPMINumberCount(obj.PMINumber);
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
                if (counter > 0)
                {
                    if (!PMSDialogService.ShowYesNo("提醒", $"[{obj.PMINumber}]在样品管理中已经存在{counter}条记录，确定再添加一条吗？"))
                    {
                        return;
                    }
                }


                var model = new DcSample();
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                model.State = PMSCommon.SimpleState.正常.ToString();
                model.ProductID = "无";
                model.SampleID = "无";
                model.TrackingStage = PMSCommon.SampleTrackingStage.未取样.ToString();
                model.TraceInformation = "";
                model.ICPOES ="";
                model.GDMS = "";
                model.IGA = "";
                model.Thermal = VMHelper.SampleVMHelper.Thermal;
                model.Permittivity = VMHelper.SampleVMHelper.Permittivity;
                model.OtherTestResult = "";
                model.SampleType = PMSCommon.SampleType.块状.ToString();
                model.SampleFor = PMSCommon.SampleFor.Customer.ToString();
                model.Quantity = 1;
                model.Weight = "";
                model.MoreInformation = "无";
                model.Remark = "无";
                model.Composition = obj.CompositionStandard;
                model.PMINumber = obj.PMINumber;
                model.PO = obj.PO;
                model.Customer = obj.CustomerName;
                model.SampleType = PMSCommon.SampleType.块状.ToString();
                model.SampleFor = PMSCommon.SampleFor.Customer.ToString();

                bool need_need = Helpers.OrderHelper.NeedSample(obj.SampleNeed);
                bool need_anlysis = Helpers.OrderHelper.NeedSample(obj.SampleForAnlysis);
                string sample_str = "";

                bool need_need_remark = Helpers.OrderHelper.HasSampleRemark(obj.SampleNeedRemark);
                bool need_anlysis_remark = Helpers.OrderHelper.HasSampleRemark(obj.SampleForAnlysisRemark);
                string sample_remark_str = "";

                if (need_need)
                {
                    sample_str += $"客户样品:{obj.SampleNeed};";
                }

                if (need_anlysis)
                {
                    sample_str += $"自分析样品:{obj.SampleForAnlysis};";
                }
                model.OriginalRequirement = sample_str;

                if (need_need_remark)
                {
                    sample_remark_str += $"客户样品:{obj.SampleNeedRemark};";
                }

                if (need_anlysis_remark)
                {
                    sample_remark_str += $"自分析样品:{obj.SampleForAnlysisRemark};";
                }
                model.OriginalRequirementRemark = sample_remark_str;


                try
                {
                    using (var s = new SampleServiceClient())
                    {
                        s.AddSample(model);
                    }
                    PMSDialogService.Show("已成功添加样品到样品管理");
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }
    }
}
