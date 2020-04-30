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
    public class XmlHelper_ns2 : IXmlHelper
    {

        private Analysis analysis = new Analysis();
        //设定命名空间参数
        private string ns_prefix = "ns1";
        private string ns = "x-schema../Schema/UltQualityCertificateSchema2016Dec.xml";
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
            writer.WriteStartElement(ns_prefix, "QualityCertificateFile", ns);
            //writer.WriteAttributeString("xmlns", "ns1", null, "x-schema../Schema/UltQualityCertificateSchema2016Dec.xml");
            writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
            //writer.WriteAttributeString("xsi", "schemaLocation", null,
            //    "http://www.cdpmi.net PMITargetForIntelSchema.xsd");

            #region FileCreationInfo
            writer.WriteStartElement(ns_prefix, "FileCreationInfo", ns);

            writer.WriteElementString(ns_prefix, "responsiblePartyEmail", ns, model.ResponsiblePartyEmail);
            //writer.WriteElementString("FileCreateDate", $"{DateTime.Today}");

            writer.WriteEndElement();
            #endregion


            #region BusinessSites
            writer.WriteStartElement(ns_prefix, "BusinessSites", ns);
            writer.WriteStartElement(ns_prefix, "BusinessSiteDescription", ns);


            writer.WriteElementString(ns_prefix, "manufacturerNumber", ns, model.ManufacturerNumber);
            writer.WriteElementString(ns_prefix, "manufacturerName", ns, model.ManufacturerName);
            writer.WriteElementString(ns_prefix, "manufacturingPlantCode", ns, model.ManufacturerPlantCode);
            writer.WriteElementString(ns_prefix, "incomingFaxNumber", ns, model.IncomingFaxNumber);
            //writer.WriteElementString(ns_prefix,"ManufactureWebSite",ns, "http://www.cdpmi.net");
            //writer.WriteElementString(ns_prefix,"ManufactureContact",ns, "leon chiu@pioneer-material.com");


            #region QualityCertificates
            writer.WriteStartElement(ns_prefix, "QualityCertificates", ns);
            writer.WriteStartElement(ns_prefix, "QualityCertificate", ns);

            writer.WriteAttributeString("CertificateType", "SingleCertificate");

            writer.WriteElementString(ns_prefix, "thisDocumentGenerationDateTime", ns,
                $"{model.ThisDocumentGenerationDateTime.ToString("yyyy-MM-ddTHH:mm:ss")}");//19个字符

            //00表示全新，05表示替换
            writer.WriteElementString(ns_prefix, "ReleaseType", ns, model.ReleaseType);

            #region ProductDescription
            writer.WriteStartElement(ns_prefix, "ProductDescription", ns);
            writer.WriteElementString(ns_prefix, "productName", ns, model.ProductName);
            writer.WriteElementString(ns_prefix, "manufacturerPartNumber", ns, model.ManufacturerPartNumber);//35个字符
            writer.WriteElementString(ns_prefix, "manufacturerOrderNumber", ns, model.ManufacturerOrderNumber);

            writer.WriteElementString(ns_prefix, "partNumber", ns, model.PartNumber);
            writer.WriteElementString(ns_prefix, "partRevisionNumber", ns, model.PartRevisionNumber);

            writer.WriteElementString(ns_prefix, "lotCreatedDate", ns,
                $"{model.LotCreatedDate.ToString("yyyy-MM-dd")}");
            writer.WriteElementString(ns_prefix, "lotNumber", ns, model.LotNumber);


            writer.WriteEndElement();
            #endregion

            writer.WriteStartElement(ns_prefix, "Shipment", ns);
            writer.WriteElementString(ns_prefix, "deliverTo", ns, model.DeliverTo);
            //writer.WriteElementString(ns_prefix, "shipmentnumber", ns, model.ShipmentNumber);
            writer.WriteElementString(ns_prefix, "scheduledshipdate", ns, model.ScheduledShipDate.ToShortDateString());
            writer.WriteElementString(ns_prefix, "actualshipdate", ns, model.ActualShipDate.ToShortDateString());
            writer.WriteStartElement(ns_prefix, "containers", ns);
            writer.WriteElementString(ns_prefix, "containernumber", ns, model.ContainerNumber);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement(ns_prefix, "Comments", ns);
            writer.WriteElementString(ns_prefix, "Comment", ns, $"{model.Comment} {model.BackPlateNumber}");
            writer.WriteEndElement();

            #region MaterialParameters
            writer.WriteStartElement(ns_prefix, "MaterialParameters", ns);

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
            writer.WriteStartElement(ns_prefix, "MaterialParameter", ns);

            writer.WriteElementString(ns_prefix, "rawLotID", ns, "");
            writer.WriteElementString(ns_prefix, "rawMaterialType", ns, "");

            writer.WriteElementString(ns_prefix, "SourceComponent", ns, p.SourceComponent);
            writer.WriteElementString(ns_prefix, "Characteristic", ns, p.Characteristic);
            writer.WriteElementString(ns_prefix, "ShortName", ns, p.ShortName);
            writer.WriteElementString(ns_prefix, "UnitOfMeasure", ns, p.UnitOfMeasure);
            //writer.WriteElementString(ns_prefix, "measurementQualifier", ns, p.MeasurementQualifier);
            //writer.WriteElementString(ns_prefix, "measurementType", ns, p.MeasurementType);
            //writer.WriteElementString(ns_prefix, "measurementValue", ns, p.MeasurementValue);

            writer.WriteStartElement(ns_prefix, "Measurements", ns);
            foreach (var item in p.Measurements)
            {
                writer.WriteStartElement(ns_prefix, "Measurement", ns);
                writer.WriteElementString(ns_prefix, "MeasurementType", ns, item.MeasurementType);
                writer.WriteElementString(ns_prefix, "MeasurementValue", ns, item.MeasurementValue);
                writer.WriteElementString(ns_prefix, "UCL", ns, item.UCL);
                writer.WriteElementString(ns_prefix, "LCL", ns, item.LCL);
                writer.WriteElementString(ns_prefix, "MDL", ns, item.MDL);
                writer.WriteElementString(ns_prefix, "CLCalc", ns, item.CLCalc);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }




        /// <summary>
        /// 暂时不用
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="character"></param>
        /// <param name="shortname"></param>
        /// <param name="unit"></param>
        /// <param name="mtype"></param>
        /// <param name="mvalue"></param>
        private void AddBasic(XmlWriter writer,
            string character, string shortname, string unit, string mtype, string mvalue)
        {
            writer.WriteStartElement(ns_prefix, "MaterialParameter", ns);
            writer.WriteElementString(ns_prefix, "Characteristic", ns, character);
            writer.WriteElementString(ns_prefix, "ShortName", ns, shortname);
            writer.WriteElementString(ns_prefix, "UnitOfMeasure", ns, unit);

            writer.WriteStartElement(ns_prefix, "Measurements", ns);
            writer.WriteStartElement(ns_prefix, "Measurement", ns);
            writer.WriteElementString(ns_prefix, "MeasurementType", ns, mtype);
            writer.WriteElementString(ns_prefix, "MeasurementValue", ns, mvalue);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        /// <summary>
        /// 暂时不用
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="gdms"></param>
        private void AddGDMS(XmlWriter writer, string gdms)
        {
            string[] items = gdms.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (items.Length > 0)
            {
                writer.WriteStartElement(ns_prefix, "MaterialParameter", ns);
                writer.WriteElementString(ns_prefix, "Characteristic", ns, "GDMS Items");
                writer.WriteElementString(ns_prefix, "shortName", ns, "GDMS");
                writer.WriteElementString(ns_prefix, "unitOfMeasure", ns, "ppm");
                writer.WriteStartElement(ns_prefix, "measurements", ns);

                foreach (var item in items)
                {
                    string[] temp = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (temp.Length >= 2)
                    {
                        writer.WriteStartElement(ns_prefix, "measurement", ns);
                        writer.WriteElementString(ns_prefix, "measurementType", ns, $"GDMS-{temp[0]}");
                        writer.WriteElementString(ns_prefix, "measurementValue", ns, temp[1]);
                        writer.WriteElementString(ns_prefix, "UCL", ns, temp[1]);
                        writer.WriteElementString(ns_prefix, "LCL", ns, temp[1]);
                        writer.WriteElementString(ns_prefix, "MDL", ns, temp[1]);
                        writer.WriteElementString(ns_prefix, "CLCalc", ns, temp[1]);

                        writer.WriteEndElement();
                    }

                }


                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// 暂时不用
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="xrf"></param>

        private void AddXRF(XmlWriter writer, string xrf)
        {

            string[] lines = xrf.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            writer.WriteStartElement(ns_prefix, "MaterialParameter", ns);
            writer.WriteElementString(ns_prefix, "Characteristic", ns, "Target Composition");
            writer.WriteElementString(ns_prefix, "ShortName", ns, "XRF");
            writer.WriteElementString(ns_prefix, "UnitOfMeasure", ns, "atm %");
            writer.WriteStartElement(ns_prefix, "Measurements", ns);


            string[] title = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] values = lines[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 1; j < title.Length; j++)
            {
                writer.WriteStartElement(ns_prefix, "Measurement", ns);
                writer.WriteElementString(ns_prefix, "MeasurementType", ns, title[j]);
                writer.WriteElementString(ns_prefix, "MeasurementValue", ns, values[j]);

                writer.WriteEndElement();
            }


            writer.WriteEndElement();
            writer.WriteEndElement();
        }


    }
}
