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
    public class WordMaterialOrderHorizontal : ReportBase
    {
        private string prefix = "原料订单横版";
        public WordMaterialOrderHorizontal()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "MaterialOrderHorizontal.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "MaterialOrderHorizontal_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }


        private DcMaterialOrder model;
        public void SetModel(DcMaterialOrder order)
        {
            if (order != null)
            {
                model = order;
                CreateFolderOnDesktop();
                var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
                targetFile = Path.Combine(targetDir, targetName);
            }
        }
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
                using (var doc = DocX.Load(tempFile))
                {
                    doc.ReplaceText("[OrderPO]", model.OrderPO ?? "");
                    doc.ReplaceText("[SupplierName]", model.Supplier ?? "");
                    doc.ReplaceText("[SupplierReceiver]", model.SupplierReceiver ?? "");
                    doc.ReplaceText("[SupplierEmail]", model.SupplierEmail ?? "");
                    doc.ReplaceText("[SupplierAddress]", model.SupplierAddress ?? "");
                    doc.ReplaceText("[OrderDate]", model.CreateTime.ToString("MM/dd/yyyy"));
                    doc.ReplaceText("[Creator]", model.Creator ?? "Leon.Chiu");


                    List<DcMaterialOrderItem> OrderItems;

                    using (var service = new MaterialOrderServiceClient())
                    {
                        var result = service.GetMaterialOrderItembyMaterialID(model.ID);
                        OrderItems = result.OrderBy(i => i.CreateTime).ToList();
                    }
                    //插入表格
                    var mainTable = doc.Tables[1];
                    double subTotalMoney = 0;
                    double elementValue = 0;
                    if (mainTable != null)
                    {
                        for (int i = 0; i < OrderItems.Count; i++)
                        {
                            //最多不超过13个记录
                            if (i > 13)
                            {
                                break;
                            }
                            var item = OrderItems[i];
                            Paragraph p;
                            p = mainTable.Rows[i + 1].Cells[0].Paragraphs[0];
                            p.Append(item.OrderItemNumber).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[1].Paragraphs[0];
                            p.Append(item.Weight.ToString("N2")).FontSize(8).Bold();

                            p = mainTable.Rows[i + 1].Cells[2].Paragraphs[0];
                            p.Append($"[{item.Purity}] {item.Composition}").FontSize(8).Bold();

                            p = mainTable.Rows[i + 1].Cells[3].Paragraphs[0];
                            p.Append(item.PMINumber).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[4].Paragraphs[0];
                            p.Append(item.DeliveryDate.ToString("yyMMdd")).FontSize(8);


                            p = mainTable.Rows[i + 1].Cells[5].Paragraphs[0];
                            var descriptionMesseage = "";
                            if (!string.IsNullOrEmpty(item.ProvideRawMaterial.Trim()))
                            {
                                descriptionMesseage = $"PMI to provide { item.ProvideRawMaterial}；{item.Description}";
                            }
                            descriptionMesseage += item.Description;

                            p.Append(descriptionMesseage).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[6].Paragraphs[0];
                            p.Append(item.MaterialPrice.ToString("N2")).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[7].Paragraphs[0];
                            p.Append(item.UnitPrice.ToString("N2")).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[8].Paragraphs[0];
                            double total = item.UnitPrice * item.Weight;
                            p.Append(total.ToString("N2")).FontSize(8);
                            subTotalMoney += total;

                            elementValue += item.MaterialPrice;
                        }
                    }
                    var remark = model.Remark ?? "";
                    if (remark != "")
                    {
                        remark = $"PMI to provide:{remark}";
                    }
                    doc.ReplaceText("[Remark]", remark);
                    doc.ReplaceText("[SubTotalMoney]", subTotalMoney.ToString("N2"));
                    doc.ReplaceText("[ElementValue]", elementValue.ToString("N2"));

                    doc.ReplaceText("[ShipFee]", model.ShipFee.ToString("N2"));
                    double totalMoney = subTotalMoney + model.ShipFee + elementValue;
                    doc.ReplaceText("[TotalMoney]", totalMoney.ToString("N2"));

                    doc.Save();
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
                PMSDialogService.Show("原材料报告创建成功，请在桌面查看");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

    }
}

