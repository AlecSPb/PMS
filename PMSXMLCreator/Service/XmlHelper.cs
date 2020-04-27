using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using CommonHelper;

namespace PMSXMLCreator.Service
{
    /// <summary>
    /// 生成XML文件
    /// </summary>
    public class XmlHelper
    {

        private Analysis analysis = new Analysis();

        public void CreateFile(ECOA model)
        {
            #region 检查逻辑
            if (Service.CheckLogic.CheckLotNumber(model.LotNumber))
            {
                XSHelper.MessageHelper.ShowStop($"LotNumber[{model.LotNumber}]中的#000编号部分不能透漏给客户,请删除");
                return;
            }
            #endregion


            string folder = XSHelper.FileHelper.GetCurrentFolderPath("OutputFile");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string filename = $"{model.LotNumber}-{model.ProductName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xml";
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
            //true表示standalone
            writer.WriteStartDocument(true);

            writer.WriteStartElement("QualityCertificateFile", "x-schema../Schema/UltQualityCertificateSchema2016Dec.xml");
            //writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
            //writer.WriteAttributeString("xsi", "schemaLocation", null,
            //    "http://www.cdpmi.net PMITargetForIntelSchema.xsd");

            #region FileCreationInfo
            writer.WriteStartElement("FileCreationInfo");

            writer.WriteElementString("responsiblePartyEmail", model.ResponsiblePartyEmail);
            //writer.WriteElementString("FileCreateDate", $"{DateTime.Today}");

            writer.WriteEndElement();
            #endregion


            #region BusinessSites
            writer.WriteStartElement("BusinessSites");
            writer.WriteStartElement("BusinessSiteDescription");


            writer.WriteElementString("manufacturerNumber", model.ManufacturerNumber);
            writer.WriteElementString("manufacturerName", model.ManufacturerName);
            writer.WriteElementString("manufacturingPlantCode", model.ManufacturerPlantCode);
            writer.WriteElementString("incomingFaxNumber", model.IncomingFaxNumber);
            //writer.WriteElementString("ManufactureWebSite", "http://www.cdpmi.net");
            //writer.WriteElementString("ManufactureContact", "leon chiu@pioneer-material.com");


            #region QualityCertificates
            writer.WriteStartElement("QualityCertificates");
            writer.WriteStartElement("QualityCertificate");

            writer.WriteAttributeString("CertificateType", "SingleCertificate");

            writer.WriteElementString("thisDocumentGenerationDateTime",
                $"{model.ThisDocumentGenerationDateTime.ToString("yyyy-MM-ddTHH:mm:ss")}");//19个字符

            //00表示全新，05表示替换
            writer.WriteElementString("ReleaseType", model.ReleaseType);

            #region ProductDescription
            writer.WriteStartElement("ProductDescription");
            writer.WriteElementString("productName", model.ProductName);
            writer.WriteElementString("manufacturerPartNumber", model.ManufacturerPartNumber);//35个字符
            //writer.WriteElementString("manufacturerOrderNumber", model.ManufacturerOrderNumber);

            writer.WriteElementString("partNumber", model.PartNumber);
            writer.WriteElementString("partRevisionNumber", model.PartRevisionNumber);

            writer.WriteElementString("lotCreatedDate",
                $"{model.LotCreatedDate.ToString("yyyy-MM-dd")}");
            writer.WriteElementString("lotNumber", model.LotNumber);


            writer.WriteEndElement();
            #endregion

            writer.WriteStartElement("Shipment");
            writer.WriteElementString("deliverTo", model.DeliverTo);
            writer.WriteElementString("shipmentnumber", model.ShipmentNumber);
            //writer.WriteElementString("scheduledshipdate", model.ScheduledShipDate.ToShortDateString());
            //writer.WriteElementString("actualshipdate", model.ActualShipDate.ToShortDateString());
            writer.WriteStartElement("containers");
            writer.WriteElementString("containernumber", model.ContainerNumber);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("Comments");
            writer.WriteElementString("Comment", $"{model.Comment} {model.BackPlateNumber}");
            writer.WriteEndElement();

            #region MaterialParameters
            writer.WriteStartElement("MaterialParameters");

            //分析参数
            List<Parameter> parameters = analysis.GetAllECOAParamenters(model);


            StringBuilder sb = new StringBuilder();
            foreach (var p in parameters)
            {
                AddMaterialParameter(writer, p);
                sb.AppendLine($"{p.Characteristic}-{p.ShortName}");
            }
            new Log().LogIt(sb.ToString());

            writer.WriteEndElement();
            #endregion

            writer.WriteEndElement();//QualityCertificate
            writer.WriteEndElement();//QualityCertificates
            #endregion


            writer.WriteEndElement();//BusinessSite
            writer.WriteEndElement();//BusinessSites
            #endregion

            writer.WriteEndElement();
            writer.WriteEndDocument();
            #endregion
            writer.Flush();
            writer.Close();
            stream.Flush();
            stream.Close();

            if (XSHelper.MessageHelper.ShowYesNo("创建完毕,要打开吗？"))
            {
                System.Diagnostics.Process.Start(folder);
                System.Diagnostics.Process.Start(filePath);
            }
        }

