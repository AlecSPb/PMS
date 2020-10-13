using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PMSClient.Components.CscanImageProcess
{
    public class ImageManager
    {
        private ImageService service = new ImageService();

        public void ViewImageInWindow(string productid)
        {
            var dialog = new ImageTypeSelectionDialog();
            dialog.ShowDialog();


            if (dialog.DialogResult == false)
            {
                return;
            }
            var result = GetImage(productid, dialog.SelectedImageType);
            if (result.IsFound)
            {
                var win = new FTPImageShow();
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri(result.ImagePath);
                bmp.EndInit();
                win.MainImage.Source = bmp;
                win.MainInfo.Content = $"{result.InfoMessage}-本地地址{result.ImagePath}";
                win.CurrentFile = result.ImagePath;
                win.Show();
            }
            else
            {
                PMSDialogService.ShowWarning(result.InfoMessage);
            }
        }

        public ImageFoundResult GetImage(string productid, ImageType imageType)
        {
            ImageFoundResult result = new ImageFoundResult();

            switch (imageType)
            {
                case ImageType.Bonding:
                    result = service.FindBondingImage(productid);
                    break;
                case ImageType.Target:
                    result = service.FindTargetImage(productid);
                    break;
                default:
                    break;
            }
            return result;
        }

    }
}