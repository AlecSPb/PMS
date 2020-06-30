using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSShipment.VMHelper
{
    /// <summary>
    /// 分页视图的基类
    /// </summary>
    public class BaseViewModelPage : ViewModelBase
    {

        public BaseViewModelPage()
        {
            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(PMSViews.Navigation));
        }

        #region PageRelated

        private int pageIndex;
        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                RaisePropertyChanged(nameof(PageIndex));
            }
        }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                RaisePropertyChanged(nameof(PageSize));
            }
        }

        private int recordCount;
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                //TODO:这一块暂时先用这样的办法吧，分页控件中，重新设置参数在改变RecordCount的时候发生，必须让RecordCount改变一下
                recordCount = 0;
                RaisePropertyChanged(nameof(RecordCount));

                recordCount = value;
                RaisePropertyChanged(nameof(RecordCount));
            }
        }
        public RelayCommand PageChanged { get; set; }
        #endregion

        #region PublicCommands
        /// <summary>
        /// 搜索
        /// </summary>
        public RelayCommand Search { get; set; }
        /// <summary>
        /// 搜索所有全部
        /// </summary>
        public RelayCommand All { get; set; }
        /// <summary>
        /// 返回导航页
        /// </summary>
        public RelayCommand GoToNavigation { get; private set; }
        #endregion
    }
}
