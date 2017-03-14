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

        public MainDesktop()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewLocator = new DesktopViewLocator();
            _viewModelLocator = this.FindResource("Locator") as ViewModelLocator;

            Messenger.Default.Register<MsgObject>(this, MainNavigationToken.Navigate, ActionNavigate);
            Messenger.Default.Register<string>(this, MainNavigationToken.StatusMessage, ActionStatusMessage);

            //load the first page
            GoTo(_viewLocator.Navigation);
        }

        private void ActionStatusMessage(string obj)
        {
            if (string.IsNullOrEmpty(obj))
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
                    break;
                case VToken.PlanSelectForTest:
                    //GoTo(_viewLocator.PlanSelect);
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
                    break;
                case VToken.RecordMillingEdit:
                    break;
                case VToken.RecordVHP:
                    GoTo(_viewLocator.RecordVHP);
                    break;
                case VToken.RecordVHPEdit:
                    break;
                case VToken.RecordTakeOut:
                    break;
                case VToken.RecordTakeOutEdit:
                    break;
                case VToken.RecordMachine:
                    break;
                case VToken.RecordMachineEdit:
                    break;
                case VToken.RecordBonding:
                    break;
                case VToken.RecordBondingEdit:
                    break;
                case VToken.MaterialOrder:
                    GoTo(_viewLocator.MaterialOrder);
                    break;
                case VToken.MaterialOrderEdit:
                    break;
                case VToken.MaterialOrderItemEdit:
                    break;
                case VToken.MaterialNeed:
                    GoTo(_viewLocator.MaterialNeed);
                    break;
                case VToken.MaterialNeedEdit:
                    _viewLocator.MaterialNeedEdit.DataContext = new MaterialNeedEditVM(model.MsgModel);
                    GoTo(_viewLocator.MaterialNeedEdit);
                    break;
                case VToken.MaterialNeedSelect:
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
