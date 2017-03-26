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
        private DesktopViewLocator _viewLocator;
        private ViewModelLocator _viewModelLocator;
        private LogInformation _logInformation;
        public MainDesktop()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewLocator = new DesktopViewLocator();
            _viewModelLocator = this.FindResource("Locator") as ViewModelLocator;
            _logInformation = new LogInformation();

            Messenger.Default.Register<MsgObject>(this, MainNavigationToken.Navigate, ActionNavigate);
            Messenger.Default.Register<string>(this, MainNavigationToken.StatusMessage, ActionStatusMessage);

            //load the first page
            //GoTo(_viewLocator.Navigation);

            GoTo(_viewLocator.LogIn);

            RefreshLogInformation();
        }

        private void RefreshLogInformation()
        {
            if (_logInformation.CurrentUser!=null)
            {
                txtCurrentUserName.Text = $"当前登录用户:{ _logInformation.CurrentUser.UserName}";
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
        private void ActionNavigate(MsgObject model)
        {
            try
            {
                switch (model.MsgToken)
                {
                    case VToken.Navigation:
                        GoTo(_viewLocator.Navigation);
                        break;
                    case VToken.LogIn:
                        break;
                    case VToken.Order:
                        GoTo(_viewLocator.Order);
                        break;
                    case VToken.OrderEdit:
                        _viewLocator.OrderEdit.DataContext = new OrderEditVM(model.MsgModel);
                        GoTo(_viewLocator.OrderEdit);
                        break;
                    case VToken.OrderRefresh:
                        break;
                    case VToken.OrderSelect:
                        GoTo(_viewLocator.OrderSelect);
                        break;
                    case VToken.OrderCheck:
                        GoTo(_viewLocator.OrderCheck);
                        break;
                    case VToken.OrderCheckRefresh:
                        break;
                    case VToken.OrderCheckEdit:
                        _viewLocator.OrderCheckEdit.DataContext = new OrderCheckEditVM(model.MsgModel);
                        GoTo(_viewLocator.OrderCheckEdit);
                        break;
                    case VToken.Misson:
                        GoTo(_viewLocator.Misson);
                        break;
                    case VToken.MissonRefresh:
                        break;
                    case VToken.Plan:
                        GoTo(_viewLocator.Plan);
                        break;
                    case VToken.PlanEdit:
                        _viewLocator.PlanEdit.DataContext = new PlanEditVM(model.MsgModel);
                        GoTo(_viewLocator.PlanEdit);
                        break;
                    case VToken.PlanSelectForTest:
                        GoTo(_viewLocator.PlanSelect);
                        break;
                    case VToken.RecordTest:
                        GoTo(_viewLocator.RecordTest);
                        break;
                    case VToken.RecordTestEdit:
                        _viewLocator.RecordTestEdit.DataContext = new RecordTestEditVM(model.MsgModel);
                        GoTo(_viewLocator.RecordTestEdit);
                        break;
                    case VToken.RecordTestSelect:
                        break;
                    case VToken.RecordDelivery:
                        GoTo(_viewLocator.RecordDelivery);
                        break;
                    case VToken.RecordDeliveryEdit:
                        _viewLocator.RecordDeliveryEdit.DataContext = new RecordDeliveryEditVM(model.MsgModel);
                        GoTo(_viewLocator.RecordDeliveryEdit);
                        break;
                    case VToken.RecordDeliveryItem:
                        break;
                    case VToken.RecordDeliveryItemEdit:
                        _viewLocator.RecordDeliveryItemEdit.DataContext = new RecordDeliveryItemEditVM(model.MsgModel);
                        GoTo(_viewLocator.RecordDeliveryItemEdit);
                        break;
                    case VToken.RecordMilling:
                        GoTo(_viewLocator.RecordMilling);
                        break;
                    case VToken.RecordMillingEdit:
                        _viewLocator.RecordMillingEdit.DataContext = new RecordMillingEditVM(model.MsgModel);
                        GoTo(_viewLocator.RecordMillingEdit);
                        break;
                    case VToken.RecordVHP:
                        GoTo(_viewLocator.RecordVHP);
                        break;
                    case VToken.RecordVHPEdit:
                        break;
                    case VToken.RecordDeMold:
                        GoTo(_viewLocator.RecordDeMold);
                        break;
                    case VToken.RecordDeMoldEdit:
                        _viewLocator.RecordDeMoldEdit.DataContext = new RecordDeMoldEditVM(model.MsgModel);
                        GoTo(_viewLocator.RecordDeMoldEdit);
                        break;
                    case VToken.RecordMachine:
                        GoTo(_viewLocator.RecordMachine);
                        break;
                    case VToken.RecordMachineEdit:
                        _viewLocator.RecordMachineEdit.DataContext = new RecordMachineEditVM(model.MsgModel);
                        GoTo(_viewLocator.RecordMachineEdit);
                        break;
                    case VToken.RecordBonding:
                        break;
                    case VToken.RecordBondingEdit:
                        break;
                    case VToken.MaterialOrder:
                        GoTo(_viewLocator.MaterialOrder);
                        break;
                    case VToken.MaterialOrderEdit:
                        _viewLocator.MaterialOrderEdit.DataContext = new MaterialOrderEditVM(model.MsgModel);
                        GoTo(_viewLocator.MaterialOrderEdit);
                        break;
                    case VToken.MaterialOrderItemEdit:
                        _viewLocator.MaterialOrderItemEdit.DataContext = new MaterialOrderItemEditVM(model.MsgModel);
                        GoTo(_viewLocator.MaterialOrderItemEdit);
                        break;
                    case VToken.MaterialNeed:
                        GoTo(_viewLocator.MaterialNeed);
                        break;
                    case VToken.MaterialNeedEdit:
                        _viewLocator.MaterialNeedEdit.DataContext = new MaterialNeedEditVM(model.MsgModel);
                        GoTo(_viewLocator.MaterialNeedEdit);
                        break;
                    case VToken.MaterialNeedSelect:
                        _viewLocator.MaterialNeedSelect.DataContext = new MaterialNeedSelectVM(model.MsgModel);
                        GoTo(_viewLocator.MaterialNeedSelect);
                        break;
                    case VToken.MaterialInventory:
                        break;
                    case VToken.MaterialInventoryEdit:
                        break;
                    case VToken.MaterialNeedRefresh:
                        break;
                    case VToken.MaterialOrderRefresh:
                        break;
                    case VToken.RecordTestRefresh:
                        break;
                    case VToken.RecordDeliveryRefresh:
                        break;
                    case VToken.PlanSelectForVHP:
                        break;
                    case VToken.RecordVHPRefresh:
                        break;
                    case VToken.RecordVHPQuickEdit:
                        GoTo(_viewLocator.RecordVHPQuickEdit);
                        break;
                    case VToken.SetRecordVHPQuickEditSelectIndex:
                        break;
                    case VToken.MaterialOrderItemRefresh:
                        break;
                    case VToken.RecordDeliveryItemRefresh:
                        break;
                    default:
                        break;
                }
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMaximum_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void btnMiinimum_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
