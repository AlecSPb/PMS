using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DocGenerator
{
    public class GeneralGenerator
    {
        public GeneralGenerator()
        {
            //设定模板文件夹
            SourceFolder = Path.Combine(Environment.CurrentDirectory, "DocTemplate");
            TargetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                DateTime.Now.ToString("yyMMdd"));
        }

        public GeneralGenerator(string targetFolder)
        {
            //设定模板文件夹
            SourceFolder = Path.Combine(Environment.CurrentDirectory, "DocTemplate");
            TargetFolder = Path.Combine(targetFolder, DateTime.Now.ToString("yyMMdd"));
        }
        public string SourceFolder { get; private set; }
        public string TargetFolder { get; set; }

        public void Generate<T>(IDoc<T> generator, T docModel, string templateName, string targetFileName)
        {
            try
            {
                string sourceFilePath = Path.Combine(SourceFolder, templateName + ".docx");
                //创建目标文件夹
                if (!Directory.Exists(TargetFolder))
                {
                    Directory.CreateDirectory(TargetFolder);
                }
                string targetFilePath = Path.Combine(TargetFolder, targetFileName + ".docx");
                generator.Generate(sourceFilePath, targetFilePath, docModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
