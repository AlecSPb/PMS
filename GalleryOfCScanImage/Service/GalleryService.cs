using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonHelper;
using GalleryOfCScanImage.MainService;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.IO;


namespace GalleryOfCScanImage.Service
{
    public class GalleryService
    {

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
                System.Threading.Thread.Sleep(1000);
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
                        .Where(i => i.TargetDimension.Contains("230") && i.State == "完成")
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
            string imagefile = Directory.GetFiles(Parameters.ImageFolder, $"{productid}.jpg").FirstOrDefault();

            return imagefile;
        }

        private void OpenFile(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    System.Diagnostics.Process.Start(filepath);
                }
            }
            catch (Exception)
            {

            }
        }


        private BasicHelper helper = new BasicHelper();
        private void CreateDocument(DcRecordBonding[] records)
        {

            string filename = helper.GetDateTimeFileName();
            string outputFolder = helper.CreateFolder("PMS临时文件夹");
            string filepath = helper.GetFullFileName(filename, outputFolder);

            try
            {

                int row = 0, column = 0;

                DocX doc = DocX.Create(filepath);
                float margin_lr = 30;
                float margin_tb = 70;
                doc.MarginLeft = margin_lr;
                doc.MarginRight = margin_lr;
                doc.MarginTop = margin_tb;
                doc.MarginBottom = margin_tb;

                doc.AddHeaders();
                doc.DifferentFirstPage = true;
                Header header = doc.Headers.First;
                var p0 = header.InsertParagraph();
                p0.Append($"CDPMI Create@{DateTime.Now.ToString()};230 mm Target Bonding Image From{Parameters.Start.ToShortDateString()}To{Parameters.End.ToShortDateString()}");

                Table table = doc.AddTable(1, 4);
                table.AutoFit = AutoFit.Window;
                int imageSize = 170;
                for (int i = 0; i < records.Count(); i++)
                {
                    System.Threading.Thread.Sleep(5);
                    string imagefile = FindImagePath(records[i].TargetProductID);
                    Cell cell = table.Rows[row].Cells[column];
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    Paragraph p = cell.Paragraphs[0];
                    if (imagefile != null)
                    {
                        p.Append(records[i].TargetProductID).FontSize(8);
                        //p.AppendLine(records[i].TargetAbbr).FontSize(6);
                        p.AppendPicture(doc.AddImage(imagefile).CreatePicture(imageSize, imageSize));
                        p.Alignment = Alignment.center;
                    }
                    else
                    {
                        p.Append($"{records[0].TargetProductID}").Alignment = Alignment.center;
                        p.Alignment = Alignment.center;
                        cell.FillColor = System.Drawing.Color.Yellow;
                    }

                    column++;
                    if (column == 4)
                    {
                        row++;
                        column = 0;
                        table.InsertRow(row);
                    }

                    //状态更新
                    if (Parameters.ShowProcessDetails)
                    {
                        UpdateMessage($"已处理{i + 1}个 {records[i].TargetProductID} {records[i].TargetComposition}");
                    }
                    int progressValue = (int)((i + 1) * 100 / records.Count());
                    UpdateProgress(progressValue);
                }

                doc.InsertTable(table);
                doc.Save();
                doc.Dispose();


                if (Parameters.OpenTheDocument)
                {
                    OpenFile(filepath);
                }
            }
            catch (Exception)
            {

            }


        }
    }
}
