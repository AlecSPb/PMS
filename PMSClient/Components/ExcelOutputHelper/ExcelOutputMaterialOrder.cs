using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputMaterialOrder : ExcelOutputBaseSimple
    {
        public ExcelOutputMaterialOrder(Guid orderId)
        {
            this.orderId = orderId;
        }

        private Guid orderId;
        public void SetOrderID(Guid orderId)
        {
            this.orderId = orderId;
        }

        public override void Output()
        {
            //ResetParameters();
            using (var service = new MaterialOrderServiceClient())
            {
                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                //插入标题行
                string[] titles = { "创建时间", "状态", "优先级", "材料编号",  "创建者","成分","内部编号", "纯度", "重量"
                        , "加工单价","加工总价", "描述", "提供原料","提供原料总价","提供原料总价+加工总价", "交付日期", "备注" };

                helper.AddRowTitle(titles);

                //插入数据行
                int rowIndex = 1;

                var pagedData = service.GetMaterialOrderItembyMaterialID(orderId);
                foreach (var item in pagedData)
                {
                    helper.CreateRow(rowIndex);
                    int columnIndex = 0;

                    helper.CreateAndSetCell(columnIndex++, item.CreateTime.ToString());
                    helper.CreateAndSetCell(columnIndex++, item.State);
                    helper.CreateAndSetCell(columnIndex++, item.Priority);
                    helper.CreateAndSetCell(columnIndex++, item.OrderItemNumber);
                    helper.CreateAndSetCell(columnIndex++, item.Creator);
                    helper.CreateAndSetCell(columnIndex++, item.Composition);
                    helper.CreateAndSetCell(columnIndex++, item.PMINumber);
                    helper.CreateAndSetCell(columnIndex++, item.Purity);
                    helper.CreateAndSetCell(columnIndex++, item.Weight);
                    helper.CreateAndSetCell(columnIndex++, item.UnitPrice);
                    helper.CreateAndSetCell(columnIndex++, item.UnitPrice * item.Weight);
                    helper.CreateAndSetCell(columnIndex++, item.Description);
                    helper.CreateAndSetCell(columnIndex++, item.ProvideRawMaterial);
                    helper.CreateAndSetCell(columnIndex++, item.MaterialPrice);
                    helper.CreateAndSetCell(columnIndex++, item.UnitPrice * item.Weight + item.MaterialPrice);
                    helper.CreateAndSetCell(columnIndex++, item.DeliveryDate.ToString());
                    helper.CreateAndSetCell(columnIndex++, item.SJIngredient);

                    rowIndex++;
                }

                helper.Save(excelFileName);

                PMSDialogService.Show($"{excelFileName}创建完毕,请到桌面查看");
                CheckOpenAfterCreate();

            }
        }
    }
}
