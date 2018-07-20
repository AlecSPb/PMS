﻿using System;
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
                    document.ReplaceText("[Customer]", model.Customer ?? "");
                    string productid = (model.CompositionAbbr ?? "") + "-" + (model.ProductID ?? "");
                    document.ReplaceText("[ProductID]", productid ?? "");
                    document.ReplaceText("[PO]", model.PO ?? "");
                    document.ReplaceText("[COADate]", DateTime.Now.ToString("MM/dd/yyyy"));

                    document.ReplaceText("[Composition]", model.Composition ?? "");
                    document.ReplaceText("[Weight]", model.Weight ?? "");
                    document.ReplaceText("[Density]", model.Density ?? "");
                    document.ReplaceText("[Resistance]", model.Resistance ?? "");
                    document.ReplaceText("[Dimension]", COAHelper.StandardizeDimension(model.Dimension) ?? "");
                    document.ReplaceText("[DimensionActual]", COAHelper.StandardizeDimension(model.DimensionActual) ?? "");
                    document.ReplaceText("[OrderDate]", model.CreateTime.AddDays(-15).ToString("MM/dd/yyyy"));
                    document.ReplaceText("[CreateDate]", model.CreateTime.ToString("MM/dd/yyyy"));

                    //填充XRF表格
                    //填充XRF表格
                    if (document.Tables[0] != null)
                    {
                        Table mainTable = document.Tables[0];
                        Paragraph p = mainTable.Rows[9].Cells[0].Paragraphs[0];
                        InsertCompositionXRFTable(document, p, model.CompositionXRF, "No Composition Test Results");


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

                    StringBuilder sb1 = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();

                    for (int i = 0; i < lines.Count(); i++)
                    {
                        string[] items = lines[i].Split(',');

                        sb1.Clear();
                        sb2.Clear();
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
                            sb1.Append(items[j] + "\r");
                            sb2.Append("Atm%" + "\r");
                        }
                    }
                    p.InsertTableAfterSelf(xrfTable);

                    //添加平均成分到表格中
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
