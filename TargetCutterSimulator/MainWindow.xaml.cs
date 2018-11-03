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

        private List<Shape> shapeList = new List<Shape>();

        private void Initialize()
        {
            this.Title = "靶材切割设计器";
        }

        private const double ratio = 2.0;

        private void AddEllipse(double diameter)
        {
            Ellipse circle = new Ellipse();
            circle.Height = diameter * ratio;
            circle.Width = diameter * ratio;
            circle.Fill = Brushes.Transparent;
            circle.StrokeThickness = 1;
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
            rect.StrokeThickness = 1;
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
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                currentShape = e.OriginalSource as Shape;
                previousPoint = e.GetPosition(currentShape);
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
            AddEllipse(50.8);
        }

        private void BtnAddRectangle_Click(object sender, RoutedEventArgs e)
        {
            AddRectangle(100, 50);
        }

        private void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            currentShape = null;
        }
    }
}
