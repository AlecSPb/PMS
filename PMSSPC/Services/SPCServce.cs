using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PMSSPC.Models;
using System.Text.RegularExpressions;


namespace PMSSPC.Services
{
    public class SPCServce
    {
        private string conStr = "server=192.168.16.254;database=PMS;uid=sa;pwd=newlifechou;";


        public List<RecordTestModel> GetRecordTestModels(string composition, string start, string end)
        {
            IDbConnection conn = new SqlConnection(conStr);

            string sql = "select id,productid,composition,density,dimension,dimensionactual,resistance,weight,createtime,compositionxrf " +
                "from recordtests " +
                "where composition like '%'+@compo+'%' and [state]!='作废'" +
                "and (createtime between @start and @end)";

            var testResults = conn.Query<RecordTestModel>(sql, new { @compo = composition, @start = start, @end = end });

            conn.Close();

            return testResults.ToList();
        }

        public List<RecordTestModel> GetRecordTestModelsFilter(string composition, string start, string end)
        {
            var result = GetRecordTestModels(composition, start, end);
            if (composition == "Ge22.22Sb22.22Te55.56")
            {
                return result.Where(i => i.Composition.Contains(composition)).ToList();
            }
            else if (composition == "Se51.0As30.6Ge12.7Si5.7")
            {
                return result.Where(i => i.Composition.Contains(composition) 
                && i.Dimension.Contains("440") 
                && double.Parse(i.Weight) > 4000).ToList();
            }
            else
            {
                return result.Where(i => i.Dimension.Contains("230") && !i.Composition.Contains("F")).ToList();
            }
        }


        public List<SPCDataItem> GetCleanedSPCDataItemDensity(string composition, string start, string end)
        {
            var data = GetRecordTestModelsFilter(composition, start, end);

            var data_cleaned = data.Where(i => i.Density > 0).OrderBy(i => i.ProductID).ToList();

            List<SPCDataItem> items = new List<SPCDataItem>();

            data_cleaned.ForEach(i =>
            {
                items.Add(new SPCDataItem { ProductID = i.ProductID, Composition = i.Composition, Value = i.Density, CreateTime = i.CreateTime });
            });

            return items;
        }

        public List<SPCDataItem> GetCleanedSPCDataItemWeight(string composition, string start, string end)
        {
            var data = GetRecordTestModelsFilter(composition, start, end);

            var data_cleaned = data.Where(i => !string.IsNullOrEmpty(i.Weight)).OrderBy(i => i.ProductID).ToList();

            List<SPCDataItem> items = new List<SPCDataItem>();

            data_cleaned.ForEach(i =>
            {
                var newItem = new SPCDataItem
                {
                    ProductID = i.ProductID,
                    Composition = i.Composition,
                    CreateTime = i.CreateTime
                };

                double weight = 0;
                if (!double.TryParse(i.Weight, out weight))
                {
                    weight = 0;
                }
                if (weight != 0)
                {
                    newItem.Value = weight;
                    items.Add(newItem);
                }
            });

            return items;
        }


        public List<SPCDataItem> GetCleanedSPCDataItemDiameter(string compositon, string start, string end)
        {
            var data = GetRecordTestModelsFilter(compositon, start, end);
            var data_cleaned = data.Where(i => !string.IsNullOrEmpty(i.DimensionActual)).OrderBy(i => i.ProductID).ToList();
            List<SPCDataItem> items = new List<SPCDataItem>();



            data_cleaned.ForEach(i =>
            {
                var newItem = new SPCDataItem
                {
                    ProductID = i.ProductID,
                    Composition = i.Composition,
                    CreateTime = i.CreateTime
                };

                double diameter = GetDimension(i.DimensionActual, 0);

                if (diameter != 0)
                {
                    newItem.Value = diameter;
                    items.Add(newItem);
                }
            });
            return items;
        }
        public List<SPCDataItem> GetCleanedSPCDataItemThickness(string compositon, string start, string end)
        {
            var data = GetRecordTestModelsFilter(compositon, start, end);
            var data_cleaned = data.Where(i => !string.IsNullOrEmpty(i.DimensionActual)).OrderBy(i => i.ProductID).ToList();
            List<SPCDataItem> items = new List<SPCDataItem>();



            data_cleaned.ForEach(i =>
            {
                var newItem = new SPCDataItem
                {
                    ProductID = i.ProductID,
                    Composition = i.Composition,
                    CreateTime = i.CreateTime
                };

                double thickness = GetDimension(i.DimensionActual, 1);

                if (thickness != 0)
                {
                    newItem.Value = thickness;
                    items.Add(newItem);
                }
            });
            return items;
        }
        private double GetDimension(string dimension, int number_sequence = 0)
        {
            if (string.IsNullOrEmpty(dimension)) return 0;
            string number_patter = @"(\d+(\.\d+))";

            var results = Regex.Matches(dimension, number_patter, RegexOptions.IgnoreCase);
            if (results.Count >= 2)
            {
                string number = "0";
                if (number_sequence == 0)
                {
                    number = results[0].Groups[0].Value;
                }
                else
                {
                    number = results[number_sequence].Groups[0].Value;
                }

                double.TryParse(number, out double result);
                return result;

            }
            else
            {
                return 0;
            }

        }


        public List<SPCDataItem> GetCleanedSPCDataItemCompositionXRF(string compositon, string start, string end, int i_compo = 1)
        {
            var data = GetRecordTestModelsFilter(compositon, start, end);
            var data_cleaned = data.Where(i => !string.IsNullOrEmpty(i.CompositionXRF)).OrderBy(i => i.ProductID).ToList();
            List<SPCDataItem> items = new List<SPCDataItem>();



            data_cleaned.ForEach(i =>
            {
                var newItem = new SPCDataItem
                {
                    ProductID = i.ProductID,
                    Composition = i.Composition,
                    CreateTime = i.CreateTime
                };


                double compo = GetCompoAverageAt(i.CompositionXRF, i_compo);

                if (compo != 0)
                {
                    newItem.Value = compo;
                    items.Add(newItem);
                }

            });
            return items;
        }


        public double GetCompoAverageAt(string xrf, int i_compo = 1)
        {
            if (!string.IsNullOrEmpty(xrf) && !xrf.Contains("无"))
            {
                string[] compositonRows = xrf.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (compositonRows.Count() >= 6)
                {
                    string avg_row = compositonRows[6];

                    string[] columns = avg_row.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (columns.Count() > i_compo)
                    {
                        double.TryParse(columns[i_compo], out double result);
                        return result;
                    }
                }
            }
            return 0;
        }



    }
}
