using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class BaseViewModelEdit : ViewModelBase
    {
        public BaseViewModelEdit()
        {
            //GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordMilling }));
        }
        #region Properties
        /// <summary>
        /// 新建还是编辑指示标志
        /// </summary>
        private string newOrEdit;
        public string NewOrEditIndicator
        {
            get { return newOrEdit; }
            set { newOrEdit = value; RaisePropertyChanged(nameof(NewOrEditIndicator)); }
        }

        #endregion

        #region Commands 
        /// <summary>
        /// 保存
        /// </summary>
        public RelayCommand Save { get; set; }
        /// <summary>
        /// 放弃返回
        /// </summary>
        public RelayCommand GiveUp { get; set; }
        #endregion
    }
}
