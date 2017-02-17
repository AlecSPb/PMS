using DocGenerator.DocModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;

namespace DocGenerator
{
    public class GeneratorMaterialOrder : GeneratorBase, IDoc<MaterialOrder>
    {
        public void Generate(string sourceFilePath, string targetFilePath, MaterialOrder reportModel)
        {
            //复制文件
            CopyTemplate(sourceFilePath, targetFilePath);
            //写入数据到文件
            using (var doc = DocX.Load(targetFilePath))
            {
                doc.ReplaceText("[OrderPO]", reportModel.OrderPO ?? "");
                doc.ReplaceText("[SupplierName]", reportModel.Supplier ?? "");
                doc.ReplaceText("[SupplierReceiver]", reportModel.SupplierReceiver ?? "");
                doc.ReplaceText("[SupplierEmail]", reportModel.SupplierEmail ?? "");
                doc.ReplaceText("[SupplierAddress]", reportModel.SupplierAddress ?? "");
                doc.ReplaceText("[OrderDate]", reportModel.CreateTime.ToString("MM/dd/yyyy"));

                //插入表格

                var mainTable = doc.Tables[1];
                double subTotalMoney = 0;
                if (mainTable != null)
                {
                    for (int i = 0; i < reportModel.MaterialOrderItems.Count; i++)
                    {
                        var item = reportModel.MaterialOrderItems[i];
                        Paragraph p;
                        p = mainTable.Rows[i + 1].Cells[0].Paragraphs[0];
                        p.Append((i + 1).ToString());

                        p = mainTable.Rows[i + 1].Cells[1].Paragraphs[0];
                        p.Append(item.Weight.ToString("N2") + "kgs");

                        p = mainTable.Rows[i + 1].Cells[2].Paragraphs[0];
                        p.Append(item.PMIWorkNumber);

                        p = mainTable.Rows[i + 1].Cells[3].Paragraphs[0];
                        item.Description = $"Processing fee to cast {item.Purity} {item.Composition} atomic%;please deliver by {item.DeliveryDate.ToShortDateString()};";
                        if (!string.IsNullOrEmpty(item.ProvideRawMaterial.Trim()))
                        {
                            item.Description += $"(PMI to provide  { item.ProvideRawMaterial})";
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

                doc.ReplaceText("[Remark]", reportModel.Remark ?? "");
                doc.ReplaceText("[SubTotalMoney]", subTotalMoney.ToString("N0") + "RMB");
                doc.ReplaceText("[ShipFee]", reportModel.ShipFee.ToString("N0") + "RMB");
                double totalMoney = subTotalMoney + reportModel.ShipFee;
                doc.ReplaceText("[TotalMoney]", totalMoney.ToString("N0") + "RMB");

                doc.Save();
            }
        }
    }
}
