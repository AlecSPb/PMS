using Novacode;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace PMSClient.ReportsHelper
{
    public class WordBondingSheet : ReportBase
    {
        private string prefix = "绑定记录单";
        public WordBondingSheet()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "BondingSheet.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "BondingSheet_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }

        public void SetModel(List<DcRecordBonding> model)
        {
            if (model != null)
            {
                this.model = model;
                CreateFolderOnDesktop();
                var targetName = $"{prefix}_{DateTime.Today.ToString("yyMMdd")}.docx";
                targetFile = Path.Combine(targetDir, targetName);
            }
        }

        private List<DcRecordBonding> model;
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
                    #region 基本字段
                    document.ReplaceText("[CreateDate]", DateTime.Now.ToString("yyyy-MM-dd"));

                    if (document.Tables[0] != null)
                    {
                        Table mainTable = document.Tables[0];
                        int rownumber = 1;
                        var ordered = model.OrderBy(i => i.TargetProductID).OrderBy(i => i.PlanBatchNumber);
                        foreach (var item in ordered)
                        {
                            mainTable.InsertRow();
                            string no = item.PlanBatchNumber.ToString() + "-" + rownumber;
                            mainTable.Rows[rownumber].Cells[0].Paragraphs[0]
                                .Append(no).FontSize(10).Alignment = Alignment.left;

                            mainTable.Rows[rownumber].Cells[1].Paragraphs[0]
                                .Append($"{item.TargetProductID}").FontSize(10);

                            mainTable.Rows[rownumber].Cells[2].Paragraphs[0]
                                .Append(item.TargetComposition).FontSize(10);

                            mainTable.Rows[rownumber].Cells[3].Paragraphs[0]
                                .Append("□").FontSize(10).Alignment=Alignment.center;

                            mainTable.Rows[rownumber].Cells[13].Paragraphs[0]
                                .Append("□缺 □裂").FontSize(10);

                            mainTable.Rows[rownumber].Cells[13].VerticalAlignment = VerticalAlignment.Center;
                            mainTable.Rows[rownumber].Cells[13].Paragraphs[0].Alignment = Alignment.center;
                            rownumber++;
                        }
                    }
                    document.Save();
                    #endregion
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
                PMSDialogService.Show("绑定记录单创建成功，即将打开");

                System.Diagnostics.Process.Start(targetFile);

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
