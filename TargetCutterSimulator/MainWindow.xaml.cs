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
using IO = System.IO;

namespace TargetCutterSimulator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }


        private void Initialize()
        {
            this.Title = "靶材切割设计器";
        }

        private const double ratio = 2.0;
        private const double normal_thickness = 3.0;
        private const double select_thickness = 4.0;

        private void AddEllipse(double diameter)
        {
            Ellipse circle = new Ellipse();
            circle.Height = diameter * ratio;
            circle.Width = diameter * ratio;
            circle.Fill = Brushes.Transparent;
            circle.StrokeThickness = normal_thickness;
            circle.Stroke = Brushes.Blue;
            circle.MouseDown += Shape_MouseDown;
            circle.MouseMove += Shape_MouseMove;
            circle.MouseUp += Shape_MouseUp;

            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform());
            transformGroup.Children.Add(new TranslateTransform());
            circle.RenderTransform = transformGroup;

            MainCanvas.Children.Add(circle);
        }

        private void AddRectangle(double width, double height)
        {
            Rectangle rect = new Rectangle();
            rect.Height = height * ratio;
            rect.Width = width * ratio;
            rect.Fill = Brushes.Transparent;
            rect.StrokeThickness = normal_thickness;
            rect.Stroke = Brushes.Blue;
            rect.MouseDown += Shape_MouseDown;
            rect.MouseMove += Shape_MouseMove;
            rect.MouseUp += Shape_MouseUp;

            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform());
            transformGroup.Children.Add(new TranslateTransform());
            rect.RenderTransform = transformGroup;

            MainCanvas.Children.Add(rect);
        }

        private void Shape_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                currentShape = e.OriginalSource as Shape;
                previousPoint = e.GetPosition(currentShape);
                currentShape.StrokeThickness = select_thickness;
            }
        }

        private Point previousPoint = new Point(0, 0);
        private Shape currentShape = null;

        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && currentShape != null)
            {
                Point position = e.GetPosition(currentShape);

                TranslateTransform translate = (currentShape.RenderTransform as TransformGroup)
                                                        .Children[1] as TranslateTransform;
                translate.X += position.X - this.previousPoint.X;
                translate.Y += position.Y - this.previousPoint.Y;
                System.Diagnostics.Debug.WriteLine($"{position.X},{position.Y}");
            }
        }

        private void BtnAddCircle_Click(object sender, RoutedEventArgs e)
        {

            if (double.TryParse(TxtDiameter.Text.Trim(), out double diameter)
                            && diameter > 0)
            {
                AddEllipse(diameter);
            }
        }

        private void BtnAddRectangle_Click(object sender, RoutedEventArgs e)
        {
            bool result1 = double.TryParse(TxtWidth.Text.Trim(), out double width);
            bool result2 = double.TryParse(TxtHeight.Text.Trim(), out double height);
            if (result1 && result2 && width > 0 && height > 0)
            {
                AddRectangle(width, height);

            }
        }

        private void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (currentShape != null)
            {
                currentShape.StrokeThickness = normal_thickness;

            }
            //currentShape = null;
            System.Diagnostics.Debug.WriteLine("Up");
        }

        private void CmCopy_Click(object sender, RoutedEventArgs e)
        {
            if (currentShape != null)
            {
                if (currentShape.GetType() == typeof(Rectangle))
                {
                    AddRectangle(currentShape.Width / ratio, currentShape.Height / ratio);
                }
                else if (currentShape.GetType() == typeof(Ellipse))
                {
                    AddEllipse(currentShape.Width / ratio);
                }
            }
            e.Handled = true;
        }

        private void CmDelete_Click(object sender, RoutedEventArgs e)
        {
            if (currentShape != null)
            {
                MainCanvas.Children.Remove(currentShape);
                currentShape = null;
            }
            e.Handled = true;
        }

        private string targetFolder = IO.Path.Combine(Environment.CurrentDirectory, "SaveAs");
        private void BtnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Size size = new Size(this.Width, this.Height);
                MainCanvas.Measure(size);

                var rtb = new RenderTargetBitmap(
                    (int)MainCanvas.ActualWidth,
                    (int)MainCanvas.ActualHeight,
                    96,
                    96,
                    PixelFormats.Pbgra32
                    );

                rtb.Render(MainCanvas);

                var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
                enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(rtb));

                string fileName = IO.Path.Combine(targetFolder, $"TC_{DateTime.Now.ToString("yyyyMMddHHmmss")}.png");
                using (var stm = IO.File.Create(fileName))
                {
                    enc.Save(stm);
                }

                MessageBox.Show("保存成功");
                System.Diagnostics.Process.Start(targetFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnOpenSaveFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(targetFolder);
        }

        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("清除画布上所有的形状?", "请问", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MainCanvas.Children.Count > 0)
                {
                    MainCanvas.Children.Clear();
                }
            }

        }
    }
}
