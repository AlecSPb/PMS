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



                doc.Save();
            }
        }
    }
}
