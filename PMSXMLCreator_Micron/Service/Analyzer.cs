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




            return null;
        }









    }
}
