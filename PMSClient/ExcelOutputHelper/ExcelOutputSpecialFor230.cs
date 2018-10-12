using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using PMSClient.OutputService;
using PMSClient.Helpers;
using System.IO;


namespace PMSClient.ExcelOutputHelper
{
    public class ExcelOutputSpecialFor230 : ExcelOutputBase
    {

        public ExcelOutputSpecialFor230()
        {

        }

        public override void Output()
        {
            ResetParameters();

            using (var service = new OutputServiceClient())
            {
                recordCount = service.GetAll230DataCount();
                pageCount = GetPageCount();

                NPOIHelper helper = new NPOIHelper();
                helper.CreateNew(sheetName);

                //插入标题行

                string[] titles = {"Target ID",
                    "Customer",
                    "Composition",
                    "Abbr",
                    "Dimension",
                    "Actual Dimension",
                    "Weight",
                    "Density",
                    "Resistance",
                    "Plate Type",
                    "Plate Number",
                    "Bonding Date",
                    "Bonding Rate",
                    "Delivery Date",
                    "Ave-1",
                    "Ave-2",
                    "Ave-3",
                    "Ave-4",
                    "Max-1",
                    "Max-2",
                    "Max-3",
                    "Min-4",
                    "Min-1",
                    "Min-2",
                    "Min-3",
                    "Min-4"};
                helper.AddRowTitle(titles);

                //插入数据行
                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    System.Diagnostics.Debug.WriteLine($"{pageIndex} ");
                    s = pageIndex * pageSize;
                    t = pageSize;

                    var pageData = service.GetAll230Data(s, t);
                    foreach (var item in pageData)
                    {
                        helper.CreateRow(rowIndex);
                        #region 写入数据行
                        //对XRF成分进行处理
                        helper.CreateAndSetCell(0, item.Delivery?.ProductID);
                        helper.CreateAndSetCell(1, item.Delivery?.Customer);
                        helper.CreateAndSetCell(2, item.Delivery?.Composition);
                        helper.CreateAndSetCell(3, item.Test?.CompositionAbbr);
                        helper.CreateAndSetCell(4, item.Delivery?.Dimension);
                        helper.CreateAndSetCell(5, item.Test?.DimensionActual);

                        helper.CreateAndSetCell(6, item.Test?.Weight);
                        helper.CreateAndSetCell(7, item.Test?.Density);
                        helper.CreateAndSetCell(8, item.Test?.Resistance);

                        helper.CreateAndSetCell(9, item.Bond?.PlateType);
                        helper.CreateAndSetCell(10, item.Bond?.PlateLot);

                        helper.CreateAndSetCell(11, item.Bond?.CreateTime.ToShortDateString());
                        helper.CreateAndSetCell(12, item.Bond?.WeldingRate.ToString());
                        helper.CreateAndSetCell(13, item.Delivery?.CreateTime.ToShortDateString());


                        //XRF成分处理
                        string xrf = item.Test?.CompositionXRF;

                        if (xrf.StartsWith("No."))
                        {
                            XRFResult result = XRFCompositionAnalysis.Anlysis(xrf);


                            int col_index = 0;
                            const int start_col_index = 14;

                            col_index = start_col_index;
                            if (result.Average.Count > 0)
                            {
                                foreach (var number in result.Average)
                                {
                                    helper.CreateAndSetCell(col_index, number.ToString("F2"));
                                    col_index++;
                                }
                            }

                            col_index = start_col_index + 4;
                            if (result.Max.Count > 0)
                            {
                                foreach (var number in result.Max)
                                {
                                    helper.CreateAndSetCell(col_index, number.ToString("F2"));
                                    col_index++;
                                }
                            }
                            col_index = start_col_index + 8;
                            if (result.Min.Count > 0)
                            {
                                foreach (var number in result.Min)
                                {
                                    helper.CreateAndSetCell(col_index, number.ToString("F2"));
                                    col_index++;
                                }
                            }

                        }



                        #endregion

                        rowIndex++;
                    }
                    pageIndex++;

                }



                helper.Save(excelFileName);
                PMSDialogService.Show($"{excelFileName}创建到桌面完毕,确定后自动打开");

                if (File.Exists(excelFileName))
                {
                    System.Diagnostics.Process.Start(excelFileName);
                }
            }
        }




    }
}
