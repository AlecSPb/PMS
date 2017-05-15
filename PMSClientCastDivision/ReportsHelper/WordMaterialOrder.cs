using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.SanjieService;
using Novacode;
using System.IO;
using System.Drawing;
namespace PMSClient.ReportsHelper
{
    /// <summary>
    /// 订单报告
    /// </summary>
    public class WordMaterialOrder : ReportBase
    {
        private string prefix = "原料订单";
        public WordMaterialOrder()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "MaterialOrder.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "MaterialOrder_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }

        public void SetTargetFolder(string targetFolder)
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            targetFile = Path.Combine(targetFolder, targetName);
        }

        private DcMaterialOrder _order;
        public void SetModel(DcMaterialOrder order)
        {
            _order = order;
        }
        public override void Output()
        {
            try
            {
                if (_order == null)
                {
                    return;
                }
                //复制到临时文件
                ReportHelper.FileCopy(sourceFile, tempFile);
                #region 创建文档
                using (var doc = DocX.Load(tempFile))
                {
                    doc.ReplaceText("[OrderPO]", _order.OrderPO ?? "");
                    doc.ReplaceText("[SupplierName]", _order.Supplier ?? "");
                    doc.ReplaceText("[SupplierReceiver]", _order.SupplierReceiver ?? "");
                    doc.ReplaceText("[SupplierEmail]", _order.SupplierEmail ?? "");
                    doc.ReplaceText("[SupplierAddress]", _order.SupplierAddress ?? "");
                    doc.ReplaceText("[OrderDate]", _order.CreateTime.ToString("MM/dd/yyyy"));
                    doc.ReplaceText("[Creator]", _order.Creator ?? "Leon.Chiu");


                    List<DcMaterialOrderItem> OrderItems;

                    using (var service = new SanjieServiceClient())
                    {
                        var result = service.GetMaterialOrderItembyMaterialID(_order.ID);
                        OrderItems = result.OrderBy(i => i.CreateTime).ToList();
                    }
                    //插入表格
                    var mainTable = doc.Tables[1];
                    double subTotalMoney = 0;
                    if (mainTable != null)
                    {
                        for (int i = 0; i < OrderItems.Count; i++)
                        {
                            var item = OrderItems[i];
                            Paragraph p;
                            p = mainTable.Rows[i + 1].Cells[0].Paragraphs[0];
                            p.Append(item.OrderItemNumber);

                            p = mainTable.Rows[i + 1].Cells[1].Paragraphs[0];
                            p.Append(item.Weight.ToString("N2") + "kg");

                            p = mainTable.Rows[i + 1].Cells[2].Paragraphs[0];
                            p.Append(item.PMINumber);

                            p = mainTable.Rows[i + 1].Cells[3].Paragraphs[0];
                            var descriptionMesseage = $"Processing fee to cast {item.Purity} [{item.Composition}] atomic%;please deliver by {item.DeliveryDate.ToShortDateString()};";
                            if (!string.IsNullOrEmpty(item.ProvideRawMaterial.Trim()))
                            {
                                descriptionMesseage += $"(PMI to provide { item.ProvideRawMaterial})";
                            }
                            descriptionMesseage += item.Description;

                            p.Append(descriptionMesseage);
                            p.AppendLine();

                            p = mainTable.Rows[i + 1].Cells[4].Paragraphs[0];
                            p.Append(item.UnitPrice.ToString("N0") + "RMB");

                            p = mainTable.Rows[i + 1].Cells[5].Paragraphs[0];
                            double total = item.UnitPrice * item.Weight;
                            p.Append(total.ToString("N0") + "RMB");
                            subTotalMoney += total;
                            if (i > 5)
                            {
                                break;
                            }
                        }
                    }
                    var remark = _order.Remark ?? "";
                    if (remark != "")
                    {
                        remark = $"PMI to provide:{remark}";
                    }
                    doc.ReplaceText("[Remark]", remark);
                    doc.ReplaceText("[SubTotalMoney]", subTotalMoney.ToString("N0") + "RMB");
                    doc.ReplaceText("[ShipFee]", _order.ShipFee.ToString("N0") + "RMB");
                    double totalMoney = subTotalMoney + _order.ShipFee;
                    doc.ReplaceText("[TotalMoney]", totalMoney.ToString("N0") + "RMB");

                    doc.Save();
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
                PMSDialogService.ShowYes("原材料报告创建成功，请在桌面查看");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

    }
}

