using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace PMSXMLCreator
{
    public class ECOAXMLGenerator
    {
        public void CreateXMLFile(ECOA model)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folder = Path.Combine(desktop, "XML Files");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string filePath = Path.Combine(folder, $"{model.ProductID}-{model.ProductAbbr}-{DateTime.Now.ToString("HHmmss")}.xml");
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

            string email = Properties.Settings.Default.ResponsiblePartyEmail;
            writer.WriteElementString("responsiblePartyEmail", email);
            //writer.WriteElementString("FileCreateDate", $"{DateTime.Today}");

            writer.WriteEndElement();
            #endregion


            #region BusinessSites
            writer.WriteStartElement("BusinessSites");
            writer.WriteStartElement("BusinessSiteDescription");


            writer.WriteElementString("manufactureNumber", Properties.Settings.Default.ManufacturerNumber);
            writer.WriteElementString("manufactureName", Properties.Settings.Default.ManufacturerName);
            writer.WriteElementString("manufacturePlantCode", Properties.Settings.Default.ManufacturerPlantCode);
            writer.WriteElementString("incomingFaxNumber", Properties.Settings.Default.IncomingFaxNumber);
            //writer.WriteElementString("ManufactureWebSite", "http://www.cdpmi.net");
            //writer.WriteElementString("ManufactureContact", "leon chiu@pioneer-material.com");


            #region QualityCertificates
            writer.WriteStartElement("QualityCertificates");
            writer.WriteStartElement("QualityCertificate");

            writer.WriteAttributeString("CertificateType", "SingleCertificate");

            writer.WriteElementString("thisDocumentGenerationDateTime",
                $"{DateTime.Now.ToString("yyyy-MM-dd")}");

            //00表示全新，05表示替换
            writer.WriteElementString("ReleaseType", "00");

            #region ProductDescription
            writer.WriteStartElement("ProductDescription");
            writer.WriteElementString("productName", model.ProductName);
            writer.WriteElementString("manufacturePartNumber", "12345678");
            writer.WriteElementString("manufactureOrderNumber", "12345678");

            writer.WriteElementString("partNumber", "500383762");
            writer.WriteElementString("partRevisionNumber", "01");

            writer.WriteElementString("lotCreatedDate",
                $"{DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")}");
            writer.WriteElementString("lotNumber", model.ProductID);


            writer.WriteEndElement();
            #endregion

            writer.WriteStartElement("Shipment");
            writer.WriteElementString("deliveryTo", model.DeliveryTo);
            writer.WriteElementString("scheduledshipdate", model.ScheduledShipDate.ToShortDateString());
            writer.WriteElementString("actualshipdate", model.ActualShipDate.ToShortDateString());
            writer.WriteStartElement("containers");
            writer.WriteElementString("container", "USP19251654");
            writer.WriteElementString("container", "USP19251655");
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("Comments");
            writer.WriteElementString("Comment", "");
            writer.WriteEndElement();

            #region MaterialParameters
            writer.WriteStartElement("MaterialParameters");
            //Basic
            AddBasic(writer, "Target Weight", "Weight", "g", "Value", model.Weight);
            AddBasic(writer, "Target Density", "Density", "g/cm3", "Value", model.Density);
            AddBasic(writer, "Resistance", "Resistance", "ohm cm", "Value", model.Resistance);
            AddBasic(writer, "Actual Dimension", "Actual Dimension", "mm", "Value", model.ActualDimension);
            //XRF
            AddXRF(writer, model.XRF);
            //GDMS
            AddGDMS(writer, model.GDMS);

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


            CommonHelper.ShowMessage("创建完毕");
            System.Diagnostics.Process.Start(filePath);

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
                //writer.WriteElementString("Characteristic", "GDMS Items");
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
