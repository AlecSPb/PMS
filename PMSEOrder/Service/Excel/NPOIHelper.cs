using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace PMSEOrder.Service.Excel
{
    public class NPOIHelper
    {
        private FileStream fs;
        private IWorkbook workbook2007;
        private ISheet currentSheet;
        private IRow currentRow;
        private ICell currentCell;

        private ICellStyle cellStyle;
        private IDataFormat format;
        /// <summary>
        /// 初始化一个workbook2007
        /// </summary>
        /// <param name="sheetName"></param>
        public void CreateNew(string sheetName)
        {
            workbook2007 = new XSSFWorkbook();
            currentSheet = workbook2007.CreateSheet(sheetName);
        }
        /// <summary>
        /// 创建行
        /// </summary>
        /// <param name="rowIndex"></param>
        public void CreateRow(int rowIndex)
        {
            currentRow = currentSheet.CreateRow(rowIndex);
        }
        /// <summary>
        /// 创建单元格并赋值
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="value"></param>
        public void CreateAndSetCell(int cellIndex, string value)
        {
            currentCell = currentRow.CreateCell(cellIndex);
            currentCell.SetCellValue(value);
        }
        public void CreateAndSetCell(int cellIndex, double value)
        {
            currentCell = currentRow.CreateCell(cellIndex);
            currentCell.SetCellValue(value);
        }

        public void CreateAndSetCell(int cellIndex, double value, string formatString)
        {
            currentCell = currentRow.CreateCell(cellIndex);
            currentCell.SetCellValue(value);

            ICellStyle cellStyle = workbook2007.CreateCellStyle();
            IDataFormat format = workbook2007.CreateDataFormat();
            cellStyle.DataFormat = format.GetFormat(formatString);
            currentCell.CellStyle = cellStyle;
        }

        /// <summary>
        /// 插入标题行
        /// </summary>
        /// <param name="titles"></param>
        public void AddRowTitle(string[] titles)
        {
            CreateRow(0);
            for (int i = 0; i < titles.Length; i++)
            {
                CreateAndSetCell(i, titles[i]);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            fs = new FileStream(fileName, FileMode.Create);
            if (workbook2007 != null)
            {
                workbook2007.Write(fs);
                workbook2007.Close();
                fs.Close();
            }
        }
    }
}
