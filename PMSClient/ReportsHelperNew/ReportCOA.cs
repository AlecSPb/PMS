using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using Novacode;
using System.Drawing;
using System.Text.RegularExpressions;
using PMSClient.Components.CscanImageProcess;

namespace PMSClient.ReportsHelperNew
{
    /// <summary>
    /// 配合COA200324使用
    /// </summary>
    public class ReportCOA : ReportBase
    {
        public DcRecordTest model;
        public ReportCOA()
        {
            model = null;
            imageType = ImageType.Target;
            IsOpenAfterCreated = true;
        }
        public void SetParameters(DcRecordTest model, ImageType imageType,bool isopen=true)
        {
            this.model = model;
            this.imageType = imageType;
            IsOpenAfterCreated = isopen;
        }
        private ImageType imageType;
        private bool IsOpenAfterCreated;

        public override void Output()
        {
            //ResetParameters();

            string source = Path.Combine(reportsFolder, "COA200324.docx");
            string temp = Path.Combine(reportsFolder, "Temp", "COA200324.docx");
            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                string printTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                doc.ReplaceText("[PrintTime]", printTime ?? "");
                Table table = doc.Tables[0];
                #region 基本字段
                doc.ReplaceText("[Customer]", model.Customer ?? "");
                string productid = (model.CompositionAbbr ?? "") + "-" + (model.ProductID ?? "");
                doc.ReplaceText("[ProductID]", productid ?? "");

                doc.ReplaceText("[LotNumber]", model.ProductID);
                doc.ReplaceText("[PO]", model.PO ?? "");

                string purity = "99.995%默认";
                using (var orderService = new OrderServiceClient())
                {
                    var order = orderService.GetOrderByPMINumber(model.PMINumber);
                    if (order != null)
                    {
                        purity = order.Purity;
                    }
                }
                doc.ReplaceText("[Purity]", purity);

                doc.ReplaceText("[COADate]", DateTime.Now.ToString("MM/dd/yyyy"));
                doc.ReplaceText("[Composition]", model.Composition ?? "");
                doc.ReplaceText("[Dimension]", COAHelper.StandardizeDimension(model.Dimension) ?? "");
                doc.ReplaceText("[Weight]", model.Weight ?? "");
                doc.ReplaceText("[Density]", model.Density ?? "");
                doc.ReplaceText("[Resistance]", model.Resistance ?? "");
                doc.ReplaceText("[DimensionActual]", COAHelper.StandardizeDimension(model.DimensionActual) ?? "");

                doc.ReplaceText("[OrderDate]", COAHelper.GetCreateDateFromPMINumber(model.PMINumber));
                doc.ReplaceText("[CreateDate]", DateTime.Now.ToString("MM/dd/yyyy"));

                //粗糙度值
                if (model.ProductID.Contains("#"))
                {
                    doc.ReplaceText("[Roughness]", model.Roughness);
                }
                else
                {
                    doc.ReplaceText("[Roughness]", "-");
                }


                //如果是是230mm的靶材，查找背板编号填入
                if (model.Dimension.Contains("230"))
                {
                    using (var service = new RecordBondingServiceClient())
                    {
                        var record = service.GetRecordBondingByProductID(model.ProductID.Trim()).FirstOrDefault();
                        if (record != null)
                        {
                            string plateid = record.PlateLot;
                            doc.ReplaceText("[PlateID]", plateid);
                        }
                        else
                        {
                            doc.ReplaceText("[PlateID]", "No Bonding");
                        }
                    }
                }
                else
                {
                    doc.ReplaceText("[PlateID]", "None");
                }


                int currentRowIndex = 14;

                #region 填充表格
                //填充XRF表格
                if (doc.Tables[0] != null)
                {
                    Table mainTable = doc.Tables[0];

                    //填充标称的成分
                    if (!string.IsNullOrEmpty(model.Composition))
                    {
                        Row specRow = mainTable.Rows[currentRowIndex];
                        int cell_index = 0;
                        foreach (var item in GetCompositionNameAndValues(model.Composition))
                        {
                            specRow.Cells[cell_index].Paragraphs[0].Append($"{item.Name}={item.Value}Atm%")
                                .Font(new System.Drawing.FontFamily("等线"))
                                .FontSize(8);
                            cell_index++;
                        }

                    }

                    currentRowIndex += 3;

                    Paragraph p = mainTable.Rows[currentRowIndex].Cells[0].Paragraphs[0];
                    //填充成分
                    InsertCompositionXRFTable(doc, p, model.CompositionXRF, "No Composition Test Results");


                    //填充图像
                    var manager = new Components.CscanImageProcess.ImageManager();
                    var result = manager.GetImage(model.ProductID, imageType);
                    if (imageType == ImageType.Bonding)
                    {
                        doc.ReplaceText("[CSCAN]", "C-SCAN");
                    }
                    else
                    {
                        doc.ReplaceText("[CSCAN]", "A-SCAN");
                    }
                    Paragraph image_p = mainTable.Rows[currentRowIndex].Cells[1].Paragraphs[0];
                    if (result.IsFound)
                    {
                        Novacode.Image img = doc.AddImage(result.ImagePath);
                        var pic = img.CreatePicture();
                        int fix_size = 140;
                        pic.Width = fix_size;
                        pic.Height = fix_size;
                        image_p.AppendPicture(pic);
                    }
                    else
                    {
                        image_p.AppendLine("NO IMAGE FOUND");
                    }
                }
                #endregion

                #endregion
                doc.Save();
            }
            File.Copy(temp, wordFileName, true);
            //PMSDialogService.Show("生成成功，即将打开");
            if (IsOpenAfterCreated)
            {
                System.Diagnostics.Process.Start(wordFileName);
            }

        }

        /// <summary>
        /// 填充XRF成分
        /// </summary>
        /// <param name="document"></param>
        /// <param name="p"></param>
        /// <param name="xrfComposition"></param>
        /// <param name="noCompositionMessage"></param>
        private void InsertCompositionXRFTable(DocX document, Paragraph p, string xrfComposition, string noCompositionMessage)
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

                    for (int i = 0; i < lines.Count(); i++)
                    {
                        string[] items = lines[i].Split(',');
                        for (int j = 0; j < items.Count(); j++)
                        {
                            Cell cell = xrfTable.Rows[i].Cells[j];
                            cell.Width = 60;
                            cell.Paragraphs[0].Append(items[j])
                                .FontSize(8)
                                .Font(new System.Drawing.FontFamily("等线"));
                        }
                    }
                    p.InsertTableBeforeSelf(xrfTable);
                }

            }
            else
            {
                p.InsertText(noCompositionMessage);
            }
        }

        /// <summary>
        /// 返回成分名称和值
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        private List<Element> GetCompositionNameAndValues(string composition)
        {
            List<Element> elements = new List<Element>();
            var matches = Regex.Matches(composition, @"([a-zA-Z]+)([0-9]+([.]{1}[0-9]+){0,1})");
            foreach (Match item in matches)
            {
                string short_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                elements.Add(new Element { Name = short_name, Value = element_value });
            }

            return elements;
        }

    }
}
