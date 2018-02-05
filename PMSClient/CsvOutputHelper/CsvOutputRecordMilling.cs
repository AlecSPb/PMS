using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using System.IO;
using CsvHelper;

namespace PMSClient.CsvOutputHelper
{
    class CsvOutputRecordMilling:CsvOutputBase
    {
        public override void Output()
        {
            int pageIndex = 1;
            int PageSize = 20;
            int recordCount = 0;
            using (var service = new RecordMillingServiceClient())
            {
                recordCount = service.GetRecordMillingCountByVHPPlanLot("","");
            }

            int pageCount = recordCount / PageSize + (recordCount % PageSize == 0 ? 0 : 1);

            int skip = 0, take = 0;
            take = PageSize;
            skip = (pageIndex - 1) * PageSize;

            string outputfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                , "导出数据-制粉" + DateTime.Now.ToString("yyyyMMddmmhhss") + ".csv");

            StreamWriter sw = new StreamWriter(new FileStream(outputfile, FileMode.Append), System.Text.Encoding.GetEncoding("GB2312"));

            //var csv = new CsvWriter(sw);
            //csv.WriteHeader()


            string titleString = "热压编号,成分,内部编号,温度,湿度,制粉方式,粒径,制粉时长,进料,出料,余料,损失率,备注,创建时间,创建者";
            sw.WriteLine(titleString);

            using (var service = new RecordMillingServiceClient())
            {
                try
                {
                    string outputString = "";
                    while (pageIndex <= pageCount)
                    {
                        var models = service.GetRecordMillingsByVHPPlanLot(skip, take, "", "");
                        outputString = PMSOuputHelper.GetRecordMillingOupput(models);
                        sw.Write(outputString.ToString());
                        sw.Flush();

                        pageIndex++;
                        skip = (pageIndex - 1) * PageSize;
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
            sw.Close();
        }
    }
}
