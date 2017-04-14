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
    /// <summary>
    /// 订单报告
    /// </summary>
    public class ReportMaterialOrder
    {
        public void Output(DcMaterialOrder order)
        {
            if (order == null)
            {
                return;
            }
            var sourceFilePath = Path.Combine(Environment.CurrentDirectory, "DocTemplate", "Reports", "MaterialOrder.docx");
            var targetFilePath = Path.Combine(Environment.CurrentDirectory, "DocTemplate", "Reports", "MaterialOrder_Temp.docx");
            var finalFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var finalFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), finalFileName + ".docx");


            if (!File.Exists(sourceFilePath))
            {
                return;
            }
            if (File.Exists(targetFilePath))
            {
                File.Delete(targetFilePath);
            }
            File.Copy(sourceFilePath, targetFilePath);


            //写入数据到文件
            using (var doc = DocX.Load(targetFilePath))
            {
                doc.ReplaceText("[OrderPO]", order.OrderPO ?? "");
                doc.ReplaceText("[SupplierName]", order.Supplier ?? "");
                doc.ReplaceText("[SupplierReceiver]", order.SupplierReceiver ?? "");
                doc.ReplaceText("[SupplierEmail]", order.SupplierEmail ?? "");
                doc.ReplaceText("[SupplierAddress]", order.SupplierAddress ?? "");
                doc.ReplaceText("[OrderDate]", order.CreateTime.ToString("MM/dd/yyyy"));
                doc.ReplaceText("[Creator]", order.Creator ?? "Leon.Chiu");


                List<DcMaterialOrderItem> OrderItems;

                using (var service = new MaterialOrderServiceClient())
                {
                    var result = service.GetMaterialOrderItembyMaterialID(order.ID);
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
                        p.Append(item.Weight.ToString("N2") + "kgs");

                        p = mainTable.Rows[i + 1].Cells[2].Paragraphs[0];
                        p.Append(item.PMINumber);

                        p = mainTable.Rows[i + 1].Cells[3].Paragraphs[0];
                        item.Description = $"Processing fee to cast {item.Purity} [{item.Composition}] atomic%;please deliver by {item.DeliveryDate.ToShortDateString()};";
                        if (!string.IsNullOrEmpty(item.ProvideRawMaterial.Trim()))
                        {
                            item.Description += $"(PMI to provide { item.ProvideRawMaterial})";
                        }
                        p.Append(item.Description);
                        p.AppendLine();

                        p = mainTable.Rows[i + 1].Cells[4].Paragraphs[0];
                        p.Append(item.UnitPrice.ToString("N0") + "RMB");

                        p = mainTable.Rows[i + 1].Cells[5].Paragraphs[0];
                        double total = item.UnitPrice * item.Weight;
                        p.Append(total.ToString("N0") + "RMB");
                        subTotalMoney += total;
                    }
                }

                doc.ReplaceText("[Remark]", order.Remark ?? "");
                doc.ReplaceText("[SubTotalMoney]", subTotalMoney.ToString("N0") + "RMB");
                doc.ReplaceText("[ShipFee]", order.ShipFee.ToString("N0") + "RMB");
                double totalMoney = subTotalMoney + order.ShipFee;
                doc.ReplaceText("[TotalMoney]", totalMoney.ToString("N0") + "RMB");

                doc.Save();
            }

            if (File.Exists(finalFilePath))
            {
                File.Delete(finalFileName);
            }

            File.Copy(targetFilePath, finalFilePath);

        }

    }
}

