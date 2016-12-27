using PMSDesktopClient.View;
using PMSDesktopClient.View.Sales;
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
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;

namespace PMSDesktopClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            RegisterNavigation();
            DataContext = new ViewModel.MainWindowVM();
            SetMainContent(new LogInView());
        }

        private void RegisterNavigation()
        {
            Messenger.Default.Register<object>(this, ViewToken.LogIn, msg => SetMainContent(new LogInView()));
            Messenger.Default.Register<object>(this, ViewToken.Order, msg => SetMainContent(new OrderView()));
            Messenger.Default.Register<object>(this, ViewToken.OrderEdit, msg => SetMainContent(new OrderEditView()));
            Messenger.Default.Register<object>(this, ViewToken.OrderReview, msg => SetMainContent(new OrderReviewView()));
            Messenger.Default.Register<object>(this, ViewToken.Misson, msg => SetMainContent(new MissonView()));
            Messenger.Default.Register<object>(this, ViewToken.Plan, msg => SetMainContent(new PlanView()));
            Messenger.Default.Register<object>(this, ViewToken.PlanEdit, msg => SetMainContent(new PlanEditView()));
        }

        private void SetMainContent(UserControl view)
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

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
