using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Media.Imaging;

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

        public string CreateQRCodeImage(string s)
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

        public BitmapImage CreateQRCodeBmp(string s)
        {
            QRCodeGenerator qrgenerator = new QRCodeGenerator();
            QRCodeData data = qrgenerator.CreateQrCode(s, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(data);
            Bitmap bitmap = qrcode.GetGraphic(20);

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = ms;
            bmp.EndInit();
            return bmp;

        }
    }
}
