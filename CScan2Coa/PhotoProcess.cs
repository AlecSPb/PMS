using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Xceed.Words.NET;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ImportTargetPhotoIntoReport.MainService;
using System.Diagnostics;


namespace ImportTargetPhotoIntoReport
{
    class PhotoProcess
    {

        private List<string> DocxFiles;
        private List<string> JpegFiles;

        public bool IsOpenOutputDirectory { get; set; }

        public PhotoMarkerControlParameter PhotoMarkerControl { get; set; }

        public PhotoProcess()
        {
            DocxFiles = new List<string>();
            JpegFiles = new List<string>();
            targetFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            IsOpenOutputDirectory = true;
            PhotoMarkerControl = new PhotoMarkerControlParameter();
        }

        public event EventHandler<ProcessMessageEventArg> ChangeMessage;
        public event EventHandler<ProcessValueEventArg> ChangeProcess;

        public void LoadFile(string jpegFolder)
        {
            if (!Directory.Exists(jpegFolder))
            {
                TriggerMessageEvent("没有找到docx或者jpeg文件夹");
            }
            else
            {
                JpegFiles = GetJPEGFullNames(jpegFolder);
                TriggerMessageEvent("文件载入完毕");
                TriggerMessageEvent($"图片文件夹中有{JpegFiles.Count}个jpg文件");


                //在docxfolder中创建targetfolder
                targetFolder = Path.Combine(jpegFolder, "添加水印的照片");
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }
                TriggerMessageEvent($"目标文件夹创建完毕{targetFolder}");

            }
        }

