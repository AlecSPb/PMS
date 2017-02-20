using PMSDesktopClient.View;
using PMSDesktopClient.ViewModel;


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
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSDesktopClient.PMSMainService;



namespace PMSDesktopClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            views = new ViewInstance();

            Messenger.Default.Register<MsgObject>(this, NavigationToken.Navigate, ActionNavigate);
            NavigateTo(views.Navigation);
        }


        private ViewInstance views;


        private void ActionNavigate(MsgObject msg)
        {
            switch (msg.MsgToken)
            {
                case VToken.Navigation:
                    NavigateTo(views.Navigation);
                    break;
                case VToken.Order:
                    NavigateTo(views.Order);
                    break;
                case VToken.OrderCheck:
                    NavigateTo(views.OrderCheck);
                    break;
                case VToken.PlanSelect:
                    NavigateTo(new PlanSelectView());
                    break;
                case VToken.Misson:
                    NavigateTo(views.Misson);
                    break;
                case VToken.Plan:
                    NavigateTo(views.Plan);
                    break;
                //case VToken.RecordVHP:
                //    NavigateTo(views.rec);
                //    break;
                case VToken.RecordTestResult:
                    NavigateTo(views.RecordTestResult);
                    break;
                case VToken.RecordDelivery:
                    //NavigateTo(new DeliveryView());
                    break;
                case VToken.MaterialNeed:
                    NavigateTo(views.MaterialNeed);
                    break;
                case VToken.MaterialOrder:
                    NavigateTo(views.MaterialOrder);
                    break;
                case VToken.RecordTestSelect:
                    NavigateTo(views.RecordTestResult);
                    break;






                case VToken.OrderEdit:
                    var view = new OrderEditView();
                    var vm = new OrderEditVM(msg.MsgModel);
                    view.DataContext = vm;
                    NavigateTo(view);
                    break;
                case VToken.OrderCheckEdit:
                    var view2 = new OrderCheckEditView();
                    var vm2 = new OrderCheckEditVM(msg.MsgModel);
                    view2.DataContext = vm2;
                    NavigateTo(view2);
                    break;
                case VToken.PlanEdit:
                    var planEditView = new PlanEditView();
                    var planEditVM = new PlanEditVM(msg.MsgModel);
                    planEditView.DataContext = planEditVM;
                    NavigateTo(planEditView);
                    break;
                case VToken.MaterialNeedEdit:
                    var materialNeedEditView = views.MaterialNeedEdit;
                    materialNeedEditView.DataContext = new MaterialNeedEditVM(msg.MsgModel);
                    NavigateTo(materialNeedEditView);
                    break;
                case VToken.MaterialOrderEdit:
                    var view6 = views.MaterialOrderEdit;
                    view6.DataContext = new MaterialOrderEditVM(msg.MsgModel);
                    NavigateTo(view6);
                    break;

                case VToken.OrderSelect:  //TODO:这里考虑共用一个OrderSelectView
                    NavigateTo(views.OrderSelect);
                    break;

                case VToken.MaterialOrderItemEdit:
                    var view7 = new MaterialOrderItemEditView();
                    var vm7 = new MaterialOrderItemEditVM(msg.MsgModel);
                    view7.DataContext = vm7;
                    NavigateTo(view7);
                    break;

                case VToken.MaterialNeedSelect:
                    var selectView2 = views.MaterialNeedSelect;
                    selectView2.DataContext = new MaterialNeedSelectVM(msg.MsgModel); ;
                    NavigateTo(selectView2);
                    break;

                case VToken.RecordTestResultEdit:
                    var view8 = new RecordTestResultEditView();
                    var vm8 = new RecordTestResultEditVM(msg.MsgModel);
                    view8.DataContext = vm8;
                    NavigateTo(view8);
                    break;
                case VToken.RecordDeliveryEdit:
                    var view9 = new RecordDeliveryEditView();
                    var vm9 = new RecordDeliveryEditVM(msg.MsgModel);
                    view9.DataContext = vm9;
                    NavigateTo(view9);
                    break;
                default:
                    break;
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
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            //if (MessageBox.Show("Are you sure to quit?","Quit",MessageBoxButton.YesNo,MessageBoxImage.Warning)==MessageBoxResult.Yes)
            //{
            //    this.Close();
            //}
            this.Close();
        }
    }
}
