using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFMediaKit.DirectShow.Controls;

namespace CameraAndQRCode
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vce.VideoCaptureSource = (string)cbo.SelectedItem;

        }

        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {

            //vce. 怎么抓取高清的原始图像           
            RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)vce.ActualWidth,
                (int)vce.ActualHeight,
                96, 96, PixelFormats.Default);

            //为避免抓不全的情况，需要在Render之前调用Measure、Arrange
            //为避免Videovce显示不全，需要把
            //Videovce的Stretch="Fill"
            vce.Measure(vce.RenderSize);
            vce.Arrange(new Rect(vce.RenderSize));
            bmp.Render(vce);
            //这里需要创建一个流以便存储摄像头拍摄到的图片。
            //当然，可以使文件流，也可以使内存流。
            //BitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bmp));
            //encoder.Save(ms);
            vce.Pause();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbo.ItemsSource = MultimediaUtil.VideoInputNames;
            if (MultimediaUtil.VideoInputNames.Length>0)
            {
                cbo.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("未发现摄像头");
            }



        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            vce.Play();
        }
    }
}
