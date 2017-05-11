using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelLibrary.SpreadSheet;
using OfficeOpenXml;


namespace ExcelLibraryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExcelLibrary();
            string filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "order.xlsx");
            using (ExcelPackage excel = new ExcelPackage(
                new System.IO.FileInfo(filename)))
            {
                var ws = excel.Workbook.Worksheets.Add("订单");
                ws.Cells[1, 1].Value = "hello";

                ws.Column(1).AutoFit();
                excel.Save();
            }





            Console.WriteLine("OK");
            Console.Read();
        }

        private static void ExcelLibrary()
        {
            Workbook workbook = new Workbook();
            Worksheet worksheet = new Worksheet("订单");
            worksheet.Cells[0, 0] = new Cell("ID");
            worksheet.Cells[0, 1] = new Cell("Name");
            worksheet.Cells[0, 2] = new Cell("Price");

            worksheet.Cells.ColumnWidth[0] = 3200;
            worksheet.Cells.ColumnWidth[1] = 6400;
            worksheet.Cells.ColumnWidth[2] = 3200;

            for (int i = 0; i < 10; i++)
            {
                worksheet.Cells[1 + i, 0] = new Cell(i);
                worksheet.Cells[1 + i, 1] = new Cell("Product " + i);
                worksheet.Cells[1 + i, 2] = new Cell(i * 100);

            }
            workbook.Worksheets.Add(worksheet);
            string filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "order.xls");
            workbook.Save(filename);
        }
    }
}
