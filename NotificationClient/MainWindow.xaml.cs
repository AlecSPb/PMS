using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotificationClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region Properties
        private double windowWidth;
        public double WindowWidth
        {
            get { return windowWidth; }
            set { windowWidth = value; RaisePropertyChanged(nameof(WindowWidth)); }
        }

        private double textblockWidth;
        public double TextblockWidth
        {
            get { return textblockWidth; }
            set { textblockWidth = value; RaisePropertyChanged(nameof(TextblockWidth)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            SetAndStart();
        }

        private void SetAndStart()
        {
            WindowWidth = window.ActualWidth;
            TextblockWidth = txtInformation.ActualWidth;
            var storyboard = (Storyboard)this.FindResource("InformationScrollAnimation");
            storyboard.Begin();
        }
    }
}
