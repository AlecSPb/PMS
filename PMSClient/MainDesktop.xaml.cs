using GalaSoft.MvvmLight.Messaging;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PMSClient.ViewModel;
using PMSClient.ViewForDesktop;

namespace PMSClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainDesktop : Window
    {
        public MainDesktop()
        {
            InitializeComponent();
            _viewLocator = new DesktopViewLocator();
            _viewModelLocator = new ViewModelLocator();

            GoTo(_viewLocator.Navigation);

        }
        private DesktopViewLocator _viewLocator;
        private ViewModelLocator _viewModelLocator;


        private void GoTo(UserControl view)
        {
            if (view != null)
            {
                mainArea.Content = view;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
            base.OnClosing(e);
        }


    }
}
