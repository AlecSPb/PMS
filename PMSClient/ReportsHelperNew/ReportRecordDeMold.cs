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
                doc.ReplaceText("[SearchCode]", selectedDate.ToString("yyyy-MM-dd dddd") ?? "");

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

                    int row_index = 0;

                    int s = 0, t = 0;
                    while (pageIndex < pageCount)
                    {
                        s = pageIndex * pageSize;
                        t = pageSize;
                        var pageData = service.GetPlanExtra(s, t, searchCode, empty);
                        var ordered = pageData.OrderBy(i => i.Plan.PlanLot).ThenBy(i => i.Plan.SearchCode);
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

                                //添加440 加工的大靶材到记录单
                                if (item.Plan.VHPRequirement.Contains("#")
                                    && (item.Plan.PlanType.Contains("其他") ||
                                            item.Plan.PlanType.Contains("加工")
                                            ||item.Plan.PlanType.Contains("外协")))
                                {
                                    string remark = GetBigNumber(item.Plan.VHPRequirement);
                                    row.Cells[12].Paragraphs[0].Append(remark);
                                    row.Cells[12].VerticalAlignment = VerticalAlignment.Center;
                                }

                                //添加是否有样品记录
                                string sample_indicator = "";
                                if (!item.Plan.PlanType.Contains("回收"))
                                {
                                    bool needSample = Helpers.OrderHelper.NeedSample(item.Misson.SampleNeed) ||
                                    Helpers.OrderHelper.NeedSample(item.Misson.SampleForAnlysis);
                                    sample_indicator = needSample ? "[样]" : "";
                                }
                                row.Cells[13].Paragraphs[0].Append(sample_indicator).Alignment=Alignment.center;
                                row.Cells[13].VerticalAlignment = VerticalAlignment.Center;


                                //隔行着色
                                if (row_index % 2 == 0)
                                {
                                    for (int j = 0; j < 14; j++)
                                    {
                                        row.Cells[j].FillColor = System.Drawing.Color.LightGray;
                                    }
                                }
                                row_index++;
                            }
                        }

                        pageIndex++;
                    }





                }
                #endregion
                doc.Save();
            }
            File.Copy(temp, wordFileName, true);
            PMSDialogService.Show("生成成功，即将打开");
            System.Diagnostics.Process.Start(wordFileName);
        }

        /// <summary>
        /// 从备注字符串中获取#165格式的编号
        /// </summary>
        /// <param name="vhprequirement"></param>
        /// <returns></returns>
        private string GetBigNumber(string vhprequirement)
        {
            if (string.IsNullOrEmpty(vhprequirement))
                return "";
            return System.Text.RegularExpressions.Regex.Match(vhprequirement, @"#\d*").Value;
        }


    }
}
