using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace PMSXMLCreator.XMLGenerator
{
    public class ECOAXMLGenerator
    {

        private Analysis analysis = new Analysis();

        public void CreateXMLFile(ECOA model)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folder = Path.Combine(desktop, "XML Files");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string filePath = Path.Combine(folder, $"{model.LotNumber}-{model.ProductName}-{DateTime.Now.ToString("HHmmss")}.xml");
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


            writer.WriteElementString("manufactureNumber", model.ManufacturerNumber);
            writer.WriteElementString("manufactureName", model.ManufacturerName);
            writer.WriteElementString("manufacturePlantCode", model.ManufacturerPlantCode);
            writer.WriteElementString("incomingFaxNumber", model.IncomingFaxNumber);
            //writer.WriteElementString("ManufactureWebSite", "http://www.cdpmi.net");
            //writer.WriteElementString("ManufactureContact", "leon chiu@pioneer-material.com");


            #region QualityCertificates
            writer.WriteStartElement("QualityCertificates");
            writer.WriteStartElement("QualityCertificate");

            writer.WriteAttributeString("CertificateType", "SingleCertificate");

            writer.WriteElementString("thisDocumentGenerationDateTime",
                $"{model.ThisDocumentGenerationDateTime.ToString("yyyy-MM-dd")}");

            //00表示全新，05表示替换
            writer.WriteElementString("ReleaseType", model.ReleaseType);

            #region ProductDescription
            writer.WriteStartElement("ProductDescription");
            writer.WriteElementString("productName", model.ProductName);
            writer.WriteElementString("manufacturePartNumber", model.ManufacturerPartNumber);
            writer.WriteElementString("manufactureOrderNumber", model.ManufacturerOrderNumber);

            writer.WriteElementString("partNumber", model.PartNumber);
            writer.WriteElementString("partRevisionNumber", model.PartRevisionNumber);

            writer.WriteElementString("lotCreatedDate",
                $"{model.LotCreatedDate.ToString("yyyy-MM-dd")}");
            writer.WriteElementString("lotNumber", model.LotNumber);


            writer.WriteEndElement();
            #endregion

            writer.WriteStartElement("Shipment");
            writer.WriteElementString("deliveryTo", model.DeliverTo);
            writer.WriteElementString("scheduledshipdate", model.ScheduledShipDate.ToShortDateString());
            writer.WriteElementString("actualshipdate", model.ActualShipDate.ToShortDateString());
            writer.WriteStartElement("containers");
            writer.WriteElementString("container", model.Containers);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("Comments");
            writer.WriteElementString("Comment", model.Comment);
            writer.WriteEndElement();

            #region MaterialParameters
            writer.WriteStartElement("MaterialParameters");

            List<Parameter> parameters = analysis.GetParamenters(model);

            foreach (var p in parameters)
            {
                AddMaterialParameter(writer, p);
            }

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


            Helper.ShowMessage("创建完毕,即将打开");
            System.Diagnostics.Process.Start(filePath);

        }

        /// <summary>
        /// 添加MaterialParameter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="p"></param>
        private void AddMaterialParameter(XmlWriter writer, Parameter p)
        {
            writer.WriteStartElement("MaterialParameter");
            writer.WriteElementString("Characteristic", p.Characteristic);
            writer.WriteElementString("ShortName", p.ShortName);
            writer.WriteElementString("UnitOfMeasure", p.UnitOfMeasure);
            writer.WriteElementString("SourceComponent", p.SourceComponent);

            writer.WriteStartElement("Measurements");
            foreach (var item in p.Measurements)
            {
                writer.WriteStartElement("Measurement");
                writer.WriteElementString("MeasurementType", item.MeasurementType);
                writer.WriteElementString("MeasurementValue", item.MeasurementValue);
                writer.WriteElementString("UCL", item.UCL);
                writer.WriteElementString("LCL", item.LCL);
                writer.WriteElementString("MDL", item.MDL);
                writer.WriteElementString("CLCalc", item.CLCalc);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

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
