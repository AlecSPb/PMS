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
using PMSCommon;
using PMSTabletClient.ViewModel;

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
            Messenger.Default.Register<MsgObject>(this, NavigationToken.Navigate, ActionNavigation);

            views = new ViewLocator();
            viewModels = new ViewModelLocator();
            NavigateTo(views.Navigation);
        }

        private void ActionNavigation(MsgObject obj)
        {
            switch (obj.MsgToken)
            {
                case VToken.Navigation:
                    NavigateTo(views.Navigation);
                    break;
                case VToken.LogIn:
                    break;
                case VToken.Order:
                    break;
                case VToken.OrderEdit:
                    break;
                case VToken.OrderRefresh:
                    break;
                case VToken.OrderSelect:
                    break;
                case VToken.OrderCheck:
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
                case VToken.PlanSelectForTestResult:
                    break;
                case VToken.RecordTestResult:
                    break;
                case VToken.RecordTestResultEdit:
                    break;
                case VToken.RecordTestResultSelect:
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
                    NavigateTo(views.RecordVHP);
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
                case VToken.RecordTestResultRefresh:
                    break;
                case VToken.RecordDeliveryRefresh:
                    break;
                case VToken.PlanSelectForVHP:
                    break;
                case VToken.RecordVHPRefresh:
                    break;
                case VToken.RecordVHPQuickEdit:
                    NavigateTo(views.RecordVHPQuickEdit);
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

        private ViewModelLocator viewModels;
        private ViewLocator views;
        protected override void OnClosing(CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
            base.OnClosing(e);
        }
        private void NavigateTo(UserControl view)
        {
            main.Content = view;
        }

        private void ActionNavigate(string viewName)
        {

        }
    }
}
