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


            Messenger.Default.Register<MsgObject>(this, NavigationToken.Navigate,ActionNavigate);
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
                //case VT.PlanSelect:
                //    NavigateTo(new PlanSelectView());
                //    break;
                //case VT.Misson:
                //    NavigateTo(new MissonView());
                //    break;
                //case VT.Plan:
                //    NavigateTo(new PlanView());
                //    break;
                //case VT.RecordVHP:
                //    NavigateTo(new RecordVHPView());
                //    break;
                //case VT.RecordTestResult:
                //    NavigateTo(new RecordTestResultView());
                //    break;
                //case VT.RecordDelivery:
                //    NavigateTo(new DeliveryView());
                //    break;
                //case VT.MaterialNeed:
                //    NavigateTo(new MaterialNeedView());
                //    break;
                //case VT.MaterialOrder:
                //    NavigateTo(new MaterialOrderView());
                //    break;
                //case VT.RecordTestSelect:
                //    NavigateTo(new RecordTestResultSelectView());
                //    break;






                //case "OrderEditView":
                //    var view = new OrderEditView();
                //    var vm = new OrderEditVM(obj.ModelObject as DcOrder, obj.IsAdd);
                //    view.DataContext = vm;
                //    NavigateTo(view);
                //    break;
                //case "OrderCheckEditView":
                //    var view2 = new OrderCheckEditView();
                //    var vm2 = new OrderCheckEditVM(obj.ModelObject as DcOrder, obj.IsAdd);
                //    view2.DataContext = vm2;
                //    NavigateTo(view2);
                //    break;
                //case "PlanEditView":
                //    var planEditView = new PlanEditView();
                //    var planEditVM = new PlanEditVM(obj.ModelObject as DcPlanVHP, obj.IsAdd);
                //    planEditView.DataContext = planEditVM;
                //    NavigateTo(planEditView);
                //    break;
                //case "MaterialNeedEditView":
                //    var materialNeedEditView = new MaterialNeedEditView();
                //    var materialNeedEditVM = new MaterialNeedEditVM(obj);
                //    materialNeedEditView.DataContext = materialNeedEditVM;
                //    NavigateTo(materialNeedEditView);
                //    break;
                //case "MaterialOrderEditView":
                //    var view6 = new MaterialOrderEditView();
                //    var vm6 = new MaterialOrderEditVM(obj);
                //    view6.DataContext = vm6;
                //    NavigateTo(view6);
                //    break;
                //case "OrderSelectView":
                //    var view5 = new OrderSelectView();
                //    var vm5 = new OrderSelectMaterialNeed(obj);
                //    view5.DataContext = vm5;
                //    NavigateTo(view5);
                //    break;

                //case "MaterialOrderItemEdit":
                //    var view7 = new MaterialOrderItemEditView();
                //    var vm7 = new MaterialOrderItemEditVM(obj.Model);
                //    view7.DataContext = vm7;
                //    NavigateTo(view7);
                //    break;

                //case "MaterialNeedSelect":
                //    var selectView2 = new MaterialNeedSelectView();
                //    var selectVM2 = new MaterialNeedSelectVM(obj.Model);
                //    selectView2.DataContext = selectVM2;
                //    NavigateTo(selectView2);
                //    break;

                //case "RecordTestResultEdit":
                //    var view8 = new RecordTestResultEditView();
                //    var vm8 = new RecordTestResultEditVM(obj.Model);
                //    view8.DataContext = vm8;
                //    NavigateTo(view8);
                //    break;
                //case "RecordDeliveryEdit":
                //    var view9 = new RecordDeliveryEditView();
                //    var vm9 = new RecordDeliveryEditVM(obj.Model);
                //    view9.DataContext = vm9;
                //    NavigateTo(view9);
                //    break;
                default:
                    break;
            }
        }


        private void NavigateTo(UserControl view)
        {
            if (view!=null)
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
