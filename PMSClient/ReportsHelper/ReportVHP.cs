using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PMSClient.MainService;
using Novacode;

namespace PMSClient.ReportsHelper
{
    public class ReportVHP : ReportBase
    {    
        private string prefix = "热压报告";
        public ReportVHP()
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "ReportRecordVHP.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "ReportRecordVHP_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }

        public void SetTargetFolder(string targetFolder)
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            targetFile = Path.Combine(targetFolder, targetName);
        }
        public void SetModel(DcPlanWithMisson model)
        {
            if (model != null)
            {
                this.model = model;
            }
        }
        private DcPlanWithMisson model;
        public override void Output()
        {
            if (model == null)
            {
                return;
            }

            ReportHelper.FileCopy(sourceFile, tempFile);
            #region 创建报告
            using (DocX document = DocX.Load(tempFile))
            {
                var plandate = model.Plan.PlanDate.ToString("yyyy-MM-dd");
                document.ReplaceText("[PlanDate]", plandate);
                document.ReplaceText("[Composition]", model.Misson.CompositionStandard);
                document.ReplaceText("[Temperature]", model.Plan.Temperature.ToString());
                document.ReplaceText("[Pressure]", model.Plan.Pressure.ToString());
                document.ReplaceText("[Vaccum]", model.Plan.Vaccum.ToString("#.##E00"));
                document.ReplaceText("[KeepTime]", model.Plan.KeepTempTime.ToString());












                document.Save();
            }
            #endregion
            //复制到临时文件
            ReportHelper.FileCopy(tempFile, targetFile);
        }

    }
}
