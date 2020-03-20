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
                        InsertBarcodeSection(doc, item.Customer);
                        InsertBarcodeSection(doc, item.Composition);

                        doc.InsertParagraph();
                    }
                }
                #endregion
                doc.Save();
            }
            File.Copy(temp, wordFileName, true);
            PMSDialogService.Show("生成成功，即将打开");
            System.Diagnostics.Process.Start(wordFileName);

        }

        private BarCodeService.IBarCodeHelper helper = new BarCodeService.BarCodeHelper();

        private void InsertBarcodeSection(DocX doc, string s)
        {
            string im = helper.CreateQRCodeImage(s);
            Image image = doc.AddImage(im);
            Picture pic = image.CreatePicture();
            var p = doc.InsertParagraph();
            p.AppendPicture(pic);
        }

    }
}
