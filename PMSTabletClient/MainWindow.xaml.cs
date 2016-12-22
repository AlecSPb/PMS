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
            RegisterTheNavigation();
        }

        private void StartFirstView()
        {
            SetMainContent(new NavigationView());
        }
        private void SetMainContent(UserControl view)
        {
            main.Content = view;
        }
        private void RegisterTheNavigation()
        {
            Messenger.Default.Register<string>(this, ViewToken.MainNavigate, ActionMainNavigateView);
            Messenger.Default.Register<string>(this, ViewToken.RecordVHP, ActionRecordVHP);
            Messenger.Default.Register<string>(this, ViewToken.RecordVHPEdit, ActionRecordVHPEdit);
        }

        private void ActionRecordVHPEdit(string obj)
        {
            SetMainContent(new RecordVHPEditView());
        }

        private void ActionRecordVHP(string obj)
        {
            SetMainContent(new RecordVHPView());
        }

        private void ActionMainNavigateView(string obj)
        {
            SetMainContent(new NavigationView());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
            base.OnClosing(e);
        }
    }
}
