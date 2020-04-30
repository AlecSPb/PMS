using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.SampleService;

namespace PMSClient.ExcelOutputHelper
{
    /// <summary>
    /// 用来导出样品记录
    /// </summary>
    public class ExcelSample : ExcelOutputBasePage
    {
        public ExcelSample()
        {

        }

        public override void Output()
        {
            ResetParameters();
            using (var service = new SampleServiceClient())
            {
                recordCount = service.GetSampleAllCount(empty,empty, empty, empty);
                pageCount = GetPageCount();
                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                string[] titles = {
                                    "样品ID",
                                    "产品ID",
                                    "成分",
                                    "客户",
                                    "PO",
                                    "PMINumber",
                                    "更多信息",
                                    "原始要求",
                                    "追踪信息",
                                    "追踪状态",
                                    "重量",
                                    "数量",
                                    "样品类型",
                                    "样品目的",
                                    "ICPOES",
                                    "GDMS",
                                    "IGA",
                                    "热学性质",
                                    "电学性质",
                                    "其他测试结果",
                                    "备注",
                                    "创建人",
                                    "创建时间",
                                    "状态"
                };

                helper.AddRowTitle(titles);

                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pageData = service.GetSampleAll(s, t,empty, empty, empty, empty);


                    foreach (var item in pageData)
                    {
                        int column_index = 0;
                        helper.CreateRow(rowIndex);
                        helper.CreateAndSetCell(column_index++, item.SampleID ?? "");
                        helper.CreateAndSetCell(column_index++, item.ProductID ?? "");
                        helper.CreateAndSetCell(column_index++, item.Composition ?? "");
                        helper.CreateAndSetCell(column_index++, item.Customer ?? "");
                        helper.CreateAndSetCell(column_index++, item.PO ?? "");
                        helper.CreateAndSetCell(column_index++, item.PMINumber ?? "");
                        helper.CreateAndSetCell(column_index++, item.MoreInformation ?? "");
                        helper.CreateAndSetCell(column_index++, item.OriginalRequirement ?? "");
                        helper.CreateAndSetCell(column_index++, item.TraceInformation ?? "");
                        helper.CreateAndSetCell(column_index++, item.TrackingStage ?? "");
                        helper.CreateAndSetCell(column_index++, item.Weight ?? "");
                        helper.CreateAndSetCell(column_index++, item.Quantity.ToString());
                        helper.CreateAndSetCell(column_index++, item.SampleType ?? "");
                        helper.CreateAndSetCell(column_index++, item.SampleFor ?? "");
                        helper.CreateAndSetCell(column_index++, item.ICPOES ?? "");
                        helper.CreateAndSetCell(column_index++, item.GDMS ?? "");
                        helper.CreateAndSetCell(column_index++, item.IGA ?? "");
                        helper.CreateAndSetCell(column_index++, item.Thermal ?? "");
                        helper.CreateAndSetCell(column_index++, item.Permittivity ?? "");
                        helper.CreateAndSetCell(column_index++, item.OtherTestResult ?? "");
                        helper.CreateAndSetCell(column_index++, item.Remark ?? "");
                        helper.CreateAndSetCell(column_index++, item.Creator ?? "");
                        helper.CreateAndSetCell(column_index++, item.CreateTime.ToString());
                        helper.CreateAndSetCell(column_index++, item.State ?? "");


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
