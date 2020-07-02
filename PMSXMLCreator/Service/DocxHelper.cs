using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;
using System.IO;
using PMSXMLCreator.Model;
using System.Diagnostics;
using System.Windows;
using CommonHelper;
using PMSXMLCreator.Service;

namespace PMSXMLCreator
{
    /// <summary>
    /// 生成docx文件
    /// </summary>
    public class DocxHelper
    {
        private Analysis anlysis;
        public void CreateFile(ECOA model)
        {
            #region 检查逻辑
            if (Service.CheckLogic.CheckLotNumber(model.LotNumber))
            {
                XSHelper.MessageHelper.ShowStop($"LotNumber[{model.LotNumber}]中的#000编号部分不能透漏给客户,请删除");
                return;
            }
            #endregion

            anlysis= new Analysis(model.CurrentSpec);

            string folder = XSHelper.FileHelper.GetCurrentFolderPath("OutputFile");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string filename = $"{model.LotNumber}-{model.ProductName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}.docx";
            string filePath = Path.Combine(folder, filename.Replace("#", "-"));

            string current_template = Path.Combine(Environment.CurrentDirectory, "DocTemplate", "Intel COA.docx");
            string temp = Path.Combine(Environment.CurrentDirectory, "temp.docx");
            try
            {
                File.Copy(current_template, temp, true);


                using (var doc = DocX.Load(temp))
                {
                    #region 填写基础信息
                    doc.ReplaceText("[ProductName]", model.ProductName ?? "");
                    doc.ReplaceText("[ProductID]", model.LotNumber ?? "");
                    doc.ReplaceText("[PrintTime]", DateTime.Now.ToString());
                    doc.ReplaceText("[CreateTime]", model.LotCreatedDate.ToString() ?? "");
                    doc.ReplaceText("[SuppNumber]", model.ManufacturerNumber ?? "");
                    doc.ReplaceText("[SuppPartNumber]", model.ManufacturerPartNumber ?? "");

                    doc.ReplaceText("[IntelPartDesc]", model.PartNumberDesc ?? "");
                    doc.ReplaceText("[IntelPartNumber]", model.PartNumber ?? "");
                    doc.ReplaceText("[IntelPartRev]", model.PartRevisionNumber ?? "");

                    doc.ReplaceText("[Comment]", model.Comment ?? "");
                    doc.ReplaceText("[AMLStatus]", model.AMLStatus ?? "");
                    doc.ReplaceText("[AMLNotes]", model.AMLNotes ?? "");
                    doc.ReplaceText("[EHSNumber]", model.EHSNumber ?? "");
                    doc.ReplaceText("[CSize]", model.ContainerSize ?? "");
                    doc.ReplaceText("[CWt]", model.ContainerWeight ?? "");
                    doc.ReplaceText("[SelfLife]", model.SelfLife ?? "");
                    doc.ReplaceText("[BackPlateNumber]", model.BackPlateNumber ?? "");
                    #endregion

                    Table main_table = doc.Tables[0];

                    int start_index = 9;


                    #region 填写成分
                    //List<Parameter> compositions = anlysis.GetProductNameComposition(model.ProductName);
                    List<Parameter> compositions = anlysis.SolveXRFByKeyStr(model.XRF);
                    foreach (var item in compositions)
                    {
                        main_table.Rows[start_index].Cells[0].Paragraphs[0].Append(item.Characteristic + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[4].Paragraphs[0].Append(item.Measurements[0].LCL + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[5].Paragraphs[0].Append(item.Measurements[0].UCL + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[6].Paragraphs[0].Append(item.Measurements[0]
                            .MeasurementValue + "\r").FontSize(6).Bold();
                        main_table.Rows[start_index].Cells[7].Paragraphs[0].Append(item.UnitOfMeasure + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[8].Paragraphs[0].Append(item.ShortName + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[9].Paragraphs[0].Append($"{item.MeasurementQualifier}" + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[10].Paragraphs[0].Append("Individual" + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[11].Paragraphs[0].Append("Key" + "\r").FontSize(6);
                    }

                    #endregion

                    #region 填写性质
                    List<Parameter> properties = anlysis.GetPropertiesParameters(model);
                    foreach (var item in properties)
                    {
                        main_table.Rows[start_index + 3].Cells[0].Paragraphs[0].Append(item.Characteristic + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[4].Paragraphs[0].Append(item.Measurements[0].LCL + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[5].Paragraphs[0].Append(item.Measurements[0].UCL + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[6].Paragraphs[0].Append(item.Measurements[0]
                            .MeasurementValue + "\r").FontSize(6).Bold();
                        main_table.Rows[start_index + 3].Cells[7].Paragraphs[0].Append(item.UnitOfMeasure + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[8].Paragraphs[0].Append(item.ShortName + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[9].Paragraphs[0].Append($"{item.MeasurementQualifier}" + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[10].Paragraphs[0].Append("Individual" + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[11].Paragraphs[0].Append("Key" + "\r").FontSize(6);
                    }
                    #endregion

                    #region 填写纯度
                    List<Parameter> purities = anlysis.SolveElementByKeyStr(model.GDMS);
                    foreach (var item in purities)
                    {

                        main_table.Rows[start_index + 6].Cells[0].Paragraphs[0].Append(item.Characteristic + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[4].Paragraphs[0].Append(item.Measurements[0].LCL + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[5].Paragraphs[0].Append(item.Measurements[0].UCL + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[6].Paragraphs[0].Append(item.Measurements[0]
                            .MeasurementValue + "\r").FontSize(6).Bold();
                        main_table.Rows[start_index + 6].Cells[7].Paragraphs[0].Append(item.UnitOfMeasure + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[8].Paragraphs[0].Append(item.ShortName + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[9].Paragraphs[0].Append($"{item.MeasurementQualifier}" + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[10].Paragraphs[0].Append("Batch" + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[11].Paragraphs[0].Append("Key" + "\r").FontSize(6);
                    }
                    #endregion

                    #region 填写含氧量
                    List<Parameter> vpis = anlysis.SolveElementByKeyStr(model.VPI);
                    foreach (var item in vpis)
                    {

                        main_table.Rows[start_index + 9].Cells[0].Paragraphs[0].Append(item.Characteristic + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[4].Paragraphs[0].Append(item.Measurements[0].LCL + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[5].Paragraphs[0].Append(item.Measurements[0].UCL + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[6].Paragraphs[0].Append(item.Measurements[0]
                            .MeasurementValue + "\r").FontSize(6).Bold();
                        main_table.Rows[start_index + 9].Cells[7].Paragraphs[0].Append(item.UnitOfMeasure + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[8].Paragraphs[0].Append(item.ShortName + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[9].Paragraphs[0].Append($"{item.MeasurementQualifier}" + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[10].Paragraphs[0].Append("Batch" + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[11].Paragraphs[0].Append("Key" + "\r").FontSize(6);
                    }
                    #endregion

                    doc.Save();
                }
                File.Copy(temp, filePath, true);
                if (XSHelper.MessageHelper.ShowYesNo("创建完毕,要打开吗？"))
                {
                    System.Diagnostics.Process.Start(filePath);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
