using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSQuotation.Models;
using PMSQuotation.Services;

namespace PMSQuotation
{
    public class DataDictionaryVM : ViewModelBase
    {
        public DataDictionaryVM()
        {
            db_service = new QuotationDbService();

            DataDicts = new ObservableCollection<DataDict>();



            Save = new RelayCommand(ActionSave);
            LoadDataDicts();
        }
        private void LoadDataDicts()
        {
            var result = db_service.GetDataDicts();

            DataDicts.Clear();
            foreach (var item in result)
            {
                DataDicts.Add(item);
            }
        }

        private QuotationDbService db_service;

        private void ActionSave()
        {
            if (DataDicts.Count > 0)
            {
                foreach (var item in DataDicts)
                {
                    item.LastUpdateTime = DateTime.Now;
                    db_service.UpdateDataDict(item);
                }
                XSHelper.XS.MessageBox.ShowInfo("Data Dict Saved Successful");
                Messenger.Default.Send(new NotificationMessage("CloseDataDictionaryWindow"), "MSG");

            }
        }

        public ObservableCollection<DataDict> DataDicts { get; set; }


        public RelayCommand Save { get; set; }

    }
}
