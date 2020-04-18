using PMSClient.OutputService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputPlanTrace : ExcelOutputBasePageByDate
    {
        public ExcelOutputPlanTrace()
        {

        }
        public override void Output()
        {
            ResetParameters();
            using (var service = new OutputServiceClient())
            {
                recordCount = service.GetPlanTraceCount(year_start, month_start, year_end, month_end);
                pageCount = GetPageCount();
                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                string[] titles = {
                                    "热压日期",
                                    "计划类型",
                                    "设备",
                                    "成分",
                                    "模具内径",
                                    "计划数量",
                                    "客户",
                                    "内部编号",
                                    "尺寸",
                                    "订单数量",
                                    "取模记录",
                                    "加工记录",
                                    "测试记录",
                                    "绑定记录",
                                    "发货记录",
                                    "报废记录"
                };

                helper.AddRowTitle(titles);

                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pageData = service.GetPlanTrace(s, t, year_start, month_start, year_end, month_end);


                    foreach (var item in pageData)
                    {
                        int column_index = 0;
                        helper.CreateRow(rowIndex);
                        helper.CreateAndSetCell(column_index++, item.PlanDate.ToString("yyMMdd"));
                        helper.CreateAndSetCell(column_index++, item.PlanType ?? "");
                        helper.CreateAndSetCell(column_index++, item.VHPDeviceCode ?? "");
                        helper.CreateAndSetCell(column_index++, item.CompositionStd ?? "");
                        helper.CreateAndSetCell(column_index++, item.MoldDiameter);
                        helper.CreateAndSetCell(column_index++, item.Quantity);

                        helper.CreateAndSetCell(column_index++, item.Customer ?? "");
                        helper.CreateAndSetCell(column_index++, item.PMINumber ?? "");
                        helper.CreateAndSetCell(column_index++, item.Dimension ?? "");
                        helper.CreateAndSetCell(column_index++, item.OrderQuantity);

                        helper.CreateAndSetCell(column_index++, item.RecordDeMold ?? "");
                        helper.CreateAndSetCell(column_index++, item.RecordMachine ?? "");
                        helper.CreateAndSetCell(column_index++, item.RecordTest ?? "");
                        helper.CreateAndSetCell(column_index++, item.RecordBonding ?? "");
                        helper.CreateAndSetCell(column_index++, item.RecordDelivery ?? "");
                        helper.CreateAndSetCell(column_index++, item.RecordFailure ?? "");

                        rowIndex++;
                    }
                    pageIndex++;
                }
                helper.Save(excelFileName);
                PMSDialogService.Show($"{excelFileName}创建完毕,请到桌面查看");

                CheckOpenAfterCreate();

            }
        }
    }
}
