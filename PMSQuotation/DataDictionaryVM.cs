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
            dict_servce = new DataDictionaryService();

            DataDicts = new ObservableCollection<DataDict>();



            Save = new RelayCommand(ActionSave);
            ToRMB = new RelayCommand(ActionRMB);
            ToUSD = new RelayCommand(ActionUSD);

            LoadDataDicts();
        }

        private void ActionUSD()
        {
            if (XSHelper.XS.MessageBox.ShowYesNo("Are you going to change all setting value to USD in batch mode?"))
            {
                string current_currency = dict_servce.GetString("basecurrency");
                if (current_currency == "USD")
                {
                    XSHelper.XS.MessageBox.ShowInfo("It is USD now,no need to change anything");
                    return;
                }
                else
                {
                    double currency_rate = dict_servce.GetDouble("rmb_usd_exchange_rate");

                    #region MyRegion

                    ChangeDataDicts("material_price_rule", currency_rate, true);
                    ChangeDataDicts("powder_price_rule", currency_rate, true);
                    ChangeDataDicts("machine_price_rule", currency_rate, true);
                    ChangeDataDicts("vhp_price_rule", currency_rate, true);
                    ChangeDataDicts("bonding_price_rule", currency_rate, true);
                    ChangeDataDicts("analysis_price_rule", currency_rate, true);
                    ChangeDataDicts("shipping_fee", currency_rate, true);
                    ChangeDataDicts("package_fee", currency_rate, true);
                    ChangeDataDicts("custom_fee", currency_rate, true);

                    SetDataDicts("basecurrency", "USD");
                    #endregion

                    SaveAndRefresh();

                    XSHelper.XS.MessageBox.ShowInfo("it is USD now");
                }
            }
        }

        private void ActionRMB()
        {
            if (XSHelper.XS.MessageBox.ShowYesNo("Are you going to change all setting value to RMB in batch mode?"))
            {
                string current_currency = dict_servce.GetString("basecurrency");
                if (current_currency == "RMB")
                {
                    XSHelper.XS.MessageBox.ShowInfo("It is RMB now,no need to change anything");
                    return;
                }
                else
                {
                    double currency_rate = dict_servce.GetDouble("rmb_usd_exchange_rate");

                    #region MyRegion

                    ChangeDataDicts("material_price_rule", currency_rate, false);
                    ChangeDataDicts("powder_price_rule", currency_rate, false);
                    ChangeDataDicts("machine_price_rule", currency_rate, false);
                    ChangeDataDicts("vhp_price_rule", currency_rate, false);
                    ChangeDataDicts("bonding_price_rule", currency_rate, false);
                    ChangeDataDicts("analysis_price_rule", currency_rate, false);
                    ChangeDataDicts("shipping_fee", currency_rate, false);
                    ChangeDataDicts("package_fee", currency_rate, false);
                    ChangeDataDicts("custom_fee", currency_rate, false);

                    SetDataDicts("basecurrency", "RMB");
                    #endregion

                    SaveAndRefresh();

                    XSHelper.XS.MessageBox.ShowInfo("it is RMB now");
                }
            }
        }

        private void SaveAndRefresh()
        {
            foreach (var item in DataDicts)
            {
                item.LastUpdateTime = DateTime.Now;
                db_service.UpdateDataDict(item);
            }
            LoadDataDicts();
        }

        private void SetDataDicts(string key, string value)
        {
            DataDicts.Where(i => i.DataKey == key).FirstOrDefault().DataValue = value;
        }

        private void ChangeDataDicts(string key, double currency_rate, bool to_usd = false)
        {
            var item = DataDicts.Where(i => i.DataKey == key).FirstOrDefault();
            string s = item.DataValue;

            string[] groups = s.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (groups.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var group in groups)
                {
                    string[] strs = group.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (strs.Length >= 2)
                    {
                        string t_key = strs[0];
                        double t_value = 0;
                        double.TryParse(strs[1], out t_value);

                        sb.Append(t_key);
                        sb.Append("=");
                        double new_t_key = 0;
                        if (to_usd && currency_rate != 0)
                        {
                            new_t_key = t_value / currency_rate;
                        }
                        else
                        {
                            new_t_key = t_value * currency_rate;
                        }
                        sb.Append(new_t_key.ToString("F2"));
                        sb.Append(";");
                    }
                }
                item.DataValue = sb.ToString();
            }
        }

        private void LoadDataDicts()
        {
            var result = db_service.GetDataDicts();

            DataDicts.Clear();
            foreach (var item in result)
            {
                DataDicts.Add(item);
                if (item.DataKey == "basecurrency")
                {
                    CalculationCurrency = item.DataValue;
                }
            }
        }

        private QuotationDbService db_service;
        private DataDictionaryService dict_servce;
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

        private string calculationCurrency;

        public string CalculationCurrency
        {
            get { return calculationCurrency; }
            set { calculationCurrency = value; RaisePropertyChanged(nameof(CalculationCurrency)); }
        }


        public ObservableCollection<DataDict> DataDicts { get; set; }


        public RelayCommand Save { get; set; }
        public RelayCommand ToRMB { get; set; }
        public RelayCommand ToUSD { get; set; }



    }
}
