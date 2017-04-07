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
using PMSClient.MainService;

namespace PMSClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainDesktop : Window
    {
        private DesktopViewLocator _views; 

        public MainDesktop()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _views = PMSHelper.DesktopViews;

            Messenger.Default.Register<PMSViews>(this, MainNavigationToken.Navigate, ActionNavigation);
            Messenger.Default.Register<string>(this, MainNavigationToken.StatusMessage, ActionStatusMessage);

            NavigateTo(_views.LogIn);

            RefreshLogInformation();
            //this.BringIntoView();
        }

        private void ActionNavigation(PMSViews token)
        {
            RefreshLogInformation();
            PMSHelper.CurrentLog.Log(token.ToString());
            switch (token)
            {
                case PMSViews.LogIn:
                    NavigateTo(_views.LogIn);
                    break;
                case PMSViews.Navigation:
                    NavigateTo(_views.Navigation);
                    break;
                case PMSViews.Order:
                    NavigateTo(_views.Order);
                    break;
                case PMSViews.OrderEdit:
                    NavigateTo(_views.OrderEdit);
                    break;
                case PMSViews.MissonSelect:
                    NavigateTo(_views.MissonSelect);
                    break;
                case PMSViews.OrderCheck:
                    NavigateTo(_views.OrderCheck);
                    break;
                case PMSViews.OrderCheckEdit:
                    NavigateTo(_views.OrderCheckEdit);
                    break;
                case PMSViews.MaterialNeed:
                    NavigateTo(_views.MaterialNeed);
                    break;
                case PMSViews.MaterialNeedEdit:
                    NavigateTo(_views.MaterialNeedEdit);
                    break;
                case PMSViews.MaterialNeedSelect:
                    NavigateTo(_views.MaterialNeedSelect);
                    break;
                case PMSViews.MaterialOrder:
                    NavigateTo(_views.MaterialOrder);
                    break;
                case PMSViews.MaterialOrderEdit:
                    NavigateTo(_views.MaterialOrderEdit);
                    break;
                case PMSViews.MaterialOrderItemEdit:
                    NavigateTo(_views.MaterialOrderItemEdit);
                    break;
                case PMSViews.MaterialInventoryIn:
                    NavigateTo(_views.MaterialInventoryIn);
                    break;
                case PMSViews.MaterialInventoryInEdit:
                    NavigateTo(_views.MaterialInventoryInEdit);
                    break;
                case PMSViews.MaterialInventoryOut:
                    NavigateTo(_views.MaterialInventoryOut);
                    break;
                case PMSViews.MaterialInventoryOutEdit:
                    NavigateTo(_views.MaterialInventoryOutEdit);
                    break;
                case PMSViews.Misson:
                    NavigateTo(_views.Misson);
                    break;
                case PMSViews.Plan:
                    NavigateTo(_views.Plan);
                    break;
                case PMSViews.PlanEdit:
                    NavigateTo(_views.PlanEdit);
                    break;
                case PMSViews.PlanSelect:
                    NavigateTo(_views.PlanSelect);
                    break;
                case PMSViews.RecordMilling:
                    NavigateTo(_views.RecordMilling);
                    break;
                case PMSViews.RecordMillingEdit:
                    NavigateTo(_views.RecordMillingEdit);
                    break;
                case PMSViews.RecordVHP:
                    NavigateTo(_views.RecordVHP);
                    break;
                case PMSViews.RecordVHPQuickEdit:
                    NavigateTo(_views.RecordVHPQuickEdit);
                    break;
                case PMSViews.RecordDeMold:
                    NavigateTo(_views.RecordDeMold);
                    break;
                case PMSViews.RecordDeMoldEdit:
                    NavigateTo(_views.RecordDeMoldEdit);
                    break;
                case PMSViews.RecordMachine:
                    NavigateTo(_views.RecordMachine);
                    break;
                case PMSViews.RecordMachineEdit:
                    NavigateTo(_views.RecordMachineEdit);
                    break;
                case PMSViews.RecordTest:
                    NavigateTo(_views.RecordTest);
                    break;
                case PMSViews.RecordTestEdit:
                    NavigateTo(_views.RecordTestEdit);
                    break;
                case PMSViews.RecordDelivery:
                    NavigateTo(_views.RecordDelivery);
                    break;
                case PMSViews.RecordDeliveryEdit:
                    NavigateTo(_views.RecordDeliveryEdit);
                    break;
                case PMSViews.RecordDeliveryItemEdit:
                    NavigateTo(_views.RecordDeliveryItemEdit);
                    break;




                default:
                    break;
            }
        }

        private void RefreshLogInformation()
        {
            var _logInformation = PMSHelper.CurrentSession;
            if (_logInformation.CurrentUser != null)
            {
                txtCurrentUserName.Text = $"当前用户:[{ _logInformation.CurrentUser.RealName}] 角色:[{_logInformation.CurrentUserRole.GroupName}]";
            }
            else
            {
                txtCurrentUserName.Text = "未登录";
            }
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



        private void NavigateTo(UserControl view)
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

        //private void btnClose_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        //private void btnMaximum_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.WindowState == WindowState.Maximized)
        //    {
        //        this.WindowState = WindowState.Normal;
        //    }
        //    else
        //    {
        //        this.WindowState = WindowState.Maximized;
        //    }
        //}

        //private void btnMiinimum_Click(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = WindowState.Minimized;
        //}

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
