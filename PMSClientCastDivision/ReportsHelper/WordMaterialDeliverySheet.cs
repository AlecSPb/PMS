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
    public class WordMaterialDeliverySheet : ReportBase
    {
        private string prefix = "原料发货单";
        public WordMaterialDeliverySheet()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "MaterialDeliverySheet.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "MaterialDeliverySheet_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }

        public void SetTargetFolder(string targetFolder)
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            targetFile = Path.Combine(targetFolder, targetName);
        }

        private List<DcMaterialOrderItemExtra> dataList;
        public void SetModel(List<DcMaterialOrderItemExtra> selectedItems)
        {
            if (selectedItems==null)
            {
                dataList = new List<DcMaterialOrderItemExtra>();
            }
            else
            {
                dataList = selectedItems;
            }
        }
        public override void Output()
        {
            try
            {
                if (dataList.Count ==0)
                {
                    return;
                }
                //复制到临时文件
                ReportHelper.FileCopy(sourceFile, tempFile);
                #region 创建文档
                using (var doc = DocX.Load(tempFile))
                {
                    doc.ReplaceText("[DeliveryDate]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    //插入表格
                    var mainTable = doc.Tables[1];
                    if (mainTable != null)
                    {
                        for (int i = 0; i < dataList.Count; i++)
                        {
                            var item =dataList[i];
                            Paragraph p;
                            p = mainTable.Rows[i + 1].Cells[0].Paragraphs[0];
                            p.Append(item.MaterialOrderItem.OrderItemNumber).FontSize(8);
                            p = mainTable.Rows[i + 1].Cells[1].Paragraphs[0];
                            p.Append(item.MaterialOrderItem.Composition).FontSize(8).Bold();
                            p = mainTable.Rows[i + 1].Cells[2].Paragraphs[0];
                            p.Append(item.MaterialOrderItem.Purity).FontSize(8).Alignment=Alignment.center;
                            p = mainTable.Rows[i + 1].Cells[3].Paragraphs[0];
                            p.Append(item.MaterialOrderItem.Weight.ToString("F3")).FontSize(8).Bold().Alignment = Alignment.right;
                            p = mainTable.Rows[i + 1].Cells[4].Paragraphs[0];
                            p.Append(item.MaterialOrderItem.PMINumber).FontSize(8).Bold();
                            p = mainTable.Rows[i + 1].Cells[5].Paragraphs[0];
                            p.Append(item.MaterialOrder.OrderPO).FontSize(8);
                            if (i > 13)
                            {
                                break;
                            }
                        }
                    }

                    doc.Save();
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
                PMSDialogService.ShowYes("发货单创建成功，请在桌面查看");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

    }
}

