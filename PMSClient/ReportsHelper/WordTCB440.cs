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
    public class WordTCB440:ReportBase
    {
        private string prefix = "TCB440Bonding";
        public WordTCB440()
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "ReportTCB440.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "ReportTCB440_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }


        public void SetTargetFolder(string targetFolder)
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            targetFile = Path.Combine(targetFolder, targetName);
        }
        public void SetModel(DcRecordTest test)
        {
            if (test != null)
            {
                model = test;
            }
        }
        private DcRecordTest model;
        public override void Output()
        {
            if (model == null)
            {
                return;
            }
            ReportHelper.FileCopy(sourceFile, tempFile);
            //写入数据到文件

            using (DocX document = DocX.Load(tempFile))
            {
                string lotNumber = (model.CompositionAbbr ?? "") + "-" + (model.ProductID ?? "");
                document.ReplaceText("[Lot]", lotNumber??"");
                document.ReplaceText("[PO]", model.PO ?? "");
                document.ReplaceText("[CurrentDate]", DateTime.Now.ToString("MM/dd/yyyy"));
                document.ReplaceText("[CurrentLot]", DateTime.Now.ToString("yyMMdd"));
                document.ReplaceText("[Size]", model.Dimension ?? "");
                document.ReplaceText("[CompositionAbbr]", model.CompositionAbbr ?? "");
                document.Save();
            }
            //复制到临时文件
            var targetName = $"PMI_{prefix}_{model.Customer}_{model.CompositionAbbr}_{model.ProductID}.docx".Replace('-', '_');
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
            ReportHelper.FileCopy(tempFile, targetFile);
        }




    }
}

