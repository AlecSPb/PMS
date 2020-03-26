using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputDeliveryItemList : ExcelOutputBasePage
    {
        public ExcelOutputDeliveryItemList()
        {

        }
        public override void Output()
        {
            ResetParameters();
            using (var service = new DeliveryServiceClient())
            {
                recordCount = service.GetDeliveryItemExtraCount(empty, empty, empty);
                pageCount = GetPageCount();
                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                string[] titles = { "产品ID", "产品类型", "成分", "缩写", "客户", "PO", "规格", "重量", "发货时间", "发货名", "创建时间", "发货地区", "包装类型", "创建者" };

                helper.AddRowTitle(titles);

                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pageData = service.GetDeliveryItemExtra(s, t, empty, empty, empty);

                    foreach (var item in pageData)
                    {
                        helper.CreateRow(rowIndex);
                        helper.CreateAndSetCell(0, item.DeliveryItem.ProductID);
                        helper.CreateAndSetCell(1, item.DeliveryItem.ProductType);
                        helper.CreateAndSetCell(2, item.DeliveryItem.Composition);
                        helper.CreateAndSetCell(3, item.DeliveryItem.Abbr);
                        helper.CreateAndSetCell(4, item.DeliveryItem.Customer);
                        helper.CreateAndSetCell(5, item.DeliveryItem.PO);
                        helper.CreateAndSetCell(6, item.DeliveryItem.Dimension);
                        helper.CreateAndSetCell(7, item.DeliveryItem.Weight);
                        helper.CreateAndSetCell(8, item.Delivery.ShipTime.ToShortDateString());
                        helper.CreateAndSetCell(9, item.Delivery.DeliveryName);
                        helper.CreateAndSetCell(10, item.DeliveryItem.CreateTime.ToString());
                        helper.CreateAndSetCell(11, item.Delivery.Country);
                        helper.CreateAndSetCell(12, item.Delivery.PackageType);
                        helper.CreateAndSetCell(13, item.DeliveryItem.Creator);


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
