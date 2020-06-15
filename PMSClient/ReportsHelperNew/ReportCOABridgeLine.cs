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
    /// 配合COABridgeLine200324使用
    /// </summary>
    public class ReportCOABridgeLine : ReportBase
    {
        public DcRecordTest model;
        public ReportCOABridgeLine()
        {
            model = null;
            imageType = ImageType.Target;
            IsOpenAfterCreated = true;
        }
        public void SetParameters(DcRecordTest model, ImageType imageType, bool isopen = true)
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

            string source = Path.Combine(reportsFolder, "COABridgeLine200324.docx");
            string temp = Path.Combine(reportsFolder, "Temp", "COABridgeLine200324.docx");
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
                string specDetail = "None";
                using (var orderService = new OrderServiceClient())
                {
                    var order = orderService.GetOrderByPMINumber(model.PMINumber);
                    if (order != null)
                    {
                        purity = order.Purity;
                        specDetail = order.DimensionDetails;
                    }
                }
                doc.ReplaceText("[Purity]", purity);
                doc.ReplaceText("[SpecDetail]", specDetail);

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
                doc.ReplaceText("[Roughness]", model.Roughness ?? "");

                doc.ReplaceText("[TargetParallelism]", model.Parallelism ?? "");

                //如果是是230mm的靶材，查找背板编号填入,从绑定记录中查询
                string platelot = "None", platespec = "None", plateDimensionActual = "None", plateParallelism = "None";
                if (model.Dimension.Contains("230"))
                {
                    using (var service = new RecordBondingServiceClient())
                    {
                        var record = service.GetRecordBondingByProductID(model.ProductID.Trim()).FirstOrDefault();
                        if (record != null)
                        {
                            string plateid = record.PlateLot;
                            platelot = plateid;
                        }
                        else
                        {
                            platelot = "No Bonding";
                        }
                    }
                }
                else
                {
                    //查询附带背板信息
                    if (!string.IsNullOrEmpty(model.BackingPlateLot) || model.BackingPlateLot != "无")
                    {
                        using (var plateServer = new PlateServiceClient())
                        {
                            var plateInfo = plateServer.GetPlateByPlateID(model.BackingPlateLot).FirstOrDefault();

                            if (plateInfo != null)
                            {
                                platespec = plateInfo.Dimension;
                                platelot = plateInfo.PlateLot;
                                plateDimensionActual = plateInfo.DimensionActual;
                                plateParallelism = plateInfo.Parallelism;
                            }
                        }
                    }
                }
                doc.ReplaceText("[PlateSpec]", platespec);
                doc.ReplaceText("[PlateLot]", platelot);
                doc.ReplaceText("[PlateActual]", plateDimensionActual);
                doc.ReplaceText("[PlateParallelism]", plateParallelism);


                //写入CSCAN Flaw Data
                string flawarea = "";
                if (model.CScan == null || model.CScan == "" || model.CScan.Contains("无"))
                {
                    flawarea = "";
                }
                else
                {
                    flawarea = model.CScan;
                }
                doc.ReplaceText("[FlawArea]", flawarea);


                int currentRowIndex = 17;

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
                            specRow.Cells[cell_index].Paragraphs[0].Append($"{item.Name}={item.Value}")
                                .Font(new System.Drawing.FontFamily("等线"))
                                .FontSize(8);
                            cell_index++;
                        }

                    }

                    currentRowIndex += 3;
                    Paragraph p = mainTable.Rows[currentRowIndex].Cells[0].Paragraphs[0];
                    string xrfWithStdDev = model.CompositionXRF;
                    //如果是bridgeline的数据，计算并追加标准差

                    int lineCount = COAHelper.SplitXRF(model.CompositionXRF).Count();
                    if (lineCount >= 7)
                    {
                        var stddev = new ReportDataProcessHelper();
                        xrfWithStdDev = stddev.AppendStdDev(model.CompositionXRF);
                    }
                    InsertCompositionXRFTable(doc, p, xrfWithStdDev, "No Composition Test Results");


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
                        int fix_size = 250;
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
                            cell.Width = 60;
                            string s = items[j];
                            if (j > 0 && i > 0)
                            {
                                if (double.TryParse(s, out double val_temp))
                                {
                                    s = val_temp.ToString("F2");
                                }
                            }
                            cell.Paragraphs[0].Append(s).FontSize(8)
                                .Font(new FontFamily("等线")).Alignment = Alignment.right;
                            //if (j == 0)
                            //{
                            //    continue;
                            //}
                        }
                    }
                    p.InsertTableBeforeSelf(xrfTable);


                    ////添加平均成分到表格中

                    //StringBuilder sb1 = new StringBuilder();//收集数字
                    //StringBuilder sb2 = new StringBuilder();//收集单位
                    ////获取倒数第二行
                    //string average_line = lines[lines.Count() - 2];

                    //string[] items2 = average_line.Split(',');
                    //for (int j = 1; j < items2.Count(); j++)
                    //{
                    //    sb1.Append(items2[j] + "\r");
                    //    sb2.Append("Atm%" + "\r");
                    //}


                    //Table mainTable = document.Tables[0];
                    //Paragraph average = mainTable.Rows[7].Cells[3].Paragraphs[0];
                    //Paragraph units = mainTable.Rows[7].Cells[4].Paragraphs[0];
                    //average.Append(sb1.ToString()).FontSize(9)
                    //    .Font(new System.Drawing.FontFamily("等线")).Bold();
                    //units.Append(sb2.ToString()).FontSize(9)
                    //    .Font(new System.Drawing.FontFamily("等线")).Bold();
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
