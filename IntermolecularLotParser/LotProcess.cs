using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Xceed.Words.NET;

namespace IntermolecularLotParser
{
    public class LotProcess
    {
        public LotProcess()
        {
            InputString = "";
            Lots = new List<LotModel>();
        }

        public string InputString { get; set; }

        public List<LotModel> Lots;

        public void Process()
        {
            string[] lines = InputString.Split(new string[] { "\n" },
                        StringSplitOptions.RemoveEmptyEntries);

            //整理有效行
            List<string> validLines = new List<string>();
            foreach (var line in lines)
            {
                if (IsValidLine(line))
                {
                    validLines.Add(line);
                    Debug.WriteLine(line);
                }
            }
            //解析到模型
            LotModel model = null;
            foreach (var line in validLines)
            {

                if (line.Contains("atomic"))
                {
                    if (model != null)
                    {
                        Lots.Add(model);
                    }

                    model = new LotModel();
                    model.Composition = line;
                    model.Items.Clear();
                    continue;
                }
                if (line.StartsWith("part#"))
                {
                    model.Group = line;
                    continue;
                }

                if (line.StartsWith("SN#"))
                {
                    model.Items.Add(line);
                    continue;
                }
            }

            if (model != null)
            {
                Lots.Add(model);
                model = null;
            }


            string fileName = "tmp.docx";
            DocX doc = DocX.Create(fileName);
            Table table = doc.AddTable(1,1);
            table.Alignment = Alignment.center;
            table.AutoFit = AutoFit.Window;
            

            int row = 0, col = 0;
            table.Rows[row].Cells[col].Paragraphs[0].Append("激光标刻内容集合");
            foreach (var item in Lots)
            {
                row++;
                table.InsertRow();
                Paragraph p = table.Rows[row].Cells[col].Paragraphs[0];
                p.AppendLine(item.Composition);
                p.AppendLine(item.Group);
                foreach (var target in item.Items)
                {
                    p.AppendLine(target);
                }
            }

            doc.InsertTable(table);

            doc.Save();

            System.Threading.Thread.Sleep(3000);
            if (File.Exists(fileName))
            {
                System.Diagnostics.Process.Start("tmp.docx");
            }
        }

        /// <summary>
        /// 检查每行是否是有效行
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsValidLine(string line)
        {
            if (line.Contains("atomic"))
            {
                return true;
            }

            string[] prefixList = { "part#", "SN#" };
            foreach (var prefix in prefixList)
            {
                if (line.StartsWith(prefix))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
