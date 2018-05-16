using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputMaterialOrderItemList : ExcelOutputBase
    {
        public ExcelOutputMaterialOrderItemList()
        {

        }
        public override void Output()
        {
            ResetParameters();
            using (var service = new MaterialOrderServiceClient())
            {
                recordCount = service.GetMaterialOrderItemExtrasCount(empty, empty, empty, empty);
                pageCount = GetPageCount();

                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                //插入标题行
                string[] titles = { "创建时间", "状态", "优先级", "材料编号",  "创建者","成分","内部编号", "纯度", "重量",
                           "供应商","订单PO", "交付日期", "原料", "单价", "描述", "提供原料" };

                helper.AddRowTitle(titles);

                //插入数据行
                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pagedData = service.GetMaterialOrderItemExtras(s, t, empty, empty, empty, empty);
                    foreach (var item in pagedData)
                    {
                        helper.CreateRow(rowIndex);
                        helper.CreateAndSetCell(0, item.MaterialOrderItem.CreateTime.ToString());
                        helper.CreateAndSetCell(1, item.MaterialOrderItem.State);
                        helper.CreateAndSetCell(2, item.MaterialOrderItem.Priority);
                        helper.CreateAndSetCell(3, item.MaterialOrderItem.OrderItemNumber);
                        helper.CreateAndSetCell(4, item.MaterialOrderItem.Creator);
                        helper.CreateAndSetCell(5, item.MaterialOrderItem.Composition);
                        helper.CreateAndSetCell(6, item.MaterialOrderItem.PMINumber);
                        helper.CreateAndSetCell(7, item.MaterialOrderItem.Purity);
                        helper.CreateAndSetCell(8, item.MaterialOrderItem.Weight.ToString());
                        helper.CreateAndSetCell(9, item.MaterialOrder.Supplier);
                        helper.CreateAndSetCell(10, item.MaterialOrder.OrderPO);
                        helper.CreateAndSetCell(11, item.MaterialOrderItem.DeliveryDate.ToString());
                        helper.CreateAndSetCell(12, item.MaterialOrderItem.SJIngredient);
                        helper.CreateAndSetCell(13, item.MaterialOrderItem.UnitPrice.ToString());
                        helper.CreateAndSetCell(14, item.MaterialOrderItem.Description);
                        helper.CreateAndSetCell(15, item.MaterialOrderItem.ProvideRawMaterial);

                        rowIndex++;
                    }
                    pageIndex++;
                }

                helper.Save(excelFileName);

                PMSDialogService.ShowYes($"{excelFileName}创建完毕,请到桌面查看");
            }
        }
    }
}
