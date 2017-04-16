using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using Novacode;
using System.IO;

namespace PMSClient.ReportsHelper
{
    /// <summary>
    /// 订单报告
    /// </summary>
    public class ReportGASOpticraftGrinding : ReportBase
    {
        private string prefix = "Opticraft440抛光";
        public ReportGASOpticraftGrinding()
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "GeAsSeOpticraftGrinding.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "GeAsSeOpticraftGrinding_Temp.docx");
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
            #region 创建文档

            #endregion
            //复制到临时文件
            ReportHelper.FileCopy(tempFile, targetFile);
        }




    }
}

