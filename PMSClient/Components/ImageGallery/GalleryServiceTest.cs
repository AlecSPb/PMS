using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;
using PMSClient.Components.CscanImageProcess;
using PMSClient.OutputService;
using XSHelper;
using System.IO;


namespace PMSClient.Components.ImageGallery
{
    /// <summary>
    /// 将超声照片汇集成图集
    /// </summary>
    public class GalleryServiceTest
    {
        public GalleryServiceTest()
        {
            tests = new List<DcRecordTest>();
        }
        private List<DcRecordTest> tests;

        private int year_start;
        private int month_start;
        private int year_end;
        private int month_end;
        public void SetParameters(int y1, int m1, int y2, int m2)
        {
            year_start = y1;
            month_start = m1;
            year_end = y2;
            month_end = m2;
        }
        public void Output()
        {
            SetTargetList(year_start, month_start, year_end, month_end);
            CreateGallery();
        }

        public event EventHandler<double> UpdateProgress;
        protected void OnUpdateProgress(double progressValue)
        {
            UpdateProgress?.Invoke(this, progressValue);
        }

        public event EventHandler UpdateButtonEnable;
        protected void OnUpdateButtonEnable()
        {
            UpdateButtonEnable?.Invoke(this, null);
        }
        /// <summary>
        /// 设置好列表
        /// </summary>
        private void SetTargetList(int year_start, int month_start, int year_end, int month_end)
        {
            using (var s = new OutputServiceClient())
            {
                int count = s.GetRecordTestCountByYearMonth(year_start, month_start, year_end, month_end);
                var result = s.GetRecordTestByYearMonth(0, count, year_start, month_start, year_end, month_end);
                tests.Clear();
                tests.AddRange(result.ToList());
            }
        }

        private ImageManager imageManager = new ImageManager();
        private void CreateGallery()
        {
            var targets = tests.Where(i => !i.Dimension.Contains("230") && !i.ProductID.Contains("OS"))
                                       .OrderByDescending(i => i.ProductID).ToList();

            #region 创建word文档
            string filename = $"Target(no 230) Gallery from {year_start}-{month_start}to {year_end}-{month_end}.docx";
            string outputFolder = XS.File.GetDesktopPath();
            string filepath = Path.Combine(outputFolder, filename);

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
                Header header = doc.Headers.first;
                var p0 = header.InsertParagraph();
                p0.Append($"CDPMI Create@{DateTime.Now.ToString()}; 230 mm Target Bonding Image From {year_start}-{month_start} To {year_end}-{month_end}");

                Table table = doc.AddTable(1, 4);
                table.AutoFit = AutoFit.Window;
                int imageSize = 170;
                for (int i = 0; i < targets.Count(); i++)
                {
                    Cell cell = table.Rows[row].Cells[column];
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    Paragraph p = cell.Paragraphs[0];
                    p.Append($"{targets[i].ProductID}").FontSize(8);
                    p.AppendLine($"[{targets[i].Composition}]").FontSize(6);
                    p.Alignment = Alignment.center;

                    //下载图片
                    var imageResult = imageManager.GetImage(targets[i].ProductID, ImageType.Target);
                    if (imageResult.IsFound)
                    {
                        string imagefile = imageResult.ImagePath;
                        //p.AppendLine(targets[i].TargetAbbr).FontSize(6);
                        p.AppendPicture(doc.AddImage(imagefile).CreatePicture(imageSize, imageSize));
                    }
                    else
                    {
                        cell.FillColor = System.Drawing.Color.Yellow;
                        //p.AppendLine($"{targets[i].Remark}");
                    }

                    column++;
                    if (column == 4)
                    {
                        row++;
                        column = 0;
                        table.InsertRow(row);
                    }

                    //状态更新
                    int progressValue = (int)((i + 1) * 100 / targets.Count());
                    OnUpdateProgress(progressValue);
                }

                doc.InsertTable(table);
                doc.Save();
                doc.Dispose();

                System.Diagnostics.Process.Start(filepath);
                OnUpdateButtonEnable();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
            #endregion
        }

    }
}
