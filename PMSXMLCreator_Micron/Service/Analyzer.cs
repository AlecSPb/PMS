using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSXMLCreator_Micron.Model;
using System.IO;

namespace PMSXMLCreator_Micron.Service
{
    /// <summary>
    /// 文本解析器
    /// </summary>
    public class Analyzer
    {
        public Micon_COA Resolve(string format_txt)
        {
            var coa = new Micon_COA();

            string[] lines = format_txt.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                //去掉注释

                //解析header,以$开头
                if (line.StartsWith("$"))
                {
                    string s = line.Remove(0, 1);//去掉前缀
                    string[] fields = s.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                    var header_temp = new BasicInfoField();
                    if (fields.Count() >= 2)
                    {
                        header_temp.FiledName = fields[0] ?? "";
                        header_temp.FiledValue = fields[1] ?? "";
                    }

                    coa.Header.Add(header_temp);
                }

                //解析InspectionItem,以*开头
                if (line.StartsWith("*"))
                {
                    string s = line.Remove(0, 1);//去掉前缀
                    string[] fields = s.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                    var inspectionItem_temp = new InspectionItem();
                    inspectionItem_temp.ItemName = fields[0] ?? "";
                    if (fields.Count() >= 5)
                    {
                        inspectionItem_temp.ResultItems.Add(new ResultItem { ResultName = "Unit", Value = fields[1] ?? "" });
                        inspectionItem_temp.ResultItems.Add(new ResultItem { ResultName = "Specification", Value = fields[2] ?? "" });
                        inspectionItem_temp.ResultItems.Add(new ResultItem { ResultName = "DetectionLimit", Value = fields[3] ?? "" });
                        inspectionItem_temp.ResultItems.Add(new ResultItem { ResultName = "InspectionValue", Value = fields[4] ?? "" });
                    }

                    coa.InspectionItems.Add(inspectionItem_temp);
                }
            }

            return coa;
        }









    }
}
