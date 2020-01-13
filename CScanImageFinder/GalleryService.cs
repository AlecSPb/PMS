using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;
using CommonHelper;
using System.IO;

namespace CScanImageFinder
{
    class GalleryService
    {
        private BasicHelper helper = new BasicHelper();

        private string FindImagePath(string productid)
        {
            string imagefile = Directory.GetFiles(@"C:\Users\XS\Desktop\Bondings", $"{productid}.jpg").FirstOrDefault();

            return imagefile;
        }

        /// <summary>
        /// 创建文档
        /// </summary>
        /// <param name="productids"></param>
        public void CreateDocument(string[] productids)
        {

            string filename = helper.GetDateTimeFileName();
            string outputFolder = helper.CreateFolder(helper.GetDesktopPath(), "PMS临时文件夹");
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
                Header header = doc.Headers.first;
                var p0 = header.InsertParagraph();
                p0.Append($"CDPMI Create@{DateTime.Now.ToString()}; ");

                Table table = doc.AddTable(1, 4);
                table.AutoFit = AutoFit.Window;
                int imageSize = 170;
                for (int i = 0; i < productids.Count(); i++)
                {
                    System.Threading.Thread.Sleep(5);
                    string imagefile = FindImagePath(productids[i]);
                    Cell cell = table.Rows[row].Cells[column];
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    Paragraph p = cell.Paragraphs[0];
                    p.Append($"{productids[i]}");
                    p.FontSize(8);
                    p.Alignment = Alignment.center;

                    if (imagefile != null)
                    {
                        //p.AppendLine(records[i].TargetAbbr).FontSize(6);
                        p.AppendPicture(doc.AddImage(imagefile).CreatePicture(imageSize, imageSize));
                    }
                    else
                    {
                        cell.FillColor = System.Drawing.Color.Yellow;
                    }

                    column++;
                    if (column == 4)
                    {
                        row++;
                        column = 0;
                        table.InsertRow(row);
                    }
                }

                doc.InsertTable(table);
                doc.Save();
                doc.Dispose();

            }
            catch (Exception)
            {

            }


        }
    }
}
