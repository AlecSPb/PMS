using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.ViewModel;
using PMSClient.ViewForDesktop;
using PMSClient.ViewForTablet;


namespace PMSClient
{
    /// <summary>
    /// 提供本项目各类视图和视图模型类的引用
    /// </summary>
    public static class NavigationCollection
    {
        private static DesktopViewLocator _desktopViews;
        private static TabletViewLocator _tabletViews;

        private static ViewModelLocator _viewModels;
        static NavigationCollection()
        {
            _desktopViews = new DesktopViewLocator();
            _tabletViews = new TabletViewLocator();
            _viewModels = (App.Current as App).FindResource("Locator") as ViewModelLocator;

        }

        public static DesktopViewLocator DesktopViews
        {
            get { return _desktopViews; }
        }
        public static TabletViewLocator TabletViews
        {
            get { return _tabletViews; }
        }

        public static ViewModelLocator ViewModels
        {
            get { return _viewModels; }
        }
    }
}
