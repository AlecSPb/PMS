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
using GalaSoft.MvvmLight.Messaging;
using NotificationDAL;

namespace NotificationManagement
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetMainContent(new NoticeDisplayView());

            Messenger.Default.Register<string>(this, NavigateToken.Navigate, view =>
             {
                 switch (view)
                 {
                     case "NoticeDisplayView":
                         SetMainContent(new NoticeDisplayView());
                         break;
                     default:
                         SetMainContent(new NoticeDisplayView());
                         break;
                 }
             });

            Messenger.Default.Register<Notice>(this, NavigateToken.Navigate, model =>
            {
                var vm = new NoticeEditVM(model);
                var v = new NoticeEditView();
                v.DataContext = vm;
                SetMainContent(v);
            });

        }

        private void SetMainContent(UserControl view)
        {
            if (view != null)
            {
                this.main.Content = view;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
            base.OnClosing(e);
        }
    }
}
