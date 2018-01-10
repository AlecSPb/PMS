using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInput.BasicService;
using CsvHelper;
using System.IO;
using DataInput.Model;

namespace DataInput
{
    public class CompoundBatchOperate
    {
        private string filepath;
        public CompoundBatchOperate()
        {
            filepath = Path.Combine(Environment.CurrentDirectory, "RawData", "温度熔点CSV.csv");
        }
        public void ReadAll()
        {
            try
            {
                StreamReader sr = new StreamReader(filepath);
                var csv = new CsvReader(sr);
                var records = csv.GetRecords<CompoundCsvModel>();

                using (var service = new CompoundServiceClient())
                {
                    List<DcBDCompound> compounds = new List<DcBDCompound>();
                    compounds.Clear();
                    foreach (var item in records)
                    {
                        DcBDCompound cc = new DcBDCompound();
                        cc.ID = Guid.NewGuid();
                        cc.CreateTime = DateTime.Now;
                        cc.Creator = "xs.zhou";
                        cc.InformationSource = "三杰";
                        cc.MaterialName = item.MaterialName;
                        cc.MeltingPoint = item.MeltingPoint;
                        cc.SpecialProperty = "无";
                        cc.BoilingPoint = "";
                        cc.State = "正常";
                        cc.Remark = "";
                        cc.Density = WhichDensity(cc.MaterialName);
                        compounds.Add(cc);
                    }

                    foreach (var item in compounds)
                    {
                        service.AddCompound(item);
                    }



                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        private double WhichDensity(string materialName)
        {
            if (materialName.Contains("Cu")
                &&
                materialName.Contains("In")
                &&
                materialName.Contains("Ga")
                &&
                materialName.Contains("Se")
                )
            {
                return 5.75;
            }

            if (materialName.Contains("Se")
                &&
                materialName.Contains("As")
                &&
                materialName.Contains("Ge")
                &&
                materialName.Contains("Si")
                )
            {
                return 4.3;
            }

            if (materialName.Contains("Se")
                &&
                materialName.Contains("As")
                &&
                materialName.Contains("Ge")
                &&
                !materialName.Contains("Si")
                )
            {
                return 4.5;
            }
            return 0;
        }
    }
}
