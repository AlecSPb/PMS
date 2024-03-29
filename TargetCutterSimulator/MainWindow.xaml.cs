﻿using System;
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
            this.Title = "Target Cutter Designer designed by xs.zhou";
            var data = new List<double>();
            #region 数据
            data.Add(50);
            data.Add(50.8);
            data.Add(76.2);
            data.Add(80);
            data.Add(100);
            data.Add(105);
            data.Add(110);
            data.Add(124.5);
            data.Add(128);
            data.Add(152);
            data.Add(155);
            data.Add(168);
            data.Add(200);
            data.Add(205);
            data.Add(206);
            data.Add(230);
            data.Add(233);
            data.Add(255);
            data.Add(300);
            data.Add(303);
            data.Add(319);
            data.Add(330);
            data.Add(336);
            data.Add(440);
            data.Add(444.7);
            data.Add(450);
            #endregion
            CboDiameter.ItemsSource = data;
        }

        private const double ratio = 2.0;
        private const double normal_thickness = 2.0;
        private const double select_thickness = 3.0;

        private Brush fill_brush = Brushes.LightGray;

        private void AddEllipse(double diameter)
        {
            MainCanvas.Children.Add(CreateEllipse(diameter));
        }

        private Ellipse CreateEllipse(double diameter)
        {
            Ellipse shape = new Ellipse();
            shape.Height = diameter * ratio;
            shape.Width = diameter * ratio;
            shape.Fill = fill_brush;
            shape.StrokeThickness = normal_thickness;
            shape.Stroke = Brushes.Blue;

            shape.MouseDown += Shape_MouseDown;
            shape.MouseMove += Shape_MouseMove;
            shape.MouseUp += Shape_MouseUp;

            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform());
            transformGroup.Children.Add(new TranslateTransform());
            shape.RenderTransform = transformGroup;

            shape.ContextMenu = this.Resources["ShapeContextMenu"] as ContextMenu;
            return shape;
        }

        private Rectangle CreateRectangle(double width, double height)
        {
            Rectangle shape = new Rectangle();
            shape.Height = height * ratio;
            shape.Width = width * ratio;
            shape.Fill = fill_brush;
            shape.StrokeThickness = normal_thickness;
            shape.Stroke = Brushes.Blue;
            shape.MouseDown += Shape_MouseDown;
            shape.MouseMove += Shape_MouseMove;
            shape.MouseUp += Shape_MouseUp;

            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform());
            transformGroup.Children.Add(new TranslateTransform());
            shape.RenderTransform = transformGroup;

            shape.ContextMenu = this.Resources["ShapeContextMenu"] as ContextMenu;
            return shape;
        }

        private void AddRectangle(double width, double height)
        {
            MainCanvas.Children.Add(CreateRectangle(width, height));
        }

        private void Shape_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                if (e.OriginalSource is Shape)
                {
                    currentShape = e.OriginalSource as Shape;
                    previousPoint = e.GetPosition(currentShape);
                    currentShape.StrokeThickness = select_thickness;
                }
            }
            System.Diagnostics.Debug.WriteLine("Down");

        }

        private void MoveCurrentShapeToTop()
        {
            int shape_count = MainCanvas.Children.Count;
            if (currentShape != null && shape_count > 0)
            {
                MainCanvas.Children.Remove(currentShape);
                MainCanvas.Children.Insert(shape_count - 1, currentShape);
            }
        }

        private void MoveCurrentShapeToBottom()
        {
            int shape_count = MainCanvas.Children.Count;
            if (currentShape != null && shape_count > 0)
            {
                MainCanvas.Children.Remove(currentShape);
                MainCanvas.Children.Insert(0, currentShape);
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

            if (double.TryParse(CboDiameter.Text.Trim(), out double diameter)
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
            System.Diagnostics.Debug.WriteLine("Up");

        }

        private TranslateTransform GetTranslateTransform(Shape shape)
        {
            if (shape != null)
            {
                return (shape.RenderTransform as TransformGroup).Children[1] as TranslateTransform;
            }
            return null;
        }

        private void CopyTranslateTransformXY(Shape source, Shape target, double offset = 5)
        {
            if (source != null && target != null)
            {
                GetTranslateTransform(target).X = GetTranslateTransform(source).X + offset;
                GetTranslateTransform(target).Y = GetTranslateTransform(source).Y + offset;
            }
        }
        private void CopyShape(Shape currentShape)
        {
            if (currentShape.GetType() == typeof(Rectangle))
            {
                var newShape = CreateRectangle(currentShape.Width / ratio, currentShape.Height / ratio);
                CopyTranslateTransformXY(currentShape, newShape);
                MainCanvas.Children.Add(newShape);
            }
            else if (currentShape.GetType() == typeof(Ellipse))
            {
                var newShape = CreateEllipse(currentShape.Width / ratio);
                CopyTranslateTransformXY(currentShape, newShape);
                MainCanvas.Children.Add(newShape);
            }
        }

        private void CmCopy_Click(object sender, RoutedEventArgs e)
        {
            if (currentShape != null)
            {
                CopyShape(currentShape);
            }
            System.Diagnostics.Debug.WriteLine("Copy");

        }

        private void CmDelete_Click(object sender, RoutedEventArgs e)
        {
            if (currentShape != null)
            {
                MainCanvas.Children.Remove(currentShape);
                currentShape = null;
            }
            System.Diagnostics.Debug.WriteLine("Delete");

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


                if (!IO.Directory.Exists(targetFolder))
                {
                    IO.Directory.CreateDirectory(targetFolder);
                }
                string fileName = IO.Path.Combine(targetFolder, $"TC{DateTime.Now.ToString("yyyyMMddHHmmss")}.png");
                using (var stm = IO.File.Create(fileName))
                {
                    enc.Save(stm);
                }

                MessageBox.Show("Save Success", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
                //System.Diagnostics.Process.Start(targetFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnOpenSaveFolder_Click(object sender, RoutedEventArgs e)
        {
            if (!IO.Directory.Exists(targetFolder))
            {
                IO.Directory.CreateDirectory(targetFolder);
            }
            System.Diagnostics.Process.Start(targetFolder);

        }

        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Clear all shapes?", "Asking", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MainCanvas.Children.Count > 0)
                {
                    MainCanvas.Children.Clear();
                }
            }

        }

        private void CmMoveTop_Click(object sender, RoutedEventArgs e)
        {
            MoveCurrentShapeToTop();
        }

        private void CmMoveBottom_Click(object sender, RoutedEventArgs e)
        {
            MoveCurrentShapeToBottom();
        }

        private void MainCanvas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmFillColor_Click(object sender, RoutedEventArgs e)
        {
            if (currentShape != null)
            {
                var dialog = new System.Windows.Forms.ColorDialog();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var color = dialog.Color;
                    var wpfcolor = new System.Windows.Media.Color();
                    wpfcolor.A = color.A;
                    wpfcolor.R = color.R;
                    wpfcolor.G = color.G;
                    wpfcolor.B = color.B;
                    fill_brush= new SolidColorBrush(wpfcolor);
                    currentShape.Fill = fill_brush;
                }
            }

        }
    }
}
