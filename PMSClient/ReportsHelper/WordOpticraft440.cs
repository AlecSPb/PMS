using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using Novacode;
using System.IO;
using System.Drawing;
namespace PMSClient.ReportsHelper
{
    /// <summary>
    /// 订单报告
    /// </summary>
    public class WordOpticraft440 : ReportBase
    {
        private string prefix = "Opticraft440抛光";
        public WordOpticraft440()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "ReportOpticraft440.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "ReportOpticraft440_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }


        public void SetModel(DcRecordTest test)
        {
            if (test != null)
            {
                model = test;
                var targetName = $"PMI_{prefix}_{StringUtil.RemoveSlash(model.Customer)}_{model.CompositionAbbr}_{model.ProductID}.docx".Replace('-', '_');
                targetFile = Path.Combine(targetDir, targetName);
            }
        }
        private DcRecordTest model;
        public override void Output()
        {
            try
            {
                if (model == null)
                {
                    return;
                }
                ReportHelper.FileCopy(sourceFile, tempFile);
                //写入数据到文件
                #region 创建文档
                using (DocX document = DocX.Load(tempFile))
                {
                    string lotNumber = (model.CompositionAbbr ?? "") + "-" + (model.ProductID ?? "");
                    document.ReplaceText("[Lot]", lotNumber ?? "");
                    document.ReplaceText("[PO]", model.PO ?? "");
                    document.ReplaceText("[CurrentDate]", DateTime.Now.ToString("MM/dd/yyyy"));
                    document.ReplaceText("[CurrentLot]", DateTime.Now.ToString("yyMMdd"));
                    document.ReplaceText("[Size]", model.Dimension ?? "");
                    document.ReplaceText("[Dimension]", model.Dimension ?? "");
                    document.Save();
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
                //PMSDialogService.ShowYes("原材料报告创建成功，请在桌面查看");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }




    }
}

