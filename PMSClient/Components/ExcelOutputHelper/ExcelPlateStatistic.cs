using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ExcelOutputHelper
{
    /// <summary>
    /// 导出背板使用信息
    /// </summary>
    public class ExcelPlateStatistic : ExcelOutputBasePage
    {
        public ExcelPlateStatistic()
        {

        }

        public override void Output()
        {
            using (var service = new RecordBondingServiceClient())
            {
                recordCount = service.GetPlateUsedStatisticsCount();
                pageCount = GetPageCount();

                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                //插入标题行
                string[] titles = { "背板编号", "已使用次数" };

                helper.AddRowTitle(titles);


                //插入数据行
                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pagedData = service.GetPlateUsedStatistics(s, t);
                    foreach (var item in pagedData)
                    {
                        helper.CreateRow(rowIndex);
                        helper.CreateAndSetCell(0, item.PlateLot ?? "");
                        helper.CreateAndSetCell(1, item.Count);

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