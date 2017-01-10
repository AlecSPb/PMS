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

        }
    }
}
