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
                    document.ReplaceText("[CreateDate]", DateTime.Now.ToString("yyyy-MM-dd dddd"));

                    if (document.Tables[0] != null)
                    {
                        Table mainTable = document.Tables[0];
                        int rownumber = 1;
                        var ordered = model.OrderBy(i => i.TargetProductID).OrderBy(i => i.PlanBatchNumber);

                        foreach (var item in ordered)
                        {
                            var current_row = mainTable.InsertRow();
                            current_row.Height = 35;

                            string no = item.PlanBatchNumber.ToString() + "-" + rownumber;

                            var cell_0 = mainTable.Rows[rownumber].Cells[0];
                            cell_0.Paragraphs[0]
                                .Append(no).FontSize(10).Bold().Alignment = Alignment.left;
                            cell_0.VerticalAlignment = VerticalAlignment.Center;

                            var cell_1 = mainTable.Rows[rownumber].Cells[1];
                            cell_1.Paragraphs[0]
                                .Append($"{item.TargetProductID}").FontSize(10).Bold();
                            cell_1.VerticalAlignment = VerticalAlignment.Center;

                            var cell_2 = mainTable.Rows[rownumber].Cells[2];
                            cell_2.Paragraphs[0]
                                .Append(item.TargetComposition).FontSize(10).Bold();
                            cell_2.VerticalAlignment = VerticalAlignment.Center;

                            var cell_3 = mainTable.Rows[rownumber].Cells[3];
                            cell_3.Paragraphs[0]
                                 .Append("□").FontSize(10).Bold().Alignment = Alignment.center;
                            cell_3.VerticalAlignment = VerticalAlignment.Center;

                            var cell_12 = mainTable.Rows[rownumber].Cells[12];
                            cell_12.Paragraphs[0]
                                .Append("大 小").FontSize(10).Bold();
                            cell_12.Paragraphs[0].Alignment = Alignment.center;
                            cell_12.VerticalAlignment = VerticalAlignment.Center;

                            var cell_13 = mainTable.Rows[rownumber].Cells[13];
                            cell_13.Paragraphs[0]
                                .Append("内 外").FontSize(10).Bold();
                            cell_13.Paragraphs[0].Alignment = Alignment.center;
                            cell_13.VerticalAlignment = VerticalAlignment.Center;
                            //添加间隔背景颜色
                            if (rownumber % 2 == 1)
                            {
                                for (int i = 0; i < 14; i++)
                                {
                                    current_row.Cells[i].FillColor = Color.LightGray;
                                }
                            }


                            rownumber++;
                        }
                    }
                    var bottom_p = document.InsertParagraph();
                    bottom_p.Append("取下压块时的温度:A=[    ]℃,B=[    ]℃");
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
