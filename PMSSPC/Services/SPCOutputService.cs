using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.SS.UserModel;
using PMSSPC.Models;
using System.IO;
using NPOI.XSSF.UserModel;

namespace PMSSPC.Services
{
    public class SPCOutputService
    {
        public void Output(SPCModel model)
        {
            if (model == null) return;
            string foldername = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename = $"{model.Start.ToString("yyMMdd")}-{model.End.ToString("yyMMdd")}-{model.Items[0].Composition}-{model.SPCType}.xlsx";
            string fullname = Path.Combine(foldername, filename);
            using (var fs = new FileStream(fullname, FileMode.Create))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Sheet1");
                int rowIndex = 0;
                int columnIndex = 0;
                #region title
                IRow row = sheet.CreateRow(rowIndex);
                row.CreateCell(columnIndex).SetCellValue("开始日期");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("结束日期");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("成分");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("分析类型");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("单位");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("UCL");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("CL");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("LCL");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("USL");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("SL");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("LSL");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("Sigma");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("Cp");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("Cpk");

                rowIndex++;
                row = sheet.CreateRow(rowIndex);
                
                columnIndex = 0;
                row.CreateCell(columnIndex).SetCellValue(model.Start.ToShortDateString());
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.End.ToShortDateString());
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.Items[0].Composition);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.SPCType);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.Unit);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.UCL);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.CL);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.LCL);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.USL);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.SL);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.LSL);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.Sigma);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.Cp);
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue(model.Cpk);
                #endregion

                rowIndex++;
                row = sheet.CreateRow(rowIndex);

                columnIndex = 0;
                row.CreateCell(columnIndex).SetCellValue("ProductID");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("Composition");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("Value");
                columnIndex++;
                row.CreateCell(columnIndex).SetCellValue("CreatedTime");
                foreach (var item in model.Items)
                {
                    rowIndex++;
                    row = sheet.CreateRow(rowIndex);
                    columnIndex = 0;
                    row.CreateCell(columnIndex).SetCellValue(item.ProductID);
                    columnIndex++;
                    row.CreateCell(columnIndex).SetCellValue(item.Composition);
                    columnIndex++;
                    row.CreateCell(columnIndex).SetCellValue(item.Value);
                    columnIndex++;
                    row.CreateCell(columnIndex).SetCellValue(item.CreateTime.ToShortDateString());

                }



                workbook.Write(fs);
            }

            System.Diagnostics.Process.Start(fullname);
        }
    }
}
