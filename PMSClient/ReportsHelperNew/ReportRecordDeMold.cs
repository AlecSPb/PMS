using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using Novacode;
using System.IO;

namespace PMSClient.ReportsHelperNew
{
    public class ReportRecordDeMold : ReportBase
    {
        public ReportRecordDeMold()
        {

        }

        public override void Output()
        {
            ResetParameters();
            var tool = new ToolWindow.DateSelector();
            if (tool.ShowDialog() == false)
                return;
            DateTime selectedDate = tool.SelectedDate;
            string searchCode = selectedDate.ToString("yyMMdd");

            string source = Path.Combine(reportsFolder, "ReportRecordDeMold.docx");
            string temp = Path.Combine(reportsFolder, "Temp", "ReportRecordDeMold.docx");
            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                #region 字段
                string createDate = DateTime.Today.ToShortDateString();
                doc.ReplaceText("[CreateDate]", createDate ?? "");
                doc.ReplaceText("[SearchCode]", searchCode ?? "");

                Table table = doc.Tables[0];
                using (var service = new MissonServiceClient())
                {
                    recordCount = service.GetPlanExtraCount(searchCode, empty);
                    if (recordCount == 0)
                    {
                        PMSDialogService.Show("找个0个计划，请确定日期选择是否正确");
                        return;
                    }

                    pageCount = GetPageCount();

                    int s = 0, t = 0;
                    while (pageIndex < pageCount)
                    {
                        s = pageIndex * pageSize;
                        t = pageSize;
                        var pageData = service.GetPlanExtra(s, t, searchCode, empty);
                        var ordered = pageData.OrderBy(i => i.Plan.PlanLot).OrderBy(i => i.Plan.SearchCode);
                        foreach (var item in ordered)
                        {
                            for (int i = 1; i < item.Plan.Quantity + 1; i++)
                            {
                                Row row = table.InsertRow();
                                row.Height = 35;
                                string row_1 = item.Plan.PlanDate.ToString("yyMMdd") + "-" + item.Plan.VHPDeviceCode + "-" + i;
                                row.Cells[0].Paragraphs[0].Append(row_1);
                                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                                row.Cells[1].Paragraphs[0].Append(item.Misson.CompositionStandard);
                                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                                row.Cells[2].Paragraphs[0].Append(item.Plan.PlanType);
                                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                                row.Cells[3].Paragraphs[0].Append(item.Plan.MoldDiameter.ToString());
                                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                            }
                        }

                        pageIndex++;
                    }





                }
                #endregion
                doc.Save();
            }
            File.Copy(temp, wordFileName,true);
            PMSDialogService.Show("生成成功，即将打开");
            System.Diagnostics.Process.Start(wordFileName);
        }




    }
}
