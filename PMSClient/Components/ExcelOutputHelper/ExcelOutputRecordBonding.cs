using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputRecordBonding : ExcelOutputBasePage
    {
        public ExcelOutputRecordBonding()
        {

        }

        public override void Output()
        {
            ResetParameters();
            using (var service = new RecordBondingServiceClient())
            {
                recordCount = service.GetRecordBondingCount(empty, empty);
                pageCount = GetPageCount();

                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                //插入标题行
                string[] titles = {"状态", "绑定批次", "靶材ID", "靶材成分", "缩写","内部编号",
                           "订单批号","靶材尺寸","背板批号","背板类型","焊合率", "备注","创建时间", "创建者" };

                helper.AddRowTitle(titles);

                //插入数据行
                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    s = pageIndex * pageSize;
                    t = pageSize;
                    var pagedData = service.GetRecordBondings(s, t, empty, empty);
                    foreach (var item in pagedData)
                    {
                        helper.CreateRow(rowIndex);

                        helper.CreateAndSetCell(0, item.State);
                        helper.CreateAndSetCell(1, item.PlanBatchNumber.ToString());
                        helper.CreateAndSetCell(2, item.TargetProductID);
                        helper.CreateAndSetCell(3, item.TargetComposition);
                        helper.CreateAndSetCell(4, item.TargetAbbr);
                        helper.CreateAndSetCell(5, item.TargetPMINumber);

                        helper.CreateAndSetCell(6, item.TargetPO);
                        helper.CreateAndSetCell(7, item.TargetDimension);
                        helper.CreateAndSetCell(8, item.PlateLot);
                        helper.CreateAndSetCell(9, item.PlateType);

                        helper.CreateAndSetCell(10, item.WeldingRate.ToString());

                        helper.CreateAndSetCell(11, item.Remark);
                        helper.CreateAndSetCell(12, item.CreateTime.ToString());
                        helper.CreateAndSetCell(13, item.Creator);

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
