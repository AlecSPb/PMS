using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocGenerator.DocModels;
using Novacode;

namespace DocGenerator
{
    public class GeneratorCOA : GeneratorBase, IDoc<Product>
    {
        public void Generate(string sourceFilePath, string targetFilePath, Product reportModel)
        {
            //复制文件
            CopyTemplate(sourceFilePath, targetFilePath);
            //写入数据到文件
            using (var doc = DocX.Load(targetFilePath))
            {
                doc.ReplaceText("[Customer]", reportModel.Customer ?? "");
                string lotNumber = (reportModel.CompositionAbbr ?? "") + "-" + (reportModel.ProductID ?? "");
                doc.ReplaceText("[ProductID]", lotNumber);
                doc.ReplaceText("[PO]", reportModel.PO ?? "");
                doc.ReplaceText("[COADate]", DateTime.Now.ToString("MM/dd/yyyy"));
                doc.ReplaceText("[Composition]", reportModel.Composition ?? "");
                doc.ReplaceText("[Size]", reportModel.Dimension ?? "");
                doc.ReplaceText("[Weight]", reportModel.Weight ?? "");
                doc.ReplaceText("[Density]", reportModel.Density ?? "");
                doc.ReplaceText("[Resistance]", reportModel.Resistance ?? "");
                doc.ReplaceText("[Dimension]", reportModel.Dimension ?? "");
                doc.ReplaceText("[DimensionActual]", reportModel.DimensionActual ?? "");
                doc.ReplaceText("[OrderDate]", reportModel.CreateTime.ToString("MM/dd/yyyy"));
                doc.ReplaceText("[CreateDate]", reportModel.CreateTime.ToString("MM/dd/yyyy"));

                //写入成分



                doc.Save();
            }
        }
    }
}
