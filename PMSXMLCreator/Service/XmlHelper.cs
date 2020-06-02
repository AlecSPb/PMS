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
    /// 遵循Intel 2019-04-22_ULT_XML_EXAMPLE_FILE
    /// 生成XML文件
    /// </summary>
    public class XmlHelper : IXmlHelper
    {

        private Analysis analysis = new Analysis();
        //设定命名空间参数
        private string ns = "x-schema:../Schema/UltQualityCertificateSchema2016Dec.xml";
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
            writer.WriteStartDocument(false);
            writer.WriteStartElement("QualityCertificateFile", ns);
            //writer.WriteAttributeString("xmlns", "ns1", null, "x-schema:../Schema/UltQualityCertificateSchema2016Dec.xml");
            //writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
            //writer.WriteAttributeString("xsi", "schemaLocation", null,
            //    "http://www.cdpmi.net PMITargetForIntelSchema.xsd");

            #region FileCreationInfo
            writer.WriteStartElement("FileCreationInfo", ns);

            writer.WriteElementString("responsiblePartyEmail", ns, model.ResponsiblePartyEmail);
            //writer.WriteElementString("FileCreateDate", $"{DateTime.Today}");

            writer.WriteEndElement();
            #endregion


            #region BusinessSites
            writer.WriteStartElement("BusinessSites", ns);
            writer.WriteStartElement("BusinessSiteDescription", ns);


            writer.WriteElementString("manufacturerNumber", ns, model.ManufacturerNumber);
            writer.WriteElementString("manufacturerName", ns, model.ManufacturerName);
            writer.WriteElementString("manufacturingPlantCode", ns, model.ManufacturerPlantCode);
            writer.WriteElementString("incomingFaxNumber", ns, model.IncomingFaxNumber);
            //writer.WriteElementString("ManufactureWebSite",ns, "http://www.cdpmi.net");
            //writer.WriteElementString("ManufactureContact",ns, "leon chiu@pioneer-material.com");


            #region QualityCertificates
            writer.WriteStartElement("QualityCertificates", ns);
            writer.WriteStartElement("QualityCertificate", ns);

            writer.WriteAttributeString("CertificateType", "SingleCertificate");

            writer.WriteElementString("thisDocumentGenerationDateTime", ns,
                $"{model.ThisDocumentGenerationDateTime.ToString("yyyy-MM-ddTHH:mm:ss")}");//19个字符

            //00表示全新，05表示替换
            if (model.IsNew)
            {
                model.ReleaseType = "00";
            }
            else
            {
                model.ReleaseType = "05";
            }
            writer.WriteElementString("releaseType", ns, model.ReleaseType);
            #region ProductDescription
            writer.WriteStartElement("ProductDescription", ns);
            writer.WriteElementString("productName", ns, model.ProductName);
            writer.WriteElementString("manufacturerPartNumber", ns, model.ManufacturerPartNumber);//35个字符
            writer.WriteElementString("manufacturerOrderNumber", ns, model.ManufacturerOrderNumber);

            writer.WriteElementString("partNumber", ns, model.PartNumber);
            writer.WriteElementString("partRevisionNumber", ns, model.PartRevisionNumber);

            writer.WriteElementString("lotCreatedDate", ns,
                $"{model.LotCreatedDate.ToString("yyyy-MM-dd")}");
            writer.WriteElementString("lotNumber", ns, model.LotNumber);


            writer.WriteEndElement();
            #endregion

            writer.WriteStartElement("Shipment", ns);
            writer.WriteElementString("deliverTo", ns, model.DeliverTo);
            //writer.WriteElementString( "shipmentnumber", ns, model.ShipmentNumber);
            writer.WriteElementString("scheduledshipdate", ns, model.ScheduledShipDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            writer.WriteElementString("actualshipdate", ns, model.ActualShipDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            writer.WriteStartElement("containers", ns);
            writer.WriteElementString("containernumber", ns, model.ContainerNumber);
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("Comments", ns);
            writer.WriteElementString("comment", ns, $"{model.Comment} {model.BackPlateNumber}");
            writer.WriteEndElement();

            #region MaterialParameters
            writer.WriteStartElement("MaterialParameters", ns);

            //分析参数
            List<Parameter> parameters = analysis.GetAllECOAParamenters(model);


            StringBuilder log = new StringBuilder();

            double ucl = 0, lcl = 0,measureValue = 0 ;

            //日志头部
            log.AppendLine("Charactersitic;ShortName;MeasurementType;UnitOfMeasure;MeasurementValue;UCL;LCL;MeasurementValue;UCL Warning;LCL Warning");

            foreach (var p in parameters)
            {
                if (p.Type == "RawMaterial")
                {
                    AddMaterialParameter(writer, p);
                }
                else if (p.Type == "Measurement")
                {
                    AddMeasurementParameter(writer, p);
                }
                else
                {
                    AddMeasurementParameter(writer, p);
                }

                #region 日志记录
                log.Append($"{p.Characteristic};{p.ShortName};{p.Type};{p.Measurements[0].MeasurementType};{p.UnitOfMeasure}");
                log.Append($";{p.Measurements[0].MeasurementValue};{p.Measurements[0].UCL};{p.Measurements[0].LCL}");

                double.TryParse(p.Measurements[0].UCL, out ucl);
                double.TryParse(p.Measurements[0].LCL, out lcl);
                double.TryParse(p.Measurements[0].MeasurementValue, out measureValue);
                if (measureValue > ucl)
                {
                    log.Append(";UCL warning");
                }
                if (measureValue <lcl)
                {
                    log.Append(";LCL warning");
                }
                log.AppendLine();

                #endregion

            }
            new Log().LogIt(log.ToString());

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
                //System.Diagnostics.Process.Start(folder);
                System.Diagnostics.Process.Start(filePath);
            }
        }

        /// <summary>
        /// 原材料参数
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="p"></param>
        private void AddMaterialParameter(XmlWriter writer, Parameter p)
        {
            writer.WriteStartElement("MaterialParameter", ns);

            writer.WriteElementString("rawlotid", ns, "");
            writer.WriteElementString("rawmaterialtype", ns, "");

            writer.WriteElementString("sourcecomponent", ns, p.SourceComponent);
            //writer.WriteElementString( "characteristic", ns, p.Characteristic);
            writer.WriteElementString("shortName", ns, p.ShortName);
            writer.WriteElementString("unitOfMeasure", ns, p.UnitOfMeasure);
            //writer.WriteElementString( "measurementQualifier", ns, p.MeasurementQualifier);
            //writer.WriteElementString( "measurementType", ns, p.MeasurementType);
            //writer.WriteElementString( "measurementValue", ns, p.MeasurementValue);

            writer.WriteStartElement("measurements", ns);
            foreach (var item in p.Measurements)
            {
                writer.WriteStartElement("measurement", ns);
                writer.WriteElementString("measurementType", ns, item.MeasurementType);
                writer.WriteElementString("measurementValue", ns, item.MeasurementValue);
                writer.WriteElementString("UCL", ns, item.UCL);
                writer.WriteElementString("LCL", ns, item.LCL);
                writer.WriteElementString("MDL", ns, item.MDL);
                writer.WriteElementString("CLCalc", ns, item.CLCalc);

                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        /// <summary>
        /// 测量参数
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="p"></param>
        private void AddMeasurementParameter(XmlWriter writer, Parameter p)
        {
            writer.WriteStartElement("MaterialParameter", ns);

            //writer.WriteElementString( "rawlotid", ns, "");
            //writer.WriteElementString( "rawmaterialtype", ns, "");
            writer.WriteElementString("shortName", ns, p.ShortName);
            writer.WriteElementString("sourcecomponent", ns, p.SourceComponent);
            //writer.WriteElementString( "characteristic", ns, p.Characteristic);
            writer.WriteElementString("unitOfMeasure", ns, p.UnitOfMeasure);
            //writer.WriteElementString( "measurementQualifier", ns, p.MeasurementQualifier);
            //writer.WriteElementString( "measurementType", ns, p.MeasurementType);
            //writer.WriteElementString( "measurementValue", ns, p.MeasurementValue);

            writer.WriteStartElement("measurements", ns);
            foreach (var item in p.Measurements)
            {
                writer.WriteStartElement("measurement", ns);
                writer.WriteElementString("measurementType", ns, item.MeasurementType);
                writer.WriteElementString("measurementValue", ns, item.MeasurementValue);
                writer.WriteElementString("UCL", ns, item.UCL);
                writer.WriteElementString("LCL", ns, item.LCL);
                writer.WriteElementString("MDL", ns, item.MDL);
                writer.WriteElementString("CLCalc", ns, item.CLCalc);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }


        #region 暂时不用
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
            writer.WriteStartElement("MaterialParameter", ns);
            writer.WriteElementString("Characteristic", ns, character);
            writer.WriteElementString("ShortName", ns, shortname);
            writer.WriteElementString("UnitOfMeasure", ns, unit);

            writer.WriteStartElement("Measurements", ns);
            writer.WriteStartElement("Measurement", ns);
            writer.WriteElementString("MeasurementType", ns, mtype);
            writer.WriteElementString("MeasurementValue", ns, mvalue);
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
                writer.WriteStartElement("MaterialParameter", ns);
                writer.WriteElementString("Characteristic", ns, "GDMS Items");
                writer.WriteElementString("shortName", ns, "GDMS");
                writer.WriteElementString("unitOfMeasure", ns, "ppm");
                writer.WriteStartElement("measurements", ns);

                foreach (var item in items)
                {
                    string[] temp = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (temp.Length >= 2)
                    {
                        writer.WriteStartElement("measurement", ns);
                        writer.WriteElementString("measurementType", ns, $"GDMS-{temp[0]}");
                        writer.WriteElementString("measurementValue", ns, temp[1]);
                        writer.WriteElementString("UCL", ns, temp[1]);
                        writer.WriteElementString("LCL", ns, temp[1]);
                        writer.WriteElementString("MDL", ns, temp[1]);
                        writer.WriteElementString("CLCalc", ns, temp[1]);

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

            writer.WriteStartElement("MaterialParameter", ns);
            writer.WriteElementString("Characteristic", ns, "Target Composition");
            writer.WriteElementString("ShortName", ns, "XRF");
            writer.WriteElementString("UnitOfMeasure", ns, "atm %");
            writer.WriteStartElement("Measurements", ns);


            string[] title = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] values = lines[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 1; j < title.Length; j++)
            {
                writer.WriteStartElement("Measurement", ns);
                writer.WriteElementString("MeasurementType", ns, title[j]);
                writer.WriteElementString("MeasurementValue", ns, values[j]);

                writer.WriteEndElement();
            }


            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        #endregion


    }
}
