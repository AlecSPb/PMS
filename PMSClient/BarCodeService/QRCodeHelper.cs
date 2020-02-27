using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;
using System.Drawing.Design;

namespace PMSClient.BarCodeService
{
    public class QRCodeHelper
    {
        public Bitmap GetImage(string s)
        {
            QRCodeGenerator qrgenerator = new QRCodeGenerator();
            QRCodeData data = qrgenerator.CreateQrCode("200220-A-1", QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(data);
            Bitmap bitmap = qrcode.GetGraphic(20);

            return bitmap;
        }
    }
}
