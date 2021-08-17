using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.Maintainance;
using Novacode;

namespace PMSClient.ReportsHelperNew
{
    public class ReportMaintenance : ReportBase
    {
        private bool IsOpenAfterCreated;
        public ReportMaintenance()
        {
            IsOpenAfterCreated = false;
        }
        public void SetParameters(bool isOpenAfterCreated)
        {
            this.IsOpenAfterCreated = isOpenAfterCreated;
        }

        public override void Output()
        {
            string source = Path.Combine(reportsFolder, "MR.docx");
            string temp = Path.Combine(reportsFolder, "Temp", "MR.docx");
            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                Table mainTable = doc.Tables[0];

                if (mainTable != null)
                {
                    using (var mr_service = new MaintenanceServiceClient())
                    {
                        int count = mr_service.GetMaintenanceRecordsCount("", "");
                        var results = mr_service.GetMaintenanceRecords(0, count, "", "");
                        int row_number = 2;
                        foreach (var item in results.OrderByDescending(i=>i.CreateTime))
                        {
                            mainTable.InsertRow();
                            mainTable.Rows[row_number].Cells[0].Paragraphs[0].Append(item.CreateTime.ToShortDateString());
                            mainTable.Rows[row_number].Cells[1].Paragraphs[0].Append(item.VHPMachineCode??"");
                            mainTable.Rows[row_number].Cells[2].Paragraphs[0].Append(item.PlanItem??"");
                            mainTable.Rows[row_number].Cells[3].Paragraphs[0].Append(item.PlanType??"");
                            mainTable.Rows[row_number].Cells[4].Paragraphs[0].Append(item.Content??"");
                            mainTable.Rows[row_number].Cells[5].Paragraphs[0].Append(item.Persons??"");

                            row_number++;
                        }


                    }

                }

                doc.Save();
            }

            File.Copy(temp, wordFileName, true);
            //PMSDialogService.Show("生成成功，即将打开");
            if (IsOpenAfterCreated)
            {
                System.Diagnostics.Process.Start(wordFileName);
            }

        }


    }
}
