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
    public class ReportMaterialOrderHorizontal : ReportBase
    {
        private string prefix = "原料订单横版";
        public ReportMaterialOrderHorizontal()
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "MaterialOrderHorizontal.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "MaterialOrderHorizontal_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }

        public void SetTargetFolder(string targetFolder)
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
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

                    using (var service = new MaterialOrderServiceClient())
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
                            p.Append(item.OrderItemNumber).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[1].Paragraphs[0];
                            p.Append(item.Weight.ToString("N2")).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[2].Paragraphs[0];
                            p.Append($"[{item.Purity}] {item.Composition}").FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[3].Paragraphs[0];
                            p.Append(item.PMINumber).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[4].Paragraphs[0];
                            p.Append(item.DeliveryDate.ToString("yyMMdd")).FontSize(8);


                            p = mainTable.Rows[i + 1].Cells[5].Paragraphs[0];
                            var descriptionMesseage = "";
                            if (!string.IsNullOrEmpty(item.ProvideRawMaterial.Trim()))
                            {
                                descriptionMesseage = $"(PMI to provide { item.ProvideRawMaterial})";
                            }
                            descriptionMesseage += item.Description;

                            p.Append(descriptionMesseage).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[6].Paragraphs[0];
                            p.Append(item.UnitPrice.ToString("N0")).FontSize(8);

                            p = mainTable.Rows[i + 1].Cells[7].Paragraphs[0];
                            double total = item.UnitPrice * item.Weight;
                            p.Append(total.ToString("N0")).FontSize(8);
                            subTotalMoney += total;
                        }
                    }
                    var remark = _order.Remark ?? "";
                    if (remark != "")
                    {
                        remark = $"PMI to provide:{remark}";
                    }
                    doc.ReplaceText("[Remark]", remark);
                    doc.ReplaceText("[SubTotalMoney]", subTotalMoney.ToString("N0"));
                    doc.ReplaceText("[ShipFee]", _order.ShipFee.ToString("N0") );
                    double totalMoney = subTotalMoney + _order.ShipFee;
                    doc.ReplaceText("[TotalMoney]", totalMoney.ToString("N0") );

                    doc.Save();
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

    }
}

