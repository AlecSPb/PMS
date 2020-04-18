using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.OutputService;

namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputPlan: ExcelOutputBasePageByDate
    {
        public ExcelOutputPlan()
        {

        }
        public override void Output()
        {
            ResetParameters();
            using (var service = new OutputServiceClient())
            {
                recordCount = service.GetPlanByYearMonthCount(year_start, month_start, year_end, month_end);
                pageCount = GetPageCount();
                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                string[] titles = {
                                    "热压日期",
                                    "批次",
                                    "设备",
                                    "成分",
                                    "内部编号",                                    
                                    "工艺号",
                                    "后续",
                                    "计算密度",
                                    "单片重量",
                                    "多片重量",
                                    "模具类型",
                                    "模具直径",
                                    "厚度",
                                    "数量",
                                    "粉粒",
                                    "环境温度",
                                    "环境湿度",
                                    "预压温度",
                                    "预压压力",
                                    "温度",
                                    "压力",
                                    "真空",
                                    "保温时间",
                                    "制粉要求",
                                    "热压要求",
                                    "特殊要求",
                                    "装料要求",
                                    "备注",
                                    "创建时间",
                                    "创建人"
                };

                helper.AddRowTitle(titles);

                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pageData = service.GetPlanByYearMonth(s, t, year_start, month_start, year_end, month_end);


                    foreach (var item in pageData)
                    {
                        int column_index = 0;
                        helper.CreateRow(rowIndex);
                        helper.CreateAndSetCell(column_index++, item.Plan.PlanDate.ToString("yyMMdd"));
                        helper.CreateAndSetCell(column_index++, item.Plan.PlanLot);
                        helper.CreateAndSetCell(column_index++, item.Plan.VHPDeviceCode);
                        helper.CreateAndSetCell(column_index++, item.Misson.CompositionStandard);
                        helper.CreateAndSetCell(column_index++, item.Misson.PMINumber);
                        helper.CreateAndSetCell(column_index++, item.Plan.ProcessCode);
                        helper.CreateAndSetCell(column_index++, item.Plan.PlanType);
                        helper.CreateAndSetCell(column_index++, item.Plan.CalculationDensity);
                        helper.CreateAndSetCell(column_index++, item.Plan.SingleWeight);
                        helper.CreateAndSetCell(column_index++, item.Plan.AllWeight);
                        helper.CreateAndSetCell(column_index++, item.Plan.MoldType);
                        helper.CreateAndSetCell(column_index++, item.Plan.MoldDiameter);
                        helper.CreateAndSetCell(column_index++, item.Plan.Thickness);
                        helper.CreateAndSetCell(column_index++, item.Plan.Quantity);
                        helper.CreateAndSetCell(column_index++, item.Plan.GrainSize);
                        helper.CreateAndSetCell(column_index++, item.Plan.RoomTemperature);
                        helper.CreateAndSetCell(column_index++, item.Plan.RoomHumidity);
                        helper.CreateAndSetCell(column_index++, item.Plan.PreTemperature);
                        helper.CreateAndSetCell(column_index++, item.Plan.PrePressure);
                        helper.CreateAndSetCell(column_index++, item.Plan.Temperature);
                        helper.CreateAndSetCell(column_index++, item.Plan.Pressure);
                        helper.CreateAndSetCell(column_index++, item.Plan.Vaccum);
                        helper.CreateAndSetCell(column_index++, item.Plan.KeepTempTime);
                        helper.CreateAndSetCell(column_index++, item.Plan.VHPRequirement);
                        helper.CreateAndSetCell(column_index++, item.Plan.MillingRequirement);
                        helper.CreateAndSetCell(column_index++, item.Plan.SpecialRequirement);
                        helper.CreateAndSetCell(column_index++, item.Plan.FillingRequirement);
                        helper.CreateAndSetCell(column_index++, item.Plan.Remark);
                        helper.CreateAndSetCell(column_index++, item.Plan.CreateTime.ToString());
                        helper.CreateAndSetCell(column_index++, item.Plan.Creator);



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
