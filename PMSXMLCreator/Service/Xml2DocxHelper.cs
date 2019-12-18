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

namespace PMSXMLCreator
{
    public class Xml2DocxHelper
    {
        public void CreateFile(ECOA model)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folder = Path.Combine(desktop, "XML Files");
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
                //引入分析类
                var anlysis = new Service.Analysis();

                using (var doc = DocX.Load(temp))
                {
                    #region 填写基础信息
                    doc.ReplaceText("[PrintTime]", DateTime.Now.ToString());
                    doc.ReplaceText("[CreateTime]", model.LotCreatedDate.ToString());
                    doc.ReplaceText("[SuppNumber]", model.ManufacturerNumber);
                    doc.ReplaceText("[SuppPartNumber]", model.ManufacturerPartNumber);
                    doc.ReplaceText("[ProductID]", model.LotNumber);
                    #endregion

                    Table main_table = doc.Tables[0];

                    int start_index = 9;


                    #region 填写成分
                    List<Parameter> compositions = anlysis.GetProductNameComposition(model.ProductName);
                    foreach (var item in compositions)
                    {
                        main_table.Rows[start_index].Cells[0].Paragraphs[0].Append(item.Characteristic + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[4].Paragraphs[0].Append("1" + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[5].Paragraphs[0].Append("1" + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[6].Paragraphs[0].Append(item.Measurements[0]
                            .MeasurementValue + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[7].Paragraphs[0].Append(item.UnitOfMeasure + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[8].Paragraphs[0].Append(item.ShortName + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[9].Paragraphs[0].Append("w/w" + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[10].Paragraphs[0].Append("Batch" + "\r").FontSize(6);
                        main_table.Rows[start_index].Cells[11].Paragraphs[0].Append("Key" + "\r").FontSize(6);
                    }

                    #endregion

                    #region 填写性质
                    List<Parameter> properties = anlysis.GetPropertiesParameters(model);
                    foreach (var item in properties)
                    {
                        main_table.Rows[start_index+3].Cells[0].Paragraphs[0].Append(item.Characteristic + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[4].Paragraphs[0].Append("1" + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[5].Paragraphs[0].Append("1" + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[6].Paragraphs[0].Append(item.Measurements[0]
                            .MeasurementValue + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[7].Paragraphs[0].Append(item.UnitOfMeasure + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[8].Paragraphs[0].Append(item.ShortName + "\r").FontSize(6);

                        main_table.Rows[start_index + 3].Cells[10].Paragraphs[0].Append("Individual" + "\r").FontSize(6);
                        main_table.Rows[start_index + 3].Cells[11].Paragraphs[0].Append("Key" + "\r").FontSize(6);
                    }
                    #endregion

                    #region 填写纯度
                    List<Parameter> purities = anlysis.GetParametersByKeyStr(model.GDMS);
                    foreach (var item in purities)
                    {

                        main_table.Rows[start_index + 6].Cells[0].Paragraphs[0].Append(item.Characteristic + "\r").FontSize(6);

                        main_table.Rows[start_index + 6].Cells[5].Paragraphs[0].Append(item.Measurements[0]
                            .MeasurementValue + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[7].Paragraphs[0].Append(item.UnitOfMeasure + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[8].Paragraphs[0].Append(item.ShortName + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[10].Paragraphs[0].Append("Batch" + "\r").FontSize(6);
                        main_table.Rows[start_index + 6].Cells[11].Paragraphs[0].Append("Key" + "\r").FontSize(6);
                    }
                    #endregion

                    #region 填写含氧量
                    List<Parameter> vpis = anlysis.GetParametersByKeyStr(model.VPI);
                    foreach (var item in vpis)
                    {

                        main_table.Rows[start_index + 9].Cells[0].Paragraphs[0].Append(item.Characteristic + "\r").FontSize(6);

                        main_table.Rows[start_index + 9].Cells[5].Paragraphs[0].Append(item.Measurements[0]
                            .MeasurementValue + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[7].Paragraphs[0].Append(item.UnitOfMeasure + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[8].Paragraphs[0].Append(item.ShortName + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[10].Paragraphs[0].Append("Batch" + "\r").FontSize(6);
                        main_table.Rows[start_index + 9].Cells[11].Paragraphs[0].Append("Key" + "\r").FontSize(6);
                    }
                    #endregion

                    doc.Save();
                }
                File.Copy(temp, filePath, true);
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
