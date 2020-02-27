using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;
using System.Drawing.Design;
using System.IO;

namespace PMSClient.BarCodeService
{
    public class QRCodeHelper:IBarCodeHelper
    {
        private string filename;
        private Helper helper;
        public QRCodeHelper()
        {
            helper = new Helper();
        }

        public string CreateBarCodeImage(string s)
        {
            filename = helper.GetTempFile($"temp{Guid.NewGuid().ToString()}.png");
            QRCodeGenerator qrgenerator = new QRCodeGenerator();
            QRCodeData data = qrgenerator.CreateQrCode(s, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(data);
            Bitmap bitmap = qrcode.GetGraphic(20);
            FileStream fs = new FileStream(filename, FileMode.Create);
            bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Close();

            return filename;
        }
    }
}
