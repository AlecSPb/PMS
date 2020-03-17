using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputMaterialOut : ExcelOutputBase
    {
        public ExcelOutputMaterialOut()
        {

        }

        public override void Output()
        {
            ResetParameters();
            using (var service = new MaterialInventoryServiceClient())
            {
                recordCount = service.GetMaterialInventoryOutCountBySearch(empty, empty, empty, empty);
                pageCount = GetPageCount();

                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                //插入标题行
                string[] titles = { "创建时间", "材料批号", "成分","内部编号", "纯度", "接受者",
                           "订购重量","实际重量", "备注", "创建者" };

                helper.AddRowTitle(titles);

                //插入数据行
                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pagedData = service.GetMaterialInventoryOutsBySearch(s, t, empty, empty, empty, empty);
                    foreach (var item in pagedData)
                    {
                        helper.CreateRow(rowIndex);
                        helper.CreateAndSetCell(0, item.CreateTime.ToString());
                        helper.CreateAndSetCell(1, item.MaterialLot);
                        helper.CreateAndSetCell(2, item.Composition);
                        helper.CreateAndSetCell(3, item.PMINumber);
                        helper.CreateAndSetCell(4, item.Purity);
                        helper.CreateAndSetCell(5, item.Receiver);
                        helper.CreateAndSetCell(6, item.Weight.ToString());
                        helper.CreateAndSetCell(7, item.ActualWeight.ToString());
                        helper.CreateAndSetCell(8, item.Remark);
                        helper.CreateAndSetCell(9, item.Creator);

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
