using PMSTabletClient.View;
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
            StartFirstView();
            RegisterTheNavigationView();
        }

        private void StartFirstView()
        {
            SetMainContent(new NavigationView());
        }
        private void SetMainContent(UserControl view)
        {
            main.Content = view;
        }
        private void RegisterTheNavigationView()
        {
            Messenger.Default.Register<string>(this, ViewToken.MainNavigate, arg => SetMainContent(new NavigationView()));
            Messenger.Default.Register<string>(this, ViewToken.RecordVHP, arg => SetMainContent(new RecordVHPView()));
            Messenger.Default.Register<string>(this, ViewToken.RecordVHPEdit, arg => SetMainContent(new RecordVHPEditView()));
            Messenger.Default.Register<string>(this, ViewToken.RecordVHPQuickEdit, arg => SetMainContent(new RecordVHPQuickEditView()));
            Messenger.Default.Register<string>(this, ViewToken.Product, arg => SetMainContent(new ProductView()));
            Messenger.Default.Register<string>(this, ViewToken.ProductEdit, arg => SetMainContent(new ProductEditView()));
            Messenger.Default.Register<string>(this, ViewToken.ProductReport, arg=> SetMainContent(new ProductReportView()));
        }


    }
}
