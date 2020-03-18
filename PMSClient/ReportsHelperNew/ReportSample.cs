using Novacode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using System.Drawing;
using PMSClient.Sample;

namespace PMSClient.ReportsHelperNew
{
    /// <summary>
    /// 用于样品管理
    /// </summary>
    public class ReportSample : ReportBase
    {
        public string SelectedTrackingStage { get; set; } = "";
        public override void Output()
        {
            ResetParameters();
            string source = Path.Combine(reportsFolder, "Samples.docx");
            string temp = Path.Combine(reportsFolder, "Temp", "Samples.docx");
            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                #region 字段
                string createDate = DateTime.Today.ToShortDateString();
                doc.ReplaceText("[CreateDate]", createDate ?? "");
                string postfix = SelectedTrackingStage == "" ? "全部" : SelectedTrackingStage;

                doc.ReplaceText("[SampleType]", postfix ?? "");

                Table table = doc.Tables[0];
                using (var service = new SampleServiceClient())
                {
                    recordCount = service.GetSampleAllCount(empty, empty, empty);
                    pageCount = GetPageCount();

                    int s = 0, t = 0;
                    while (pageIndex < pageCount)
                    {
                        s = pageIndex * pageSize;
                        t = pageSize;
                        var pageData = service.GetSampleAll(s, t, empty, empty, SelectedTrackingStage);
                        var ordered = pageData;
                        int row_index = 0;
                        foreach (var item in ordered)
                        {
                            row_index++;
                            Row row = table.InsertRow();
                            row.Height = 35;
                            row.Cells[0].Paragraphs[0].Append(row_index.ToString()).Alignment
                                = Alignment.center;
                            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[1].Paragraphs[0].Append(item.PMINumber ?? "");
                            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[2].Paragraphs[0].Append(item.Composition ?? "");
                            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[3].Paragraphs[0].Append(item.Customer ?? "");
                            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[4].Paragraphs[0].Append(item.OriginalRequirement ?? "");
                            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[5].Paragraphs[0].Append(item.TrackingStage ?? "").Alignment = Alignment.center;
                            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                        }

                        pageIndex++;
                    }





                }
                #endregion
                doc.Save();
            }
            File.Copy(temp, wordFileName, true);
            //PMSDialogService.Show("生成成功，即将打开");
            System.Diagnostics.Process.Start(wordFileName);
        }
    }
}
