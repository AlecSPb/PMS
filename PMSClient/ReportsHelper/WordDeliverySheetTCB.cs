﻿using Novacode;
using PMSClient.MainService;
using System;
using System.IO;
using System.Linq;

namespace PMSClient.ReportsHelper
{
    public class WordDeliverySheetTCB : ReportBase
    {
        private string prefix = "发货清单";
        public WordDeliverySheetTCB()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "DeliverySheet200324_tcb.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "DeliverySheet200324_tcb_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }
        public void SetModel(DcDelivery model)
        {
            if (model != null)
            {
                this.model = model;
                CreateFolderOnDesktop();
                var targetName = $"{prefix}_{model.DeliveryName}.docx";
                targetFile = Path.Combine(targetDir, targetName);
            }
        }
        private string sheetType = "";
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


                bool isCompositonCoverd = false;
                if (PMSDialogService.ShowYesNo("请问", "要隐藏真实成分吗？"))
                {
                    isCompositonCoverd = true;
                }

                //写入数据到文件
                #region 创建文档
                using (DocX document = DocX.Load(tempFile))
                {
                    #region 基本字段
                    document.ReplaceText("[CreateTime]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    document.ReplaceText("[ShipTime]", model.ShipTime.ToString("yyyy-MM-dd"));
                    document.ReplaceText("[Country]", model.Country ?? "");
                    document.ReplaceText("[DeliveryName]", model.DeliveryName ?? "");
                    document.ReplaceText("[DeliveryNumber]", (model.DeliveryExpress ?? "") + " " + (model.DeliveryNumber ?? ""));
                    document.ReplaceText("[InvoiceNumber]", model.InvoiceNumber ?? "");

                    if (document.Tables[0] != null)
                    {
                        Table mainTable = document.Tables[1];

                        using (var service = new DeliveryServiceClient())
                        {
                            var result = service.GetDeliveryItemByDeliveryID(model.ID)
                                .OrderBy(i => i.OrderNumber)
                                .ThenBy(i => i.PackNumber)
                                .ThenBy(i => i.ProductID);
                            int rownumber = 1;
                            int datanumber = 1;
                            double table_font_size = 12;

                            foreach (var item in result)
                            {
                                mainTable.Rows[rownumber].Cells[0].Paragraphs[0].Append(datanumber.ToString())
                                    .FontSize(table_font_size).Alignment = Alignment.center;
                                mainTable.Rows[rownumber].Cells[1].Paragraphs[0].Append(item.ProductID).FontSize(table_font_size);

                                string compositon = "";
                                if (isCompositonCoverd)
                                {
                                    compositon = "BiTeSe";
                                }
                                else
                                {
                                    compositon = item.Composition;
                                }
                                mainTable.Rows[rownumber].Cells[3].Paragraphs[0].Append(compositon).FontSize(table_font_size);

                                mainTable.Rows[rownumber].Cells[4].Paragraphs[0].Append(item.Customer).FontSize(table_font_size);
                                mainTable.Rows[rownumber].Cells[5].Paragraphs[0].Append(item.Dimension).FontSize(table_font_size);

                                //判定是否要查找靶材的额外信息

                                if (item.ProductType != "背板")
                                {
                                    //查找230mm靶材的[绑定记录],并填写到背板编号栏
                                    if (item.Dimension.Trim().StartsWith("230"))
                                    {

                                        string bp_lot = Helpers.DeliveryHelper.GetBPLotFromBonding(item.ProductID);
                                        string new_plate_lot = bp_lot;
                                        mainTable.Rows[rownumber].Cells[6].Paragraphs[0]
                                            .Append(new_plate_lot)
                                            .FontSize(table_font_size).Alignment = Alignment.left;

                                        //显示背板使用次数
                                        int count_used = Helpers.DeliveryHelper.GetPlateUsedTimeFromPlateLot(bp_lot);
                                        mainTable.Rows[rownumber].Cells[9].Paragraphs[0]
                                                .Append(count_used.ToString())
                                                .FontSize(table_font_size).Alignment = Alignment.center;


                                    }
                                    else//在[测试记录]中查找非绑定靶材的背板记录
                                    {
                                        string bp_lot = Helpers.DeliveryHelper.GetBPLotFromTest(item.ProductID);
                                        mainTable.Rows[rownumber].Cells[6].Paragraphs[0]
                                                    .Append(bp_lot)
                                                    .FontSize(table_font_size).Alignment = Alignment.left;
                                    }
                                }

                                //按照文档中英文类型更改类型文字
                                string itemType = "";
                                if (sheetType == "English")
                                {
                                    if (item.ProductType.Contains("靶材"))
                                    {
                                        itemType = "Target";

                                    }
                                    else if (item.ProductType.Contains("背板"))
                                    {
                                        itemType = "BP";

                                    }
                                    else if (item.ProductType.Contains("绑定"))
                                    {
                                        itemType = "Bonding";

                                    }
                                    else
                                    {
                                        itemType = item.ProductType;
                                    }
                                }
                                else
                                {
                                    itemType = item.ProductType;
                                }
                                mainTable.Rows[rownumber].Cells[2].Paragraphs[0].Append(itemType).FontSize(table_font_size);

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
                ReportHelper.FileCopy(tempFile, targetFile);
                PMSDialogService.Show("发货清单创建成功，即将打开");
                System.Diagnostics.Process.Start(targetFile);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
