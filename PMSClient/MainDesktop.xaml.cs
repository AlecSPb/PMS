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
            this.BringIntoView();
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

        /// <summary>
        /// Main Navigation Part
        /// </summary>
        /// <param name="model"></param>
        //private void ActionNavigate(MsgObject model)
        //{
        //    try
        //    {
        //        //log
        //        PMSHelper.CurrentLog.Log(model.NavigateTo.ToString());

        //        switch (model.NavigateTo)
        //        {
        //            case VToken.Navigation:
        //                NavigateTo(_viewLocator.Navigation);
        //                RefreshLogInformation();
        //                break;
        //            case VToken.LogIn:
        //                NavigateTo(_viewLocator.LogIn);
        //                break;
        //            case VToken.Order:
        //                NavigateTo(_viewLocator.Order);
        //                break;
        //            case VToken.OrderEdit:
        //                _viewModelLocator.OrderEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.OrderEdit.DataContext = _viewModelLocator.OrderEdit;
        //                NavigateTo(_viewLocator.OrderEdit);
        //                break;
        //            case VToken.OrderRefresh:
        //                break;
        //            case VToken.OrderSelect:
        //                _viewModelLocator.OrderSelect.SetKeyProeprties(model.NavigateFrom, model.NavigateFrom);
        //                _viewLocator.OrderSelect.DataContext = _viewModelLocator.OrderSelect;
        //                NavigateTo(_viewLocator.OrderSelect);
        //                break;
        //            case VToken.OrderCheck:
        //                NavigateTo(_viewLocator.OrderCheck);
        //                break;
        //            case VToken.OrderCheckRefresh:
        //                break;
        //            case VToken.OrderCheckEdit:
        //                _viewModelLocator.OrderCheckEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.OrderCheckEdit.DataContext = _viewModelLocator.OrderCheckEdit;
        //                NavigateTo(_viewLocator.OrderCheckEdit);
        //                break;
        //            case VToken.Misson:
        //                NavigateTo(_viewLocator.Misson);
        //                break;
        //            case VToken.MissonRefresh:
        //                break;
        //            case VToken.Plan:
        //                NavigateTo(_viewLocator.Plan);
        //                break;
        //            case VToken.PlanEdit:
        //                _viewModelLocator.PlanEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.PlanEdit.DataContext = _viewModelLocator.PlanEdit;
        //                NavigateTo(_viewLocator.PlanEdit);
        //                break;
        //            case VToken.PlanSelectForTest:
        //                NavigateTo(_viewLocator.PlanSelect);
        //                break;
        //            case VToken.RecordTest:
        //                NavigateTo(_viewLocator.RecordTest);
        //                break;
        //            case VToken.RecordTestEdit:
        //                _viewModelLocator.RecordTestEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.RecordTestEdit.DataContext = _viewModelLocator.RecordTestEdit;
        //                NavigateTo(_viewLocator.RecordTestEdit);
        //                break;
        //            case VToken.RecordTestSelect:
        //                break;
        //            case VToken.RecordDelivery:
        //                NavigateTo(_viewLocator.RecordDelivery);
        //                break;
        //            case VToken.RecordDeliveryEdit:
        //                _viewModelLocator.RecordDeliveryEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.RecordDeliveryEdit.DataContext = _viewModelLocator.RecordDeliveryEdit;
        //                NavigateTo(_viewLocator.RecordDeliveryEdit);
        //                break;
        //            case VToken.RecordDeliveryItem:
        //                break;
        //            case VToken.RecordDeliveryItemEdit:
        //                _viewModelLocator.RecordDeliveryItemEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.RecordDeliveryItemEdit.DataContext = _viewModelLocator.RecordDeliveryItemEdit;
        //                NavigateTo(_viewLocator.RecordDeliveryItemEdit);
        //                break;
        //            case VToken.RecordMilling:
        //                NavigateTo(_viewLocator.RecordMilling);
        //                break;
        //            case VToken.RecordMillingEdit:
        //                _viewModelLocator.RecordMillingEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.RecordMillingEdit.DataContext = _viewModelLocator.RecordMillingEdit;
        //                NavigateTo(_viewLocator.RecordMillingEdit);
        //                break;
        //            case VToken.RecordVHP:
        //                NavigateTo(_viewLocator.RecordVHP);
        //                break;
        //            case VToken.RecordVHPEdit:
        //                break;
        //            case VToken.RecordDeMold:
        //                NavigateTo(_viewLocator.RecordDeMold);
        //                break;
        //            case VToken.RecordDeMoldEdit:
        //                _viewModelLocator.RecordDeMoldEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.RecordDeMoldEdit.DataContext = _viewModelLocator.RecordDeMoldEdit;
        //                NavigateTo(_viewLocator.RecordDeMoldEdit);
        //                break;
        //            case VToken.RecordMachine:
        //                NavigateTo(_viewLocator.RecordMachine);
        //                break;
        //            case VToken.RecordMachineEdit:
        //                _viewModelLocator.RecordMachineEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.RecordMachineEdit.DataContext = _viewModelLocator.RecordMachineEdit;
        //                NavigateTo(_viewLocator.RecordMachineEdit);
        //                break;
        //            case VToken.RecordBonding:
        //                break;
        //            case VToken.RecordBondingEdit:
        //                break;
        //            case VToken.MaterialOrder:
        //                NavigateTo(_viewLocator.MaterialOrder);
        //                break;
        //            case VToken.MaterialOrderEdit:
        //                _viewModelLocator.MaterialOrderEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.MaterialOrderEdit.DataContext = _viewModelLocator.MaterialOrderEdit;
        //                NavigateTo(_viewLocator.MaterialOrderEdit);
        //                break;
        //            case VToken.MaterialOrderItemEdit:
        //                _viewModelLocator.MaterialOrderItemEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.MaterialOrderItemEdit.DataContext = _viewModelLocator.MaterialOrderItemEdit;
        //                NavigateTo(_viewLocator.MaterialOrderItemEdit);
        //                break;
        //            case VToken.MaterialNeed:
        //                NavigateTo(_viewLocator.MaterialNeed);
        //                break;
        //            case VToken.MaterialNeedEdit:
        //                _viewModelLocator.MaterialNeedEdit.SetKeyProperties(model.MsgModel);
        //                _viewLocator.MaterialNeedEdit.DataContext = _viewModelLocator.MaterialNeedEdit;
        //                NavigateTo(_viewLocator.MaterialNeedEdit);
        //                break;
        //            case VToken.MaterialNeedEdit2:
        //                //选择Order后，更改现有VM
        //                _viewModelLocator.MaterialNeedEdit.SetKeyProperties(model.MsgModel.Model as DcOrder);
        //                _viewLocator.MaterialNeedEdit.DataContext = _viewModelLocator.MaterialNeedEdit;
        //                NavigateTo(_viewLocator.MaterialNeedEdit);
        //                break;
        //            case VToken.MaterialNeedSelect:
        //                _viewLocator.MaterialNeedSelect.DataContext = new MaterialNeedSelectVM(model.MsgModel);
        //                NavigateTo(_viewLocator.MaterialNeedSelect);
        //                break;
        //            case VToken.MaterialInventory:
        //                break;
        //            case VToken.MaterialInventoryEdit:
        //                break;
        //            case VToken.MaterialNeedRefresh:
        //                break;
        //            case VToken.MaterialOrderRefresh:
        //                break;
        //            case VToken.RecordTestRefresh:
        //                break;
        //            case VToken.RecordDeliveryRefresh:
        //                break;
        //            case VToken.PlanSelectForVHP:
        //                break;
        //            case VToken.RecordVHPRefresh:
        //                break;
        //            case VToken.RecordVHPQuickEdit:
        //                NavigateTo(_viewLocator.RecordVHPQuickEdit);
        //                break;
        //            case VToken.SetRecordVHPQuickEditSelectIndex:
        //                break;
        //            case VToken.MaterialOrderItemRefresh:
        //                break;
        //            case VToken.RecordDeliveryItemRefresh:
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}



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
