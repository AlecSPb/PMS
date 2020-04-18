using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.OutputService;


namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputOrder: ExcelOutputBasePageByDate
    {
        public ExcelOutputOrder()
        {

        }
        public override void Output()
        {
            ResetParameters();
            using (var service = new OutputServiceClient())
            {
                recordCount = service.GetOrderByYearMonthCount(year_start, month_start, year_end, month_end);
                pageCount = GetPageCount();
                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                string[] titles = {
                                    "客户名称",
                                    "PO",
                                    "标准成分",
                                    "原始成分",
                                    "产品类型",
                                    "纯度",
                                    "数量",
                                    "单位",
                                    "尺寸",
                                    "尺寸细节",
                                    "客户样品",
                                    "最后期限",
                                    "最低要求",
                                    "备注",
                                    "优先级",
                                    "状态",
                                    "创建时间",
                                    "创建人",
                                    "策略类型",
                                    "内部编号",
                                    "成分缩写",
                                    "图纸",
                                    "自分析样品",
                                    "运到",
                                    "背板",
                                    "特殊要求",
                                    "PartNumber",
                                    "二次加工尺寸",
                                    "二次加工细节",
                                    "最后更新时间",
                                    "背板图纸"
                };

                helper.AddRowTitle(titles);

                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pageData = service.GetOrderByYearMonth(s, t, year_start, month_start, year_end, month_end);


                    foreach (var item in pageData)
                    {
                        int column_index = 0;
                        helper.CreateRow(rowIndex);
                        helper.CreateAndSetCell(column_index++, item.CustomerName ?? "");
                        helper.CreateAndSetCell(column_index++, item.PO ?? "");
                        helper.CreateAndSetCell(column_index++, item.CompositionStandard ?? "");
                        helper.CreateAndSetCell(column_index++, item.CompositionOriginal ?? "");
                        helper.CreateAndSetCell(column_index++, item.ProductType ?? "");
                        helper.CreateAndSetCell(column_index++, item.Purity ?? "");
                        helper.CreateAndSetCell(column_index++, item.Quantity);
                        helper.CreateAndSetCell(column_index++, item.QuantityUnit ?? "");
                        helper.CreateAndSetCell(column_index++, item.Dimension ?? "");
                        helper.CreateAndSetCell(column_index++, item.DimensionDetails ?? "");
                        helper.CreateAndSetCell(column_index++, item.SampleNeed ?? "");
                        helper.CreateAndSetCell(column_index++, item.DeadLine.ToShortDateString());
                        helper.CreateAndSetCell(column_index++, item.MinimumAcceptDefect ?? "");
                        helper.CreateAndSetCell(column_index++, item.Remark ?? "");
                        helper.CreateAndSetCell(column_index++, item.Priority ?? "");
                        helper.CreateAndSetCell(column_index++, item.State ?? "");
                        helper.CreateAndSetCell(column_index++, item.CreateTime.ToString());
                        helper.CreateAndSetCell(column_index++, item.Creator ?? "");
                        helper.CreateAndSetCell(column_index++, item.PolicyType ?? "");
                        helper.CreateAndSetCell(column_index++, item.PMINumber ?? "");
                        helper.CreateAndSetCell(column_index++, item.CompositionAbbr ?? "");
                        helper.CreateAndSetCell(column_index++, item.Drawing ?? "");
                        helper.CreateAndSetCell(column_index++, item.SampleForAnlysis ?? "");
                        helper.CreateAndSetCell(column_index++, item.ShipTo ?? "");
                        helper.CreateAndSetCell(column_index++, item.WithBackingPlate ?? "");
                        helper.CreateAndSetCell(column_index++, item.SpecialRequirement ?? "");
                        helper.CreateAndSetCell(column_index++, item.PartNumber ?? "");
                        helper.CreateAndSetCell(column_index++, item.SecondMachineDimension ?? "");
                        helper.CreateAndSetCell(column_index++, item.SecondMachineDetails ?? "");
                        helper.CreateAndSetCell(column_index++, item.LastUpdateTime.ToString());
                        helper.CreateAndSetCell(column_index++, item.PlateDrawing ?? "");


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
