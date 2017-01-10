using DocGenerator.DocModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;

namespace DocGenerator
{
    public class GeneratorMaterialOrder : GeneratorBase,IDoc<MaterialOrder>
    {
        public void Generate(string sourceFilePath, string targetFilePath, MaterialOrder reportModel)
        {
            //复制文件
            CopyTemplate(sourceFilePath, targetFilePath);
            //写入数据到文件
            using (var doc=DocX.Load(targetFilePath))
            {
                doc.ReplaceText("[OrderPO]", reportModel.OrderPO ?? "");
                doc.ReplaceText("[SupplierName]", reportModel.Supplier??"");
                doc.ReplaceText("[SupplierReceiver]", reportModel.SupplierReceiver ?? "");
                doc.ReplaceText("[SupplierEmail]", reportModel.SupplierEmail ?? "");
                doc.ReplaceText("[SupplierAddress]", reportModel.SupplierAddress ?? "");
                doc.ReplaceText("[OrderDate]", reportModel.CreateTime.ToString("MM/dd/yyyy"));

                //插入成分表格
                doc.Save();
            }
        }
    }
}
