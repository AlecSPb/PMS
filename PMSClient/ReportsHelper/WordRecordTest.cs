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
    public class WordRecordTest : ReportBase
    {
        private string prefix = "测试报告";
        public WordRecordTest()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "RecordTest.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "RecordTest_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }

        public void SetModel(DcRecordTest test)
        {
            if (test != null)
            {
                model = test;
                CreateFolderOnDesktop();
                var targetName = $"{prefix}_{model.CompositionAbbr}_{model.ProductID}.docx";
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
                //复制到临时文件
                ReportHelper.FileCopy(sourceFile, tempFile);
                #region 创建文档
                //写入数据到文件
                using (var doc = DocX.Load(tempFile))
                {
                    doc.ReplaceText("[Composition]", model.Composition ?? "");
                    doc.ReplaceText("[Customer]", model.Customer ?? "");
                    doc.ReplaceText("[PO]", model.PO ?? "");
                    doc.ReplaceText("[CreateTime]", model.CreateTime.ToShortDateString());

                    doc.ReplaceText("[ProductID]", model.ProductID ?? "");
                    doc.ReplaceText("[Weight]", model.Weight ?? "");
                    doc.ReplaceText("[Density]", model.Density ?? "");
                    doc.ReplaceText("[Resistance]", model.Resistance ?? "");
                    doc.ReplaceText("[Remark]", model.Remark ?? "");
                    doc.ReplaceText("[Dimension]", model.Dimension ?? "");
                    doc.ReplaceText("[DimensionActual]", model.DimensionActual ?? "");
                    //写入XRF数据表
                    Table mainTable = doc.Tables[0];
                    if (mainTable != null)
                    {
                        Paragraph p = mainTable.Rows[9].Cells[0].Paragraphs[0];
                        //判断xrf数据内容是否以No开头
                        if (model.CompositionXRF.StartsWith("No"))
                        {
                            string[] lines = model.CompositionXRF.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            int rowCount = lines.Count();
                            int columnCount = lines[0].Split(',').Count();
                            Table xrfTable = doc.AddTable(rowCount, columnCount);
                            xrfTable.Design = TableDesign.TableGrid;
                            xrfTable.Alignment = Alignment.center;
                            xrfTable.AutoFit = AutoFit.Contents;
                            for (int i = 0; i < lines.Count(); i++)
                            {
                                string[] items = lines[i].Split(',');
                                for (int j = 0; j < items.Count(); j++)
                                {
                                    Cell cell = xrfTable.Rows[i].Cells[j];
                                    cell.Width = 80;
                                    cell.Paragraphs[0].Append(items[j]).FontSize(11).Font(new System.Drawing.FontFamily("Calibri"));
                                }
                            }
                            p.InsertTableAfterSelf(xrfTable);

                        }
                        else
                        {
                            p.Append(model.CompositionXRF);
                        }

                    }
                    doc.Save();
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
