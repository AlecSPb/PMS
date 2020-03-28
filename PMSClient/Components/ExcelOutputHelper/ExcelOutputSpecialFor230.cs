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
    public class ExcelOutputSpecialFor230 : ExcelOutputBasePage
    {

        public ExcelOutputSpecialFor230()
        {

        }
        //进度事件
        public event EventHandler<double> UpdateProgress;

        private void OnUpdateProgress(double progressValue)
        {
            UpdateProgress?.Invoke(this, progressValue);
        }

        private int year_start = 0; 
        private int month_start = 0; 
        private int year_end = 0; 
        private int month_end = 0;

        public void SetParameter(int y_s,int m_s,int y_e,int m_e)
        {
            year_start = y_s;
            month_start = m_s;
            year_end = y_e;
            month_end = m_e;
        }

        public override void Output()
        {

            excelFileName = excelFileName.Replace(".xlsx", $"{year_start}_{month_start}to{year_end}_{month_end}.xlsx");

            //计算进度
            double count_total = 0, count_current = 0;

            ResetParameters();
            using (var service = new OutputServiceClient())
            {
                recordCount = service.GetAll230DataByYearMonthCount(year_start, month_start, year_end, month_end);
                pageCount = GetPageCount();

                count_total = recordCount;


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
                    "Shipment Date",
                    "",
                    "Ave-1",
                    "Ave-2",
                    "Ave-3",
                    "Ave-4",
                    "Max-1",
                    "Max-2",
                    "Max-3",
                    "Max-4",
                    "Min-1",
                    "Min-2",
                    "Min-3",
                    "Min-4",
                    "Comp-1-1",
                    "Comp-1-2",
                    "Comp-1-3",
                    "Comp-1-4",
                    "Comp-2-1",
                    "Comp-2-2",
                    "Comp-2-3",
                    "Comp-2-4",
                    "Comp-3-1",
                    "Comp-3-2",
                    "Comp-3-3",
                    "Comp-3-4",
                    "Comp-4-1",
                    "Comp-4-2",
                    "Comp-4-3",
                    "Comp-4-4",
                    "Comp-5-1",
                    "Comp-5-2",
                    "Comp-5-3",
                    "Comp-5-4"
                };
                helper.AddRowTitle(titles);

                //插入数据行
                int rowIndex = 1;
                int s = 0, t = 0;
                while (pageIndex < pageCount)
                {
                    System.Diagnostics.Debug.WriteLine($"{pageIndex} ");
                    s = pageIndex * pageSize;
                    t = pageSize;

                    var pageData = service.GetAll230DataByYearMonth(s, t, year_start, month_start, year_end, month_end);
                    foreach (var item in pageData)
                    {
                        //进度累加,触发事件
                        count_current++;
                        double current_progress = count_current * 100 / count_total;
                        OnUpdateProgress(current_progress);


                        helper.CreateRow(rowIndex);
                        #region 写入数据行
                        //对XRF成分进行处理
                        helper.CreateAndSetCell(0, item.ProductID);
                        helper.CreateAndSetCell(1, item.Customer);
                        helper.CreateAndSetCell(2, item.Composition);
                        helper.CreateAndSetCell(3, item.CompositionAbbr);
                        helper.CreateAndSetCell(4, item.Dimension);
                        helper.CreateAndSetCell(5, item.DimensionActual);

                        helper.CreateAndSetCell(6, item.Weight);
                        helper.CreateAndSetCell(7, item.Density);
                        helper.CreateAndSetCell(8, item.Resistance);

                        helper.CreateAndSetCell(9, item.PlateType);
                        helper.CreateAndSetCell(10, item.PlateLot);

                        helper.CreateAndSetCell(11, item.BondCreateTime.ToShortDateString());
                        helper.CreateAndSetCell(12, item.WeldingRate.ToString());
                        helper.CreateAndSetCell(13, item.DeliveryCreateTime.ToShortDateString());
                        helper.CreateAndSetCell(14, "");


                        //XRF成分处理
                        string xrf = item.CompositionXRF;

                        if (xrf.StartsWith("No."))
                        {
                            XRFResult result = XRFCompositionAnalysis.Anlysis(xrf);


                            int col_index = 0;
                            const int start_col_index = 15;

                            //插入平均值
                            col_index = start_col_index;
                            if (result.Average.Count > 0)
                            {
                                foreach (var number in result.Average)
                                {
                                    helper.CreateAndSetCell(col_index, number.ToString("F2"));
                                    col_index++;
                                }
                            }
                            //插入最大值

                            col_index = start_col_index + 4;
                            if (result.Max.Count > 0)
                            {
                                foreach (var number in result.Max)
                                {
                                    helper.CreateAndSetCell(col_index, number.ToString("F2"));
                                    col_index++;
                                }
                            }
                            //插入最小值
                            col_index = start_col_index + 8;
                            if (result.Min.Count > 0)
                            {
                                foreach (var number in result.Min)
                                {
                                    helper.CreateAndSetCell(col_index, number.ToString("F2"));
                                    col_index++;
                                }
                            }
                            //插入具体的成分
                            int compo_count = 0;
                            if (result.Compostions.Count > 0)
                            {
                                foreach (var compo_line in result.Compostions)
                                {
                                    col_index = start_col_index + 12 + 4 * compo_count;
                                    if (compo_line.Count > 0)
                                    {
                                        foreach (var compo_col in compo_line)
                                        {
                                            helper.CreateAndSetCell(col_index,
                                                compo_col.ToString("F2"));
                                            col_index++;
                                        }
                                    }
                                    compo_count++;
                                }
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print(item.ProductID);
                            System.Diagnostics.Debug.Print(item.Composition);
                            System.Diagnostics.Debug.Print(item.CompositionXRF);
                        }


                        #endregion

                        rowIndex++;
                    }
                    pageIndex++;
                    //System.Threading.Thread.Sleep(200);
                }



                helper.Save(excelFileName);
                //PMSDialogService.Show($"{excelFileName}创建到桌面完毕,确定后自动打开");

                CheckOpenAfterCreate();
            }
        }


    }
}
