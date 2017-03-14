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
            _viewLocator = new DesktopViewLocator();
            _viewModelLocator = new ViewModelLocator();

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

        private void ActionNavigate(MsgObject obj)
        {
            switch (obj.MsgToken)
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
                    _viewLocator.OrderEdit.DataContext = new OrderEditVM(obj.MsgModel);
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
                    break;
                case VToken.Misson:
                    break;
                case VToken.MissonRefresh:
                    break;
                case VToken.Plan:
                    break;
                case VToken.PlanEdit:
                    break;
                case VToken.PlanSelectForTest:
                    break;
                case VToken.RecordTest:
                    break;
                case VToken.RecordTestEdit:
                    break;
                case VToken.RecordTestSelect:
                    break;
                case VToken.RecordDelivery:
                    break;
                case VToken.RecordDeliveryEdit:
                    break;
                case VToken.RecordDeliveryItem:
                    break;
                case VToken.RecordDeliveryItemEdit:
                    break;
                case VToken.RecordMilling:
                    break;
                case VToken.RecordMillingEdit:
                    break;
                case VToken.RecordVHP:
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
                    break;
                case VToken.MaterialOrderEdit:
                    break;
                case VToken.MaterialOrderItemEdit:
                    break;
                case VToken.MaterialNeed:
                    break;
                case VToken.MaterialNeedEdit:
                    break;
                case VToken.MaterialNeedSelect:
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
