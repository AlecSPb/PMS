using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalleryOfCScanImage.MainService;

namespace GalleryOfCScanImage.Service
{
    public class GalleryService
    {

        private string filename;

        public GalleryService()
        {
            Parameters = new ProcessParameter();
        }
        public event EventHandler<string> UpdateStatus;
        public event EventHandler<int> UpdateProgressValue;

        public ProcessParameter Parameters { get; set; }



        private void UpdateMessage(string s)
        {
            UpdateStatus?.Invoke(this, s);
        }

        private void UpdateProgress(int i)
        {
            UpdateProgressValue?.Invoke(this, i);
        }

        public void Process()
        {
            var records = GetBondings(Parameters.Start, Parameters.End);
            UpdateMessage($"共计{records.Count()}个数据要处理");
            if (records.Count() > 0)
            {
                UpdateMessage($"开始处理，耐心等待");
                System.Threading.Thread.Sleep(2000);
                CreateDocument(records);

                UpdateMessage($"处理完毕");
            }
        }

        private DcRecordBonding[] GetBondings(DateTime start, DateTime end)
        {
            DcRecordBonding[] bondings = null;
            try
            {
                using (var s = new RecordBondingServiceClient())
                {
                    bondings = s.GetRecordBondingsByDateTime(start, end)
                        .Where(i => i.TargetDimension.Contains("230"))
                        .ToArray();
                }
            }
            catch (Exception)
            {
            }
            return bondings;
        }

        private string FindImagePath(string productid)
        {
            return "";
        }

        private void CreateDocument(DcRecordBonding[] records)
        {
            for (int i = 0; i < records.Count(); i++)
            {
                System.Threading.Thread.Sleep(50);







                if (Parameters.ShowProcessDetails)
                {
                    UpdateMessage($"已处理{i + 1}个 {records[i].TargetProductID} {records[i].TargetComposition}");
                }
                int progressValue = (int)((i + 1) * 100 / records.Count());
                UpdateProgress(progressValue);
            }
        }
    }
}
