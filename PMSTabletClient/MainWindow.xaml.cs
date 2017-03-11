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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using PMSCommon;
using PMSTabletClient.ViewModel;

namespace PMSTabletClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            viewLocator = new ViewLocator();
            viewModelLocator = new ViewModelLocator();
            NavigateTo(viewLocator.Navigation);
        }
        private ViewModelLocator viewModelLocator;
        private ViewLocator viewLocator;
        protected override void OnClosing(CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
            base.OnClosing(e);
        }
        private void NavigateTo(UserControl view)
        {
            main.Content = view;
        }

        private void ActionNavigate(string viewName)
        {

        }
    }
}
