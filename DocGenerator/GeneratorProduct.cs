using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;//DocX
using DocGenerator.DocModels;

namespace DocGenerator
{
    public class GeneratorProduct : GeneratorBase,IDoc<Product>
    {
        public void Generate(string sourceFilePath, string targetFilePath, Product reportModel)
        {
            //复制文件
            CopyTemplate(sourceFilePath, targetFilePath);
            //写入数据到文件
            using (var doc=DocX.Load(targetFilePath))
            {
                doc.ReplaceText("[Composition]", reportModel.Composition ?? "");
                doc.ReplaceText("[Customer]", reportModel.Customer ?? "");
                doc.ReplaceText("[PO]", reportModel.PO ?? "");
                doc.ReplaceText("[CreateTime]", reportModel.CreateTime.ToShortDateString());

                doc.ReplaceText("[ProductID]", reportModel.ProductID ?? "");
                doc.ReplaceText("[Weight]", reportModel.Weight ?? "");
                doc.ReplaceText("[Density]", reportModel.Density ?? "");
                doc.ReplaceText("[Resistance]", reportModel.Resistance ?? "");
                doc.ReplaceText("[Remark]", reportModel.Remark ?? "");
                doc.ReplaceText("[Dimension]", reportModel.Dimension ?? "");
                doc.ReplaceText("[DimensionActual]", reportModel.DimensionActual ?? "");


                doc.Save();
            }
        }
    }
}
