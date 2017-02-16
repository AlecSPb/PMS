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
            DataContext = new ViewModel.MainWindowVM();
            Messenger.Default.Register<string>(this, NavigationToken.Navigate, ActionNavigate);
            Messenger.Default.Register<MessageObject>(this, NavigationToken.Edit,ActionEdit);

            NavigateTo(new NavigationView());
        }

        private void ActionEdit(MessageObject obj)
        {
            switch (obj.ViewName)
            {
                case "OrderEditView":
                    var view = new OrderEditView();
                    var vm=new OrderEditVM(obj.ModelObject as DcOrder,obj.IsAdd);
                    view.DataContext = vm;
                    NavigateTo(view);
                    break;
                case "OrderCheckEditView":
                    var view2 = new OrderCheckEditView();
                    var vm2 = new OrderCheckEditVM(obj.ModelObject as DcOrder, obj.IsAdd);
                    view2.DataContext = vm2;
                    NavigateTo(view2);
                    break;
                case "PlanEditView":
                    var planEditView = new PlanEditView();
                    var planEditVM = new PlanEditVM(obj.ModelObject as DcPlanVHP,obj.IsAdd);
                    planEditView.DataContext = planEditVM;
                    NavigateTo(planEditView);
                    break;
                case "MaterialNeedEditView":
                    var materialNeedEditView = new MaterialNeedEditView();
                    var materialNeedEditVM = new MaterialNeedEditVM(obj);
                    materialNeedEditView.DataContext = materialNeedEditVM;
                    NavigateTo(materialNeedEditView);
                    break;
                case "OrderSelectView":
                    var view5 = new OrderSelectView();
                    var vm5 = new OrderSelectForMaterialNeedEditVM(obj);
                    view5.DataContext = vm5;
                    NavigateTo(view5);
                    break;
                default :
                    break;
            }
        }

        private void ActionNavigate(string viewName)
        {
            switch (viewName)
            {
                case "NavigationView":
                    NavigateTo(new NavigationView());
                    break;
                case "OrderView":
                    NavigateTo(new OrderView());
                    break;
                case "OrderEditView":
                    NavigateTo(new OrderEditView());
                    break;
                case "OrderCheckView":
                    NavigateTo(new OrderCheckView());
                    break;
                case "MissonView":
                    NavigateTo(new MissonView());
                    break;
                case "PlanView":
                    NavigateTo(new PlanView());
                    break;
                case "RecordVHPView":
                    NavigateTo(new RecordVHPView());
                    break;
                case "RecordTestResultView":
                    NavigateTo(new RecordTestResultView());
                    break;
                case "DeliveryView":
                    NavigateTo(new DeliveryView());
                    break;
                case "MaterialNeedView":
                    NavigateTo(new MaterialNeedView());
                    break;
                case "MaterialOrderView":
                    NavigateTo(new MaterialOrderView());
                    break;
                case "MaterialNeedEditView":
                    NavigateTo(new PlanView());
                    break;
                case "MaterialOrderEditView":
                    NavigateTo(new MaterialOrderView());
                    break;
                case "OrderSelectView":
                    NavigateTo(new OrderSelectView());
                    break;
                default:
                    NavigateTo(new NavigationView());
                    break;
            }
        }

        private void NavigateTo(UserControl view)
        {
            mainArea.Content = view;
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
