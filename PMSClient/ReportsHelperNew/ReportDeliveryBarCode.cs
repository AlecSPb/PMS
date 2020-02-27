using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PMSClient.MainService;
using Novacode;

namespace PMSClient.ReportsHelperNew
{
    public class ReportDeliveryBarCode : ReportBase
    {
        private Guid id;
        public ReportDeliveryBarCode(Guid id)
        {
            this.id = id;
        }
        public override void Output()
        {
            ResetParameters();

            string source = Path.Combine(reportsFolder, "DeliverySheetBarCode.docx");
            string temp = Path.Combine(reportsFolder, "Temp", "DeliverySheetBarCode.docx");
            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                #region 字段
                using (var s = new DeliveryServiceClient())
                {
                    var list = s.GetDeliveryItemByDeliveryID(id);
                    foreach (var item in list)
                    {
                        InsertBarcodeSection(doc, item.ProductID);
                        //InsertBarcodeSection(doc, item.Composition);
                        InsertBarcodeSection(doc, item.Customer);
                        doc.InsertParagraph("----------------------");
                        //System.Threading.Thread.Sleep(1000);
                    }
                }
                #endregion
                doc.Save();
            }
            File.Copy(temp, wordFileName, true);
            PMSDialogService.Show("生成成功，即将打开");
            System.Diagnostics.Process.Start(wordFileName);

        }
        private void InsertBarcodeSection(DocX doc, string s)
        {
            var helper = new BarCodeService.BarCodeHelper();
            doc.InsertParagraph().Append($"{s}");
            System.Drawing.Image image_bar = helper.GetImage(s);
            System.Diagnostics.Debug.Print(s);
            string im = helper.SaveTemp(image_bar);
            Image image = doc.AddImage(im);
            //helper.Del();
            Picture pic = image.CreatePicture();
            var p = doc.InsertParagraph();
            p.AppendPicture(pic);
        }

    }
}
