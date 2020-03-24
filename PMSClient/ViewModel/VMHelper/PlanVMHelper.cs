using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ViewModel.VMHelper
{
    public class PlanVMHelper
    {
        /// <summary>
        /// 创建标签内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string CreateLabel(DcPlanWithMisson model)
        {
            if (model == null)
                return "";

            //提示该标签日期超前了
            if (model.Plan.PlanDate.Date >= DateTime.Now.Date)
            {
                PMSDialogService.ShowWarning("选择的计划还没有热压，请确认你选择的【计划日期】是否正确？");
            }

            var lb = new StringBuilder();
            //第1行
            lb.AppendLine($"{model.Plan.VHPDeviceCode} ***************************");
            lb.Append(UsefulPackage.PMSTranslate.PlanLot(model));
            lb.Append(model.Plan.PlanType);

            //判定是否显示样品标志,只在非回收记录中显示 样品标记
            string sample_indicator = "";
            if (!model.Plan.PlanType.Contains("回收"))
            {
                bool needSample = Helpers.OrderHelper.NeedSample(model.Misson.SampleNeed) ||
                Helpers.OrderHelper.NeedSample(model.Misson.SampleForAnlysis);
                sample_indicator = needSample ? "-[样]" : "";
            }

            lb.AppendLine($"{sample_indicator}");

            //第2行
            lb.AppendLine(model.Misson.CompositionStandard);
            //第3行
            if (model.Plan.PlanType.Contains("回收"))
            {
                lb.Append("模:");
                lb.AppendLine(model.Plan.MoldDiameter.ToString() + "mm OD x " + model.Plan.Thickness + "mm");
            }
            lb.AppendLine(model.Misson.PMINumber);
            if (!model.Plan.PlanType.Contains("回收"))
            {
                lb.Append("靶:");
                lb.AppendLine(model.Misson.Dimension);
                lb.AppendLine(model.Misson.DimensionDetails);
            }

            lb.AppendLine("===== 样品标签↓  =====");
            lb.AppendLine(model.Misson.CompositionStandard);
            lb.AppendLine("Weight      g");
            lb.AppendLine(UsefulPackage.PMSTranslate.PlanLot(model));

            lb.AppendLine("=====  简成分样品标签↓  =====");
            lb.AppendLine(Helpers.CompositionHelper.RemoveNumbers(model.Misson.CompositionStandard));
            lb.AppendLine("Weight      g");
            lb.AppendLine(UsefulPackage.PMSTranslate.PlanLot(model));
            lb.AppendLine();


            return lb.ToString();
        }
    }
}
