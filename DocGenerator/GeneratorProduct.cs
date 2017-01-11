using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;//DocX
using DocGenerator.DocModels;

namespace DocGenerator
{
    public class GeneratorProduct : GeneratorBase,IDoc<Product>
    {
        public void Generate(string sourceFilePath, string targetFilePath, Product reportModel)
        {
            //复制文件
            CopyTemplate(sourceFilePath, targetFilePath);
            //写入数据到文件
            using (var doc=DocX.Load(targetFilePath))
            {
                doc.ReplaceText("[Composition]", reportModel.Composition ?? "");
                doc.ReplaceText("[Customer]", reportModel.Customer ?? "");
                doc.ReplaceText("[PO]", reportModel.PO ?? "");
                doc.ReplaceText("[CreateTime]", reportModel.CreateTime.ToShortDateString());

                doc.ReplaceText("[ProductID]", reportModel.ProductID ?? "");
                doc.ReplaceText("[Weight]", reportModel.Weight ?? "");
                doc.ReplaceText("[Density]", reportModel.Density ?? "");
                doc.ReplaceText("[Resistance]", reportModel.Resistance ?? "");
                doc.ReplaceText("[Remark]", reportModel.Remark ?? "");
                doc.ReplaceText("[Dimension]", reportModel.Dimension ?? "");
                doc.ReplaceText("[DimensionActual]", reportModel.DimensionActual ?? "");
                //写入XRF数据表
                Table mainTable = doc.Tables[0];
                if (mainTable!=null)
                {
                    Paragraph p = mainTable.Rows[9].Cells[0].Paragraphs[0];
                    //判断xrf数据内容是否以No开头
                    if (reportModel.CompositionXRF.StartsWith("No"))
                    {
                        string[] lines = reportModel.CompositionXRF.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        int rowCount = lines.Count();
                        int columnCount = lines[0].Split(',').Count();
                        Table xrfTable = doc.AddTable(rowCount,columnCount);
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
                        p.Append(reportModel.CompositionXRF);
                    }


                }

                doc.Save();
            }
        }
    }
}
