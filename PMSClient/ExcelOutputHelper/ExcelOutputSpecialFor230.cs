using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using PMSClient.OutputService;


namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputSpecialFor230 : ExcelOutputBase
    {

        public ExcelOutputSpecialFor230()
        {

        }

        public override void Output()
        {
            ResetParameters();

            using (var service = new OutputServiceClient())
            {
                recordCount = service.GetAll230DataCount();
                pageCount = GetPageCount();

                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                //插入标题行

                string[] titles = {"Product ID"};
                helper.AddRowTitle(titles);

                //插入数据行
                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex<pageCount)
                {
                    System.Diagnostics.Debug.Write($"{pageIndex} ");
                    s = pageIndex * pageSize;
                    t = pageSize;

                    var pageData = service.GetAll230Data(s, t);
                    foreach (var item in pageData)
                    {
                        helper.CreateRow(rowIndex);
                        #region 写入数据行
                        //对XRF成分进行处理
                        helper.CreateAndSetCell(0, item.Delivery.ProductID);






                        #endregion

                        rowIndex++;
                    }
                    pageIndex++;

                }



                helper.Save(excelFileName);
                PMSDialogService.Show($"{excelFileName}创建完毕,请到桌面查看");

            }
        }




    }
}
