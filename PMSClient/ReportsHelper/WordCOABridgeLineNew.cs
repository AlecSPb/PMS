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
    public class WordCOABridgeLineNew : ReportBase
    {
        private string prefix = "COA";
        public WordCOABridgeLineNew()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "COABridgeLineNew.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "COABridgeLineNew_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }
        public void SetTargetFileName(string targetFileName)
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            targetFile = Path.Combine(targetFileName, targetName);
        }
        public void SetModel(DcRecordTest test)
        {
            if (test != null)
            {
                model = test;
                CreateFolderOnDesktop();
                var targetName = $"PMI_{prefix}_{StringUtil.RemoveSlash(model.Customer)}_{StringUtil.RemoveSlash(model.CompositionAbbr)}_{model.ProductID}.docx".Replace('-', '_');
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
                    document.ReplaceText("[Customer]", model.Customer ?? "");
                    string productid = (model.CompositionAbbr ?? "") + "-" + (model.ProductID ?? "");
                    document.ReplaceText("[ProductID]", productid ?? "");
                    document.ReplaceText("[PO]", model.PO ?? "");
                    document.ReplaceText("[COADate]", DateTime.Now.ToString("MM/dd/yyyy"));

                    string purity = "99.995%默认";
                    using (var orderService = new OrderServiceClient())
                    {
                        var order = orderService.GetOrderByPMINumber(model.PMINumber);
                        if (order != null)
                        {
                            purity = order.Purity;
                        }
                    }
                    document.ReplaceText("[Purity]", purity);

                    document.ReplaceText("[Composition]", model.Composition ?? "");
                    document.ReplaceText("[Weight]", model.Weight ?? "");
                    document.ReplaceText("[Density]", model.Density ?? "");
                    document.ReplaceText("[Resistance]", model.Resistance ?? "");
                    document.ReplaceText("[Dimension]", COAHelper.StandardizeDimension(model.Dimension) ?? "");
                    document.ReplaceText("[DimensionActual]", COAHelper.StandardizeDimension(model.DimensionActual) ?? "");

                    document.ReplaceText("[OrderDate]", COAHelper.GetCreateDateFromPMINumber(model.PMINumber));
                    document.ReplaceText("[CreateDate]", DateTime.Now.ToString("MM/dd/yyyy"));

                    string roughness = model.Roughness == "无" ? "None" : model.Roughness;
                    document.ReplaceText("[Roughness]", roughness);



                    //写入CSCAN Flaw Data
                    string flawarea = model.CScan == null ? "None" : model.CScan;

                    document.ReplaceText("[FlawArea]", flawarea);





                    //如果是440靶材，加入粗糙度值
                    if (model.ProductID.Contains("#"))
                    {
                        document.ReplaceText("[Roughness]", model.Roughness);

                    }
                    else
                    {
                        document.ReplaceText("[Roughness]", "-");
                    }

                    //填充XRF表格
                    //填充XRF表格
                    if (document.Tables[0] != null)
                    {
                        Table mainTable = document.Tables[0];
                        Paragraph p = mainTable.Rows[9].Cells[0].Paragraphs[0];

                        string xrfWithStdDev = model.CompositionXRF;


                        //如果是bridgeline的数据，计算并追加标准差

                        int lineCount = COAHelper.SplitXRF(model.CompositionXRF).Count();
                        if (lineCount >= 2 && model.Customer.Contains("Bridgeline"))
                        {
                            var stddev = new PMSClient.ReportsHelperNew.ReportDataProcessHelper();
                            xrfWithStdDev = stddev.AppendStdDev(model.CompositionXRF);
                        }

                        InsertCompositionXRFTable(document, p, xrfWithStdDev, "No Composition Test Results");



                        //填充标称的成分
                        if (!string.IsNullOrEmpty(model.Composition))
                        {
                            Paragraph elementNames = mainTable.Rows[7].Cells[0].Paragraphs[0];
                            foreach (var name in GetCompositionNames(model.Composition))
                            {
                                elementNames.Append(name + "\r").FontSize(9)
                                    .Font(new System.Drawing.FontFamily("Times New Roman")).Bold();
                            }


                            Paragraph elementValues = mainTable.Rows[7].Cells[1].Paragraphs[0];
                            Paragraph units = mainTable.Rows[7].Cells[2].Paragraphs[0];
                            foreach (var at in GetCompositionValues(model.Composition))
                            {
                                elementValues.Append(at + "\r")
                                    .FontSize(9).Font(new System.Drawing.FontFamily("Times New Roman")).Bold();
                                units.Append("Atm%" + "\r")
                                    .FontSize(9).Font(new System.Drawing.FontFamily("Times New Roman")).Bold();
                            }

                        }


                    }

                    document.Save();
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
                //PMSDialogService.Show("原材料报告创建成功，即将打开");
                //System.Diagnostics.Process.Start(targetFile);

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }



        private static void InsertCompositionXRFTable(DocX document, Paragraph p, string xrfComposition, string noCompositionMessage)
        {
            if (!string.IsNullOrEmpty(xrfComposition))
            {
                string[] lines = xrfComposition.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                //根据csv数据确定xrf表格
                int rowCount = lines.Count();
                int columnCount = lines[0].Split(',').Count();

                if (rowCount < 2)
                {
                    p.InsertText(xrfComposition);
                }
                else
                {
                    Table xrfTable = document.AddTable(rowCount, columnCount);
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
                            cell.Paragraphs[0].Append(items[j]).FontSize(9)
                                .Font(new FontFamily("Times New Roman")).Bold();
                            if (j == 0)
                            {
                                continue;
                            }
                        }
                    }
                    p.InsertTableAfterSelf(xrfTable);


                    //添加平均成分到表格中

                    StringBuilder sb1 = new StringBuilder();//收集数字
                    StringBuilder sb2 = new StringBuilder();//收集单位
                    //获取倒数第二行
                    string average_line = lines[lines.Count() - 2];

                    string[] items2 = average_line.Split(',');
                    for (int j = 1; j < items2.Count(); j++)
                    {
                        sb1.Append(items2[j] + "\r");
                        sb2.Append("Atm%" + "\r");
                    }


                    Table mainTable = document.Tables[0];
                    Paragraph average = mainTable.Rows[7].Cells[3].Paragraphs[0];
                    Paragraph units = mainTable.Rows[7].Cells[4].Paragraphs[0];
                    average.Append(sb1.ToString()).FontSize(9)
                        .Font(new System.Drawing.FontFamily("Times New Roman")).Bold();
                    units.Append(sb2.ToString()).FontSize(9)
                        .Font(new System.Drawing.FontFamily("Times New Roman")).Bold();
                }

            }
            else
            {
                p.InsertText(noCompositionMessage);
            }
        }

        private static string[] GetMatchesString(string material, string pattern)
        {
            var matches = System.Text.RegularExpressions.Regex.Matches(material, pattern);
            string[] compositionNames = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                compositionNames[i] = matches[i].Value;
            }
            return compositionNames;
        }

        private static string[] GetCompositionValues(string material)
        {
            return GetMatchesString(material, @"[\d\.]+");
        }

        private static string[] GetCompositionNames(string material)
        {
            return GetMatchesString(material, @"[A-Za-z]+");
        }



    }
}

