using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CommonHelper;
using PMSXMLCreator_Micron.Model;
using PMSXMLCreator_Micron.Service;

namespace PMSXMLCreator_Micron.Service
{
    public class XmlHelper
    {
        public void CreateECOA(Micon_COA model)
        {
            if (model == null)
            {
                return;
            }

            string folder = XSHelper.FileHelper.GetCurrentFolderPath("OutputFile");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string filerawname = $"{model.COANumber}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            string filename = $"{filerawname}.xml";
            string filePath = Path.Combine(folder, filename.Replace("#", "-"));
            FileInfo file = new FileInfo(filePath);

            //使用xmlwriter
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding(false);
            settings.NewLineChars = Environment.NewLine;

            FileStream stream = file.Create();

            XmlWriter writer = XmlWriter.Create(stream, settings);


            #region 写入数据到xml
            writer.WriteStartDocument(false);

            writer.WriteStartElement("Micron_COA");


            var log = new Logger();
            StringBuilder sb_log = new StringBuilder();

            //Header
            #region Header
            writer.WriteStartElement("Header");
            sb_log.AppendLine($"[Headers]");
            foreach (var item in model.Header)
            {
                writer.WriteStartElement("BasicInfoField");
                writer.WriteAttributeString("FieldName", item.FieldName);
                writer.WriteAttributeString("FieldValue", item.FieldValue);
                writer.WriteEndElement();
                sb_log.AppendLine($"{item.FieldName};{item.FieldValue}");
            }

            writer.WriteEndElement();
            #endregion

            //Content
            #region Content
            writer.WriteStartElement("Content");
            writer.WriteStartElement("UnitId");
            writer.WriteAttributeString("Value", "");

            sb_log.AppendLine($"[Content]");
            sb_log.AppendLine($"ItemName;ResultItem;ResultName;DetectionLimit;Value;");

            foreach (var item in model.InspectionItems)
            {
                writer.WriteStartElement("InspectionItem");
                writer.WriteAttributeString("ItemName", item.ItemName);
                sb_log.Append($"{item.ItemName};");
                foreach (var resultItem in item.ResultItems)
                {
                    sb_log.Append($"{resultItem.Value};");

                    if (resultItem.ResultName.Contains("DetectionLimit"))
                    {
                        continue;
                    }
                    writer.WriteStartElement("ResultItem");
                    writer.WriteAttributeString("ResultName", resultItem.ResultName);
                    writer.WriteAttributeString("Value", resultItem.Value);
                    writer.WriteEndElement();

                }
                sb_log.AppendLine();
                writer.WriteEndElement();

            }

            log.LogIt(sb_log.ToString(),$"OutputFile\\{filerawname}-log.txt");
            writer.WriteEndElement();
            writer.WriteEndElement();
            #endregion

            writer.WriteEndElement();


            #endregion

            writer.Flush();
            writer.Close();
            stream.Flush();
            stream.Close();

            if (XSHelper.MessageHelper.ShowYesNo("创建完毕,要打开吗？"))
            {
                //System.Diagnostics.Process.Start(folder);
                System.Diagnostics.Process.Start(filePath);
            }
        }
    }
}
