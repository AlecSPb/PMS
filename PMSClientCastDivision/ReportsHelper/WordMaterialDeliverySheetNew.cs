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
    public class WordMaterialDeliverySheetNew : ReportBase
    {
        private string prefix = "原料发货单";
        public WordMaterialDeliverySheetNew()
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

        public override void Output()
        {
            try
            {
                //复制到临时文件
                ReportHelper.FileCopy(sourceFile, tempFile);
                #region 创建文档
                using (var doc = DocX.Load(tempFile))
                {
                    doc.ReplaceText("[DeliveryDate]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    //插入表格
                    using (var s = new SanjieServiceClient())
                    {
                        var ds = s.GetMaterialInventoryInTemporary();
                        var mainTable = doc.Tables[1];
                        if (mainTable != null)
                        {
                            int row_index = 0;
                            foreach (var item in ds)
                            {
                                Paragraph p;
                                p = mainTable.Rows[row_index + 1].Cells[0].Paragraphs[0];
                                p.Append(item.MaterialLot ?? "").FontSize(8);
                                p = mainTable.Rows[row_index + 1].Cells[1].Paragraphs[0];
                                p.Append(item.Composition ?? "").FontSize(8).Bold();
                                p = mainTable.Rows[row_index + 1].Cells[2].Paragraphs[0];
                                p.Append(item.Purity ?? "").FontSize(8).Alignment = Alignment.center;
                                p = mainTable.Rows[row_index + 1].Cells[3].Paragraphs[0];
                                p.Append(item.Weight.ToString("F3")).FontSize(8).Bold().Alignment = Alignment.right;
                                p = mainTable.Rows[row_index + 1].Cells[4].Paragraphs[0];
                                p.Append(item.PMINumber ?? "").FontSize(8).Bold();

                                p = mainTable.Rows[row_index + 1].Cells[5].Paragraphs[0];
                                p.Append(item.Remark ?? "").FontSize(8);

                                p = mainTable.Rows[row_index + 1].Cells[6].Paragraphs[0];
                                p.Append(item.SupplierPO??"").FontSize(8);
                                row_index++;
                            }
                        }

                    }
                    doc.Save();
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
                PMSDialogService.ShowYes("发货单创建成功，请在桌面查看");
                System.Diagnostics.Process.Start(targetFile);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

    }
}

