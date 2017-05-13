using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using PMSClient.MainService;


namespace PMSClient.ReportsHelper
{
    public class ExcelOrder : ReportBase
    {
        private string prefix = "COA";
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
                    using (var service = new OrderServiceClient())
                    {
                        #region 表头
                        ws.Cells[0, 0].Value = "订单日期";
                        ws.Cells[0, 1].Value = "客户名称";
                        ws.Cells[0, 2].Value = "PO";
                        ws.Cells[0, 3].Value = "内部编号";
                        ws.Cells[0, 4].Value = "标准成分";
                        ws.Cells[0, 5].Value = "缩写";
                        ws.Cells[0, 6].Value = "产品类型";
                        ws.Cells[0, 7].Value = "纯度";
                        ws.Cells[0, 8].Value = "数量";
                        ws.Cells[0, 9].Value = "单位";
                        ws.Cells[0, 10].Value = "单位";
                        ws.Cells[0, 11].Value = "尺寸";
                        ws.Cells[0, 12].Value = "尺寸细节";
                        ws.Cells[0, 13].Value = "样品需求";
                        ws.Cells[0, 14].Value = "交付期限";
                        ws.Cells[0, 15].Value = "最低要求";
                        ws.Cells[0, 16].Value = "备注信息";
                        ws.Cells[0, 17].Value = "策略";

                        #endregion
                        int recordCount = 0;
                        int pageSize = 10;
                        int pageIndex = 1;
                        int skip = 0;
                        int take = pageSize;
                        recordCount = service.GetOrderCountByYear(Year);
                        int rowNumber = 1;
                        for (pageIndex = 1; pageIndex <= recordCount / pageSize + 1; pageIndex++)
                        {
                            skip = (pageIndex - 1) * pageSize;
                            var results = service.GetOrderByYear(skip, take, Year);
                            foreach (var item in results)
                            {
                                #region 表数据
                                ws.Cells[rowNumber, 0].Value = item.CreateTime.Date;
                                ws.Cells[rowNumber, 1].Value = item.CustomerName;
                                ws.Cells[rowNumber, 2].Value = item.PO;
                                ws.Cells[rowNumber, 3].Value = item.PMINumber;
                                ws.Cells[rowNumber, 4].Value = item.CompositionStandard;
                                ws.Cells[rowNumber, 5].Value = item.CompositionAbbr;
                                #endregion
                                rowNumber++;
                            }
                            pageIndex++;
                        }
                        #region 表宽度调整
                        ws.Column(0).AutoFit();
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
                        #endregion
                    }
                    excel.Save();
                }

                PMSDialogService.ShowYes("订单数据导出成功");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
