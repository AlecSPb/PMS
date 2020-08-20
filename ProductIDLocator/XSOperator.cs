using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.IO;
using ProductIDLocator.MainService;

namespace ProductIDLocator
{
    public class XSOperator
    {
        private string filepath;
        public XSOperator()
        {
            filepath = "sample_data.xlsx";
            ProductIDs = new List<string>();
        }

        public List<string> ProductIDs { get; set; }

        private void ReadIDs()
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("文件不存在");
                return;
            }
            using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {

                        while (reader.Read())
                        {
                            string line = reader.GetString(0);
                            ProductIDs.Add(line);
                        }
                    } while (reader.NextResult());
                }
            }
            Console.WriteLine("读取完毕");
        }

        private void ShowIDs()
        {
            foreach (var id in ProductIDs)
            {
                Console.WriteLine(id);
            }
        }

        private void SearchDBByProductID()
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            using (var s = new RecordTestServiceClient())
            {
                foreach (var id in ProductIDs)
                {
                    if (id == null) continue;
                    var rt = s.GetRecordTestByProductID(id.Trim()).FirstOrDefault();
                    if (rt != null)
                    {
                        sb.Append(id);
                        sb.Append(",");
                        sb.Append(rt.ProductID);
                        sb.Append(",");
                        sb.Append(rt.Composition);
                        sb.Append(",");
                        sb.Append(rt.CompositionAbbr);
                        sb.Append(",");
                        sb.Append(rt.Customer);
                        sb.Append(",");
                        sb.Append(rt.PO);
                        sb.Append(",");
                        sb.Append(rt.PMINumber);
                        sb.Append(",");
                        sb.Append(rt.ProductID);
                        sb.Append("-");
                        sb.Append(rt.Composition);
                        sb.AppendLine();
                    }
                    else
                    {
                        sb.AppendLine($"{id}, , , , , ");
                    }
                }
            }
            File.WriteAllText("data_result.csv", sb.ToString());
            Console.WriteLine("写入文件结束");
        }
        public void Process()
        {
            ReadIDs();
            SearchDBByProductID();
            ShowIDs();
        }
    }
}
