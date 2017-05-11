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
    public class ReportDeliverySheet : ReportBase
    {
        private string prefix = "发货清单";
        public ReportDeliverySheet()
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "DeliverySheet.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "DeliverySheet_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }
        public void SetTargetFolder(string targetFolder)
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            targetFile = Path.Combine(targetFolder, targetName);
        }
        public void SetModel(DcDelivery model)
        {
            if (model != null)
            {
                this.model = model;
            }
        }

        private DcDelivery model;
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
                    document.ReplaceText("[CreateTime]", DateTime.Now.ToString("yyyy-MM-dd"));
                    document.ReplaceText("[ShipTime]", model.ShipTime.ToString("yyyy-MM-dd"));
                    document.ReplaceText("[Country]", model.Country ?? "");
                    document.ReplaceText("[DeliveryName]", model.DeliveryName ?? "");
                    document.ReplaceText("[DeliveryNumber]", model.DeliveryNumber ?? "");
                    document.ReplaceText("[InvoiceNumber]", model.InvoiceNumber ?? "");

                    if (document.Tables[0] != null)
                    {
                        Table mainTable = document.Tables[1];

                        using (var service = new DeliveryServiceClient())
                        {
                            var result = service.GetDeliveryItemByDeliveryID(model.ID).OrderBy(i => i.PackNumber);
                            int rownumber = 1;
                            int datanumber = 1;
                            foreach (var item in result)
                            {
                                mainTable.Rows[rownumber].Cells[0].Paragraphs[0].Append(datanumber.ToString()).FontSize(10));
                                mainTable.Rows[rownumber].Cells[1].Paragraphs[0].Append(item.ProductID).FontSize(10));
                                mainTable.Rows[rownumber].Cells[2].Paragraphs[0].Append(item.ProductType).FontSize(10));
                                mainTable.Rows[rownumber].Cells[3].Paragraphs[0].Append(item.Composition).FontSize(10));
                                mainTable.Rows[rownumber].Cells[4].Paragraphs[0].Append(item.Customer).FontSize(10));
                                mainTable.Rows[rownumber].Cells[5].Paragraphs[0].Append(item.PO).FontSize(10));
                                mainTable.Rows[rownumber].Cells[6].Paragraphs[0].Append(item.Dimension).FontSize(10));
                                mainTable.Rows[rownumber].Cells[7].Paragraphs[0].Append(item.PackNumber.ToString()).FontSize(10));

                                mainTable.InsertRow();
                                datanumber++;
                                rownumber++;
                            }
                        }

                    }
                    document.Save();
                    #endregion
                }
                #endregion
                //复制到临时文件
                var targetName = $"{prefix}_{model.DeliveryName}.docx";
                targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
                ReportHelper.FileCopy(tempFile, targetFile);


            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
