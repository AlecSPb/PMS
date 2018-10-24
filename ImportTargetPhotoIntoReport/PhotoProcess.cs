using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Xceed.Words.NET;


namespace ImportTargetPhotoIntoReport
{
    class PhotoProcess
    {

        public void Analysis()
        {
            //get id from docx

            //search jpg from jpgs
            //insert into this docx

            //next

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

                Image img = doc.AddImage(jpegFile);

                var pic = img.CreatePicture();
                doc.InsertParagraph().AppendPicture(pic).Alignment = Alignment.center;




                string targetFile = Path.GetFileName(docxFile);
                string newDocxFile = Path.Combine(targetFolder, targetFile);
                doc.SaveAs(newDocxFile);
            }
            else
            {
                throw new FileNotFoundException("docx文档和jpeg文档没有找到");
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
            return GetFileInFolder(folderPath, "*.jpg|*.jpeg");

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





    }
}
