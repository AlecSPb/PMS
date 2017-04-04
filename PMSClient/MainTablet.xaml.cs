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
                PMSHelper.CurrentLog.Log(model.MsgToken.ToString());

                switch (model.MsgToken)
                {
                    case VToken.Navigation:
                        GoTo(_viewLocator.Navigation);
                        break;
                    case VToken.LogIn:
                        //GoTo(_viewLocator.LogIn);
                        break;
                    case VToken.Order:
                        //GoTo(_viewLocator.Order);
                        break;
                    case VToken.OrderEdit:
                        //_viewModelLocator.OrderEdit.SetKeyProperties(model.MsgModel);
                        //_viewLocator.OrderEdit.DataContext = _viewModelLocator.OrderEdit;
                        //GoTo(_viewLocator.OrderEdit);
                        break;
                    case VToken.OrderRefresh:
                        break;
                    case VToken.OrderSelect:
                        //GoTo(_viewLocator.OrderSelect);
                        break;
                    case VToken.OrderCheck:
                        //GoTo(_viewLocator.OrderCheck);
                        break;
                    case VToken.OrderCheckRefresh:
                        break;
                    case VToken.OrderCheckEdit:
                        //_viewModelLocator.OrderCheckEdit.SetKeyProperties(model.MsgModel);
                        //_viewLocator.OrderCheckEdit.DataContext = _viewModelLocator.OrderCheckEdit;
                        //GoTo(_viewLocator.OrderCheckEdit);
                        break;
                    case VToken.Misson:
                        //GoTo(_viewLocator.Misson);
                        break;
                    case VToken.MissonRefresh:
                        break;
                    case VToken.Plan:
                        //GoTo(_viewLocator.Plan);
                        break;
                    case VToken.PlanEdit:
                        //_viewModelLocator.PlanEdit.SetKeyProperties(model.MsgModel);
                        //_viewLocator.PlanEdit.DataContext = _viewModelLocator.PlanEdit;
                        //GoTo(_viewLocator.PlanEdit);
                        break;
                    case VToken.PlanSelectForTest:
                        GoTo(_viewLocator.PlanSelect);
                        break;
                    case VToken.RecordTest:
                        GoTo(_viewLocator.RecordTest);
                        break;
                    case VToken.RecordTestEdit:
                        _viewModelLocator.RecordTestEdit.SetKeyProperties(model.MsgModel);
                        _viewLocator.RecordTestEdit.DataContext = _viewModelLocator.RecordTestEdit;
                        GoTo(_viewLocator.RecordTestEdit);
                        break;
                    case VToken.RecordTestSelect:
                        break;
                    case VToken.RecordDelivery:
                        GoTo(_viewLocator.RecordDelivery);
                        break;
                    case VToken.RecordDeliveryEdit:
                        _viewModelLocator.RecordDeliveryEdit.SetKeyProperties(model.MsgModel);
                        _viewLocator.RecordDeliveryEdit.DataContext = _viewModelLocator.RecordDeliveryEdit;
                        GoTo(_viewLocator.RecordDeliveryEdit);
                        break;
                    case VToken.RecordDeliveryItem:
                        break;
                    case VToken.RecordDeliveryItemEdit:
                        _viewModelLocator.RecordDeliveryItemEdit.SetKeyProperties(model.MsgModel);
                        _viewLocator.RecordDeliveryItemEdit.DataContext = _viewModelLocator.RecordDeliveryItemEdit;
                        GoTo(_viewLocator.RecordDeliveryItemEdit);
                        break;
                    case VToken.RecordMilling:
                        //GoTo(_viewLocator.RecordMilling);
                        break;
                    case VToken.RecordMillingEdit:
                        //_viewModelLocator.RecordMillingEdit.SetKeyProperties(model.MsgModel);
                        //_viewLocator.RecordMillingEdit.DataContext = _viewModelLocator.RecordMillingEdit;
                        //GoTo(_viewLocator.RecordMillingEdit);
                        break;
                    case VToken.RecordVHP:
                        GoTo(_viewLocator.RecordVHP);
                        break;
                    case VToken.RecordVHPEdit:
                        break;
                    case VToken.RecordDeMold:
                        //GoTo(_viewLocator.RecordDeMold);
                        break;
                    case VToken.RecordDeMoldEdit:
                        //_viewModelLocator.RecordDeMoldEdit.SetKeyProperties(model.MsgModel);
                        //_viewLocator.RecordDeMoldEdit.DataContext = _viewModelLocator.RecordDeMoldEdit;
                        //GoTo(_viewLocator.RecordDeMoldEdit);
                        break;
                    case VToken.RecordMachine:
                        //GoTo(_viewLocator.RecordMachine);
                        break;
                    case VToken.RecordMachineEdit:
                        //_viewModelLocator.RecordMachineEdit.SetKeyProperties(model.MsgModel);
                        //_viewLocator.RecordMachineEdit.DataContext = _viewModelLocator.RecordMachineEdit;
                        //GoTo(_viewLocator.RecordMachineEdit);
                        break;
                    case VToken.RecordBonding:
                        break;
                    case VToken.RecordBondingEdit:
                        break;
                    case VToken.MaterialOrder:
                        //GoTo(_viewLocator.MaterialOrder);
                        break;
                    case VToken.MaterialOrderEdit:
                        //_viewModelLocator.MaterialOrderEdit.SetKeyProperties(model.MsgModel);
                        //_viewLocator.MaterialOrderEdit.DataContext = _viewModelLocator.MaterialOrderEdit;
                        //GoTo(_viewLocator.MaterialOrderEdit);
                        break;
                    case VToken.MaterialOrderItemEdit:
                        //_viewModelLocator.MaterialOrderItemEdit.SetKeyProperties(model.MsgModel);
                        //_viewLocator.MaterialOrderItemEdit.DataContext = _viewModelLocator.MaterialOrderItemEdit;
                        //GoTo(_viewLocator.MaterialOrderItemEdit);
                        break;
                    case VToken.MaterialNeed:
                        //GoTo(_viewLocator.MaterialNeed);
                        break;
                    case VToken.MaterialNeedEdit:
                        //_viewModelLocator.MaterialNeedEdit.SetKeyProperties(model.MsgModel);
                        //_viewLocator.MaterialNeedEdit.DataContext = _viewModelLocator.MaterialNeedEdit;
                        //GoTo(_viewLocator.MaterialNeedEdit);
                        break;
                    case VToken.MaterialNeedSelect:
                        //_viewLocator.MaterialNeedSelect.DataContext = new MaterialNeedSelectVM(model.MsgModel);
                        //GoTo(_viewLocator.MaterialNeedSelect);
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


    }
}
