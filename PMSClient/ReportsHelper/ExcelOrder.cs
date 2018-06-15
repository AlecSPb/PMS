using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using PMSClient.MainService;
using PMSClient.CustomControls;

namespace PMSClient.ReportsHelper
{
    public class ExcelOrder : ReportBase
    {
        private string prefix = "订单";
        public ExcelOrder()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameXlsx}";
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
            Year = DateTime.Now.Year;
        }

        public int Year { get; set; }
        public override void Output()
        {
            try
            {
                var fileName = new FileInfo(targetFile);
                using (var excel = new ExcelPackage(fileName))
                {
                    var ws = excel.Workbook.Worksheets.Add("销售订单");
                    #region 表头
                    ws.Cells[1, 1].Value = "订单日期";
                    ws.Cells[1, 2].Value = "客户名称";
                    ws.Cells[1, 3].Value = "PO";
                    ws.Cells[1, 4].Value = "内部编号";
                    ws.Cells[1, 5].Value = "标准成分";
                    ws.Cells[1, 6].Value = "缩写";
                    ws.Cells[1, 7].Value = "产品类型";
                    ws.Cells[1, 8].Value = "纯度";
                    ws.Cells[1, 9].Value = "数量";
                    ws.Cells[1, 10].Value = "单位";
                    ws.Cells[1, 11].Value = "尺寸";
                    ws.Cells[1, 12].Value = "尺寸细节";
                    ws.Cells[1, 13].Value = "样品需求";
                    ws.Cells[1, 14].Value = "交付期限";
                    ws.Cells[1, 15].Value = "最低要求";
                    ws.Cells[1, 16].Value = "备注信息";
                    ws.Cells[1, 17].Value = "策略";
                    #endregion

                    using (var service = new OrderServiceClient())
                    {
                        int recordCount = 0;
                        int pageSize = 10;
                        int pageIndex = 1;
                        int skip = 0;
                        int take = pageSize;
                        recordCount = service.GetOrderCountByYear(Year);
                        if (recordCount==0)
                        {
                            PMSDialogService.Show("该年数据数目为0，请重新选择");
                            return;
                        }

                        int rowNumber = 2;
                        for (pageIndex = 1; pageIndex <= recordCount / pageSize + 1; pageIndex++)
                        {
                            skip = (pageIndex - 1) * pageSize;
                            var results = service.GetOrderByYear(skip, take, Year);
                            foreach (var item in results)
                            {
                                #region 表数据
                                ws.Cells[rowNumber, 1].Value = item.CreateTime.ToShortDateString();
                                ws.Cells[rowNumber, 2].Value = item.CustomerName;
                                ws.Cells[rowNumber, 3].Value = item.PO;
                                ws.Cells[rowNumber, 4].Value = item.PMINumber;
                                ws.Cells[rowNumber, 5].Value = item.CompositionStandard;
                                ws.Cells[rowNumber, 6].Value = item.CompositionAbbr;
                                ws.Cells[rowNumber, 7].Value = item.ProductType;
                                ws.Cells[rowNumber, 8].Value = item.Purity;
                                ws.Cells[rowNumber, 9].Value = item.Quantity;
                                ws.Cells[rowNumber, 10].Value = item.QuantityUnit;
                                ws.Cells[rowNumber, 11].Value = item.Dimension;
                                ws.Cells[rowNumber, 12].Value = item.DimensionDetails;
                                ws.Cells[rowNumber, 13].Value = item.SampleNeed;
                                ws.Cells[rowNumber, 14].Value = item.DeadLine.ToShortDateString();
                                ws.Cells[rowNumber, 15].Value = item.MinimumAcceptDefect;
                                ws.Cells[rowNumber, 16].Value = item.Remark;
                                ws.Cells[rowNumber, 17].Value = item.PolicyType;
                                #endregion
                                rowNumber++;
                            }
                        }
                    }

                    //#region 表宽度调整
                    ws.Column(1).AutoFit();
                    ws.Column(2).AutoFit();
                    ws.Column(3).AutoFit();
                    ws.Column(4).AutoFit();
                    ws.Column(5).AutoFit();
                    ws.Column(6).AutoFit();
                    ws.Column(7).AutoFit();
                    ws.Column(8).AutoFit();
                    ws.Column(9).AutoFit();
                    ws.Column(10).AutoFit();
                    ws.Column(11).AutoFit();
                    ws.Column(12).AutoFit();
                    ws.Column(13).AutoFit();
                    ws.Column(14).AutoFit();
                    ws.Column(15).AutoFit();
                    ws.Column(16).AutoFit();
                    ws.Column(17).AutoFit();
                    //#endregion
                    excel.Save();
                }

                PMSDialogService.Show("订单数据导出成功");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
