using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace PMSXMLCreator
{
    public class ECOAXMLHelper
    {
        public void CreateXMLFile(ECOA model)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, $"{DateTime.Now.ToString("yyMMddHHmmss")}.xml");
            FileInfo file = new FileInfo(filePath);

            //使用xmlwriter
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding(false);
            settings.NewLineChars = Environment.NewLine;


            FileStream stream = file.Create();

            XmlWriter writer = XmlWriter.Create(stream, settings);
            #region 写入数据到xml
            writer.WriteStartDocument();

            writer.WriteStartElement("QualityCertificateFile", "http://www.cpdmi.net");
            writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
            //writer.WriteAttributeString("xsi", "schemaLocation", null,
            //    "http://www.cdpmi.net PMITargetForIntelSchema.xsd");

            #region FileCreationInfo
            writer.WriteStartElement("FileCreationInfo");

            writer.WriteElementString("ResponsiblePartyEmail", "cdpmi@pioneer-materials.com");
            writer.WriteElementString("FileCreateDate", $"{DateTime.Today}");

            writer.WriteEndElement();
            #endregion


            #region BusinessSites
            writer.WriteStartElement("BusinessSites");
            writer.WriteStartElement("BusinessSiteDescription");


            writer.WriteElementString("Manufacture", "Pioneer Materials Inc.");
            writer.WriteElementString("ManufactureNumber", "00");
            writer.WriteElementString("ManufacturePlantCode", "01");
            writer.WriteElementString("ManufactureWebSite", "http://www.cdpmi.net");
            writer.WriteElementString("ManufactureContact", "leon chiu@pioneer-material.com");


            #region QualityCertificates
            writer.WriteStartElement("QualityCertificates");
            writer.WriteStartElement("QualityCertificate");

            writer.WriteAttributeString("CertificateType", "Single");
            writer.WriteElementString("GenerationTime", $"{DateTime.Now}");
            writer.WriteElementString("ReleaseType", "version1.0");

            writer.WriteStartElement("ProductDescription");
            writer.WriteElementString("ProductID", model.ProductID);
            writer.WriteElementString("ProductName", model.ProductName);
            writer.WriteElementString("PONumber", model.PONumber);
            writer.WriteElementString("ManufacturePartNumber", "12345678");
            writer.WriteElementString("PartNumber", "00");
            writer.WriteElementString("PartRevisionNumber", "00");
            writer.WriteEndElement();

            writer.WriteStartElement("Shipment");
            writer.WriteElementString("DeliveryTo", model.DeliveryTo);
            writer.WriteElementString("ScheduledShipDate", model.ScheduledShipDate.ToShortDateString());
            writer.WriteElementString("ActualShipDate", model.ActualShipDate.ToShortDateString());
            writer.WriteStartElement("Containers");
            writer.WriteElementString("Container", "USP19251654");
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("Comments");
            writer.WriteElementString("Comment", "none");
            writer.WriteEndElement();

            #region MaterialParameters
            writer.WriteStartElement("MaterialParameters");
            //Weight
            SingleMaterialParameter(writer, "Target Weight", "Weight", "g", "Value", model.Weight);
            SingleMaterialParameter(writer, "Target Density", "Density", "g/cm3", "Value", model.Density);
            SingleMaterialParameter(writer, "Resistance", "Resistance", "ohm cm", "Value", model.Resistance);
            SingleMaterialParameter(writer, "Actual Dimension", "Actual Dimension", "mm", "Value", model.ActualDimension);

            AddGDMSPart(writer, model.GDMS);

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


        private void SingleMaterialParameter(XmlWriter writer,
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

        private void AddGDMSPart(XmlWriter writer, string gdms)
        {
            string[] items = gdms.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (items.Length > 0)
            {
                writer.WriteStartElement("MaterialParameter");
                writer.WriteElementString("Characteristic", "GDMS Items");
                writer.WriteElementString("ShortName", "GDMS");
                writer.WriteElementString("UnitOfMeasure", "ppm");
                writer.WriteStartElement("Measurements");

                foreach (var item in items)
                {
                    string[] temp = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (temp.Length >= 2)
                    {
                        writer.WriteStartElement("Measurement");
                        writer.WriteElementString("MeasurementType", $"GDMS-{temp[0]}");
                        writer.WriteElementString("MeasurementValue", temp[1]);

                        writer.WriteEndElement();
                    }

                }


                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

    }
}
