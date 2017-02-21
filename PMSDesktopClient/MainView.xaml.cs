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
            views = new ViewLocator();
            viewmodels = new ViewModelLocator();



            Messenger.Default.Register<MsgObject>(this, NavigationToken.Navigate, ActionNavigate);
            NavigateTo(views.Navigation);
        }


        private ViewLocator views;//TODO:考虑将viewlocator化？
        private ViewModelLocator viewmodels;

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
                case VToken.Misson:
                    NavigateTo(views.Misson);
                    break;
                case VToken.Plan:
                    NavigateTo(views.Plan);
                    break;
                case VToken.RecordTestResult:
                    NavigateTo(views.RecordTestResult);
                    break;
                case VToken.RecordDelivery:
                    NavigateTo(views.RecordDelivery);
                    break;
                case VToken.RecordVHP:
                    NavigateTo(views.RecordVHP);
                    break;

                case VToken.MaterialNeed:
                    NavigateTo(views.MaterialNeed);
                    break;
                case VToken.MaterialOrder:
                    NavigateTo(views.MaterialOrder);
                    break;
                case VToken.RecordTestResultSelect:
                    var viewRecordTestResultSelect = views.RecordTestResultSelect;
                    viewRecordTestResultSelect.DataContext = new RecordTestResultSelectVM(msg.MsgModel);
                    NavigateTo(viewRecordTestResultSelect);
                    break;

                case VToken.PlanSelectForTestResult:
                    var planselect1 = views.PlanSelect;
                    planselect1.DataContext = viewmodels.PlanSelectForRecordTestResult;
                    NavigateTo(planselect1);
                    break;
                case VToken.PlanSelectForVHP:
                    var planselect2 = views.PlanSelect;
                    planselect2.DataContext = viewmodels.PlanSelectForRecordVHP;
                    NavigateTo(planselect2);
                    break;

                case VToken.RecordVHPEdit:
                    var recordvhpeditview = views.RecordVHPEdit;
                    recordvhpeditview.DataContext = new RecordVHPEditVM(msg.MsgModel);
                    NavigateTo(recordvhpeditview);
                    break;

                case VToken.RecordVHPQuickEdit:
                    NavigateTo(views.RecordVHPQuickEdit);
                    break;


                case VToken.OrderEdit:
                    var view = views.OrderEdit;
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
                    var view7 = views.MaterialOrderItemEdit;
                    view7.DataContext = new MaterialOrderItemEditVM(msg.MsgModel);
                    NavigateTo(view7);
                    break;

                case VToken.MaterialNeedSelect:
                    var selectView2 = views.MaterialNeedSelect;
                    selectView2.DataContext = new MaterialNeedSelectVM(msg.MsgModel); ;
                    NavigateTo(selectView2);
                    break;

                case VToken.RecordTestResultEdit:
                    var view8 = views.RecordTestResultEdit;
                    view8.DataContext = new RecordTestResultEditVM(msg.MsgModel);
                    NavigateTo(view8);
                    break;
                case VToken.RecordDeliveryEdit:
                    var view9 = views.RecordDeliveryEdit;
                    view9.DataContext = new RecordDeliveryEditVM(msg.MsgModel);
                    NavigateTo(view9);
                    break;
                case VToken.RecordDeliveryItemEdit:
                    var view10 = views.RecordDeliveryItemEdit;
                    view10.DataContext = new RecordDeliveryItemEditVM(msg.MsgModel);
                    NavigateTo(view10);
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