        /// <summary>
        /// 加载所有的docx，jpeg文件到处理列表
        /// </summary>
        /// <param name="docxFolder"></param>
        /// <param name="jpegFolder"></param>
        public void LoadFile(string docxFolder, string jpegFolder)
        {
            if (!Directory.Exists(docxFolder) || !Directory.Exists(jpegFolder))
            {
                TriggerMessageEvent("没有找到docx或者jpeg文件夹");
            }
            else
            {
                DocxFiles = GetDocxFullNames(docxFolder);
                JpegFiles = GetJPEGFullNames(jpegFolder);
                TriggerMessageEvent("文件载入完毕");

                TriggerMessageEvent($"COA文件夹中有{DocxFiles.Count}个docx文件");

                TriggerMessageEvent($"图片文件夹中有{JpegFiles.Count}个jpg文件");


                //在docxfolder中创建targetfolder
                targetFolder = Path.Combine(docxFolder, "追加照片后的报告");
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }
                TriggerMessageEvent($"目标文件夹创建完毕{targetFolder}");

            }
        }

        private string targetFolder;

        private void TriggerMessageEvent(string msg)
        {
            ChangeMessage?.Invoke(this, new ProcessMessageEventArg() { Message = msg });
        }

        public void TriggerProgressEvent(int value)
        {
            ChangeProcess?.Invoke(this, new ProcessValueEventArg() { Progress = value });
        }

        private string error_prefix = "[Error]:";

        /// <summary>
        /// 批量处理Docx文件
        /// </summary>
        public void ProcessDocx()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int total = DocxFiles.Count;
            int count = 0;

            foreach (var docx in DocxFiles)
            {
                if (!File.Exists(docx))
                {
                    TriggerMessageEvent($"没有找到对应的{Path.GetFileNameWithoutExtension(docx)}文件");
                    continue;
                }

                //模拟演示
                //System.Threading.Thread.Sleep(500);

                string product_id = GetProductIDFromDocxName(docx).Replace('_', '-');
                if (product_id != "")
                {
                    bool hasFind = false;
                    //查找180608-AB-1-R
                    foreach (var img in JpegFiles)
                    {
                        if (img.Contains($"{product_id}-R"))
                        {
                            AppendJPGIntoDocx(docx, img, targetFolder);
                            hasFind = true;
                            break;
                        }
                    }
                    //如果没有找到，继续查找180608-AB-1
                    if (!hasFind)
                    {
                        foreach (var img in JpegFiles)
                        {
                            if (img.Contains($"{product_id}"))
                            {
                                AppendJPGIntoDocx(docx, img, targetFolder);
                                hasFind = true;
                                break;
                            }
                        }
                    }

                    if (!hasFind)
                    {
                        TriggerMessageEvent($"{error_prefix}{Path.GetFileNameWithoutExtension(docx)}没有找到对应的jpg图片");
                    }
                    count++;

                    int progress_value = count * 100 / total;
                    if (progress_value <= 100)
                    {
                        TriggerProgressEvent(progress_value);
                    }

                }
                else
                {
                    TriggerMessageEvent($"{error_prefix}{Path.GetFileNameWithoutExtension(docx)}产品ID解析有问题");
                }

            }

            sw.Stop();
            TriggerMessageEvent($"消耗时间:{sw.ElapsedMilliseconds}ms");

            if (IsOpenOutputDirectory)
            {
                System.Diagnostics.Process.Start(targetFolder);
            }
        }

        /// <summary>
        /// 批量处理图片
        /// </summary>
        public void ProcessCscanPhoto()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int total = JpegFiles.Count;
            int count = 0;

            foreach (var jpeg in JpegFiles)
            {
                if (!File.Exists(jpeg))
                {
                    TriggerMessageEvent($"没有找到对应的{Path.GetFileNameWithoutExtension(jpeg)}文件");
                    continue;
                }

                //模拟演示
                //System.Threading.Thread.Sleep(500);

                AddWaterPrintMarker(jpeg);

                count++;

                int progress_value = count * 100 / total;
                if (progress_value <= 100)
                {
                    TriggerProgressEvent(progress_value);
                }


            }
            sw.Stop();
            TriggerMessageEvent($"消耗时间:{sw.ElapsedMilliseconds}ms");
            if (IsOpenOutputDirectory)
            {
                System.Diagnostics.Process.Start(targetFolder);
            }
        }

        /// <summary>
        /// 追加图片到文档结尾并另存
        /// </summary>
        /// <param name="docxFile"></param>
        /// <param name="jpegFile"></param>
        /// <param name="targetFolder"></param>
        public void AppendJPGIntoDocx(string docxFile, string jpegFile, string targetFolder)
        {
            if (File.Exists(docxFile) && File.Exists(jpegFile))
            {
                DocX doc = DocX.Load(docxFile);

                doc.InsertSectionPageBreak();
                doc.InsertParagraph("CSCAN IMAGE", false, new Formatting() { Size = 20 }).Alignment = Alignment.center;


                Xceed.Words.NET.Image img;
                //图片追加处理
                string img_file = jpegFile;

                if (img_file == null)
                {
                    return;
                }

                img = doc.AddImage(img_file);

                var pic = img.CreatePicture();
                doc.InsertParagraph().AppendPicture(pic).Alignment = Alignment.center;




                string targetFile = Path.GetFileName(docxFile);
                string newDocxFile = Path.Combine(targetFolder, targetFile);
                doc.SaveAs(newDocxFile);
                doc.Dispose();
            }
            else
            {
                TriggerMessageEvent($"{error_prefix}docx文档和jpeg文档没有找到");
            }
        }

        /// <summary>
        /// 获取文档名列表
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public List<string> GetDocxFullNames(string folderPath)
        {
            return GetFileInFolder(folderPath, "*.docx");
        }
        /// <summary>
        /// 获取图片名列表
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public List<string> GetJPEGFullNames(string folderPath)
        {
            return GetFileInFolder(folderPath, "*.jpg");

        }
        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private List<string> GetFileInFolder(string folderPath, string condition)
        {
            if (!Directory.Exists(folderPath))
            {
                return null;
            }
            var files = Directory.GetFiles(folderPath, condition, SearchOption.TopDirectoryOnly);
            return files.ToList();
        }
        /// <summary>
        /// 从docx文件名获取id
        /// </summary>
        /// <param name="docxName"></param>
        /// <returns></returns>
        public string GetProductIDFromDocxName(string docxName)
        {
            return GetProductIDFromFileName(docxName, @"\d{6}_\w{2}_\d{1}");
        }
        /// <summary>
        /// 从jpg文件名中获取id
        /// </summary>
        /// <param name="docxName"></param>
        /// <returns></returns>
        public string GetProductIDFromJPEGName(string jpgName)
        {
            return GetProductIDFromFileName(jpgName, @"\d{6}-\w{2}-\d{1}");
        }


        private string GetProductIDFromFileName(string fileName, string pattern)
        {
            if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(pattern))
            {
                return string.Empty;
            }

            var match = Regex.Match(fileName, pattern);

            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 在图片左上角插入文件名
        /// </summary>
        /// <param name="jpgName"></param>
        /// <returns></returns>
        private void AddWaterPrintMarker(string jpgName)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(jpgName);
                Graphics g = Graphics.FromImage(img);
                System.Drawing.Font font = new System.Drawing.Font("Arial", 8);

                string file_name = Path.GetFileNameWithoutExtension(jpgName);
                string product_id = GetProductIDFromJPEGName(file_name);

                float x = 5, y = 5;
                float interval = 15;

                if (PhotoMarkerControl.HasProductID)
                {
                    g.DrawString(product_id, font, Brushes.Orange, x, y);
                }

                if (PhotoMarkerControl.HasWeldingRation || PhotoMarkerControl.HasComposition)
                {
                    DcRecordBonding bonding = null;
                    using (var service = new RecordBondingServiceClient())
                    {
                        bonding = service.GetRecordBondingByProductID(product_id).FirstOrDefault();
                    }

                    if (bonding != null)
                    {
                        if (PhotoMarkerControl.HasComposition)
                        {
                            string composition = bonding.TargetComposition;
                            y += interval;
                            g.DrawString(composition, font, Brushes.Orange, x, y);

                        }

                        if (PhotoMarkerControl.HasWeldingRation)
                        {
                            string composition = bonding.WeldingRate.ToString("F2") + "%";
                            y += interval;
                            g.DrawString(composition, font, Brushes.Orange, x, y);
                        }
                    }
                }


                //左下角添加PMI标志
                int height = img.Height;
                y = height - 25;

                g.DrawString("CSCAN@CDPMI", font, Brushes.White, x, y);



                MemoryStream ms = new MemoryStream();
                string targetFile = Path.GetFileName(jpgName);
                string newFile = Path.Combine(targetFolder, targetFile);

                img.Save(newFile);
                g.Dispose();
                img.Dispose();
            }
            catch (Exception ex)
            {
                TriggerMessageEvent($"{error_prefix}{ex.Message}");
            }
        }


    }
}