        /// <summary>
        /// 添加MaterialParameter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="p"></param>
        private void AddMaterialParameter(XmlWriter writer, Parameter p)
        {
            writer.WriteStartElement("MaterialParameter");

            //writer.WriteElementString("rawLotID", "");
            //writer.WriteElementString("rawMaterialType", "");

            writer.WriteElementString("SourceComponent", p.SourceComponent);
            writer.WriteElementString("Characteristic", p.Characteristic);
            writer.WriteElementString("ShortName", p.ShortName);
            writer.WriteElementString("UnitOfMeasure", p.UnitOfMeasure);
            writer.WriteElementString("measurementQualifier", p.MeasurementQualifier);
            writer.WriteElementString("measurementType", p.MeasurementType);
            writer.WriteElementString("measurementValue", p.MeasurementValue);

            //writer.WriteStartElement("Measurements");
            //foreach (var item in p.Measurements)
            //{
            //    writer.WriteStartElement("Measurement");
            //    writer.WriteElementString("MeasurementType", item.MeasurementType);
            //    writer.WriteElementString("MeasurementValue", item.MeasurementValue);
            //    writer.WriteElementString("UCL", item.UCL);
            //    writer.WriteElementString("LCL", item.LCL);
            //    writer.WriteElementString("MDL", item.MDL);
            //    writer.WriteElementString("CLCalc", item.CLCalc);
            //    writer.WriteEndElement();
            //}
            //writer.WriteEndElement();

            writer.WriteEndElement();
        }





        private void AddBasic(XmlWriter writer,
            string character, string shortname, string unit, string mtype, string mvalue)
        {
            writer.WriteStartElement("MaterialParameter");
            writer.WriteElementString("Characteristic", character);
            writer.WriteElementString("ShortName", shortname);
            writer.WriteElementString("UnitOfMeasure", unit);

            writer.WriteStartElement("Measurements");
            writer.WriteStartElement("Measurement");
            writer.WriteElementString("MeasurementType", mtype);
            writer.WriteElementString("MeasurementValue", mvalue);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private void AddGDMS(XmlWriter writer, string gdms)
        {
            string[] items = gdms.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (items.Length > 0)
            {
                writer.WriteStartElement("MaterialParameter");
                writer.WriteElementString("Characteristic", "GDMS Items");
                writer.WriteElementString("shortName", "GDMS");
                writer.WriteElementString("unitOfMeasure", "ppm");
                writer.WriteStartElement("measurements");

                foreach (var item in items)
                {
                    string[] temp = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (temp.Length >= 2)
                    {
                        writer.WriteStartElement("measurement");
                        writer.WriteElementString("measurementType", $"GDMS-{temp[0]}");
                        writer.WriteElementString("measurementValue", temp[1]);
                        writer.WriteElementString("UCL", temp[1]);
                        writer.WriteElementString("LCL", temp[1]);
                        writer.WriteElementString("MDL", temp[1]);
                        writer.WriteElementString("CLCalc", temp[1]);

                        writer.WriteEndElement();
                    }

                }


                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }


        private void AddXRF(XmlWriter writer, string xrf)
        {

            string[] lines = xrf.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            writer.WriteStartElement("MaterialParameter");
            writer.WriteElementString("Characteristic", "Target Composition");
            writer.WriteElementString("ShortName", "XRF");
            writer.WriteElementString("UnitOfMeasure", "atm %");
            writer.WriteStartElement("Measurements");


            string[] title = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] values = lines[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 1; j < title.Length; j++)
            {
                writer.WriteStartElement("Measurement");
                writer.WriteElementString("MeasurementType", title[j]);
                writer.WriteElementString("MeasurementValue", values[j]);

                writer.WriteEndElement();
            }


            writer.WriteEndElement();
            writer.WriteEndElement();
        }


    }
}
