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
    public class BarCodeHelper:IBarCodeHelper
    {
        private string filename;
        private Helper helper;
        public BarCodeHelper()
        {
            helper = new Helper();
            
        }
        public string CreateBarCodeImage(string s)
        {
            filename = helper.GetTempFile($"temp{Guid.NewGuid().ToString()}.png");
            var barcode = new Barcode();
            barcode.Height = 80;
            barcode.Width = 600;
            barcode.StandardizeLabel = true;
            barcode.IncludeLabel = true;
            barcode.LabelPosition = LabelPositions.TOPLEFT;
            barcode.RotateFlipType = RotateFlipType.RotateNoneFlipNone;
            barcode.Alignment = AlignmentPositions.LEFT;
            Image image = barcode.Encode(TYPE.CODE128, s);
            FileStream fs = new FileStream(filename, FileMode.Create);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Close();
            return filename;
        }

    }
}
