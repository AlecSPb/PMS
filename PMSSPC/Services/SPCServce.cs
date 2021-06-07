using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PMSSPC.Models;

namespace PMSSPC.Services
{
    public class SPCServce
    {
        private string conStr = "server=192.168.16.254;database=PMS;uid=sa;pwd=newlifechou;";


        public List<RecordTestModel> GetRecordTestModels(string composition, string start, string end)
        {
            IDbConnection conn = new SqlConnection(conStr);

            string sql = "select id,productid,composition,density,dimensionactual,resistance,weight,createtime from recordtests " +
                "where composition like '%'+@compo+'%' and [state]!='作废' and dimension like '%230%'" +
                "and (createtime between @start and @end) and composition not like '%F%'";

            var testResults = conn.Query<RecordTestModel>(sql, new { @compo = composition, @start = start, @end = end });

            conn.Close();

            return testResults.ToList();
        }

        public List<SPCDataItem> GetCleanedSPCDataItemDensity(string composition, string start, string end)
        {
            var data = GetRecordTestModels(composition, start, end);

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
            var data = GetRecordTestModels(composition, start, end);

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
    }
}
