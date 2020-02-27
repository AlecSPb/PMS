using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Design;
using BarcodeLib;
using System.IO;

namespace PMSClient.BarCodeService
{
    public class BarCodeHelper
    {
        private string filename;
        public BarCodeHelper()
        {
            filename = $"temp{Guid.NewGuid().ToString()}.png";
        }
        public Image GetImage(string s)
        {
            var barcode = new Barcode();
            barcode.Height = 60;
            barcode.Width = 500;
            Image image = barcode.Encode(TYPE.CODE128, s);
            return image;
        }

        public string SaveTemp(Image image)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Close();
            return filename;
        }

        public void Del()
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
    }
}
