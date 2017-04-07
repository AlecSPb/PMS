using GalaSoft.MvvmLight.Messaging;
using PMSClient.ViewForTablet;
using PMSClient.ViewModel;
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
using System.Windows.Shapes;
using System.ComponentModel;

namespace PMSClient
{
    /// <summary>
    /// MainTablet.xaml 的交互逻辑
    /// </summary>
    public partial class MainTablet : Window
    {
        public MainTablet()
        {
            InitializeComponent();
        }


        private TabletViewLocator _viewLocator;
        private ViewModelLocator _viewModelLocator;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewLocator = new TabletViewLocator();
            _viewModelLocator = this.FindResource("Locator") as ViewModelLocator;

            Messenger.Default.Register<MsgObject>(this, MainNavigationToken.Navigate, ActionNavigate);
            Messenger.Default.Register<string>(this, MainNavigationToken.StatusMessage, ActionStatusMessage);

            //load the first page
            GoTo(_viewLocator.Navigation);
        }

        private void ActionStatusMessage(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                txtStateMessage.Text = obj;
            }
            else
            {
                txtStateMessage.Text = "状态栏";
            }
        }

        /// <summary>
        /// Main Navigation Part
        /// </summary>
        /// <param name="model"></param>
        private void ActionNavigate(MsgObject model)
        {
            try
            {
                //log
                PMSHelper.CurrentLog.Log(model.NavigateTo.ToString());


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

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
