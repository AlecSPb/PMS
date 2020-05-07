using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.OutputService;

namespace PMSClient.ExcelOutputHelper
{
    /// <summary>
    /// 用来输出原料订单项目
    /// </summary>
    public class ExcelOutputMaterialOrderItemList : ExcelOutputBasePageByDate
    {
        public ExcelOutputMaterialOrderItemList()
        {

        }
        public override void Output()
        {
            //ResetParameters();
            using (var service = new OutputServiceClient())
            {
                recordCount = service.GetMaterialOrderItemsByYearMonthCount(year_start, month_start, year_end, month_end);
                pageCount = GetPageCount();

                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                //插入标题行
                string[] titles = { "状态", "创建时间", "创建者", "类别", "品名", "规格", "细节", "数量", "单位", "级别", "存储位置", "负责人", "最大预警量", "最小预警量", "出入历史", "盘点历史", "最后更新时间", "备注" };

                helper.AddRowTitle(titles);

                //插入数据行
                int rowIndex = 1;
                int columnIndex = 0;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pagedData = service.GetMaterialOrderItemsByYearMonth(s, t, year_start, month_start, year_end, month_end);
                    foreach (var item in pagedData)
                    {
                        columnIndex = 0;
                        helper.CreateRow(rowIndex);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.CreateTime.ToString());
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.State);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.Priority);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.OrderItemNumber);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.Creator);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.Composition);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.PMINumber);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.Purity);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrder.Supplier);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrder.OrderPO);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.DeliveryDate.ToString("yyyy-MM-dd"));
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.Weight);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.UnitPrice, "¥#,##0");
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.UnitPrice * item.MaterialOrderItem.Weight, "¥#,##0");
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.Description);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.ProvideRawMaterial);
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.MaterialPrice, "¥#,##0");
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.UnitPrice * item.MaterialOrderItem.Weight + 
                            item.MaterialOrderItem.MaterialPrice, "¥#,##0");
                        helper.CreateAndSetCell(columnIndex++, item.MaterialOrderItem.SJIngredient);
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
