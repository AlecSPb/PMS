using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System.Collections.ObjectModel;


namespace PMSClient.ViewModel
{
    public class MaterialNeedEditVM : BaseViewModelEdit
    {
        public MaterialNeedEditVM()
        {
            InitializeBasicData();
            InitialCommands();
        }

        public void SetNew()
        {

            var empty = new DcMaterialNeed();
            #region 初始化
            empty.Id = Guid.NewGuid();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.State = PMSCommon.SimpleState.正常.ToString();
            empty.Composition = "需求原料成分";
            empty.PMINumber = Helpers.DefaultHelper.DefaultPMINumber();
            empty.Purity = "5N";
            empty.Weight = 1;
            empty.HowManyTargets = "1pcs";
            #endregion
            IsNew = true;
            CurrentMaterialNeed = empty;
        }

        public void SetDuplicate(DcMaterialNeed model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentMaterialNeed = model;
                CurrentMaterialNeed.Id = Guid.NewGuid();
                CurrentMaterialNeed.CreateTime = DateTime.Now;
                CurrentMaterialNeed.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentMaterialNeed.State = PMSCommon.SimpleState.正常.ToString();
            }
        }

        public void SetEdit(DcMaterialNeed model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentMaterialNeed = model;
            }
        }

        public void SetBySelect(PMSClient.NewService.DcOrder order)
        {
            if (order != null)
            {
                CurrentMaterialNeed.Composition = order.CompositionStandard;
                CurrentMaterialNeed.PMINumber = order.PMINumber;
                //RaisePropertyChanged(nameof(CurrentMaterialNeed));
            }
        }
        public void SetByCalculate(double weight)
        {
            //克到千克的转换
            CurrentMaterialNeed.Weight = weight / 1000;
            //RaisePropertyChanged(nameof(CurrentMaterialNeed));
        }

        private void InitializeBasicData()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);

            Calculator = new RelayCommand(ActionCalculator);
        }

        private void ActionCalculator()
        {
            PMSHelper.ToolViewModels.MaterialNeedCalcualtion.SetRequestView(PMSViews.MaterialNeedEdit);
            NavigationService.GoTo(PMSViews.MaterialNeedCalcuationTool);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.MissonSelect.SetRequestView(PMSViews.MaterialNeedEdit);
            NavigationService.GoTo(PMSViews.MissonSelect);
        }

        private void ActionSave()
        {
            if (CurrentMaterialNeed.Composition.Contains("Si"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[Si],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialNeed.Composition.Contains("O"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[O],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialNeed.Composition.Contains("B"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[B],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialNeed.Composition.Contains("C"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[C],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialNeed.Composition.Contains("F"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[F],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialNeed.Composition.Contains("Na"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[Na],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialNeed.Composition.Contains("K"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[K],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialNeed.Composition.Contains("Cs"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[Cs],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialNeed.Composition.Contains("Cl"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[Cl],确定使用这个成分？"))
                {
                    return;
                };
            }

            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentMaterialNeed.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }

            try
            {
                var service = new MaterialNeedServiceClient();
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                if (IsNew)
                {
                    service.AddMaterialNeedByUID(CurrentMaterialNeed, uid);
                }
                else
                {
                    service.UpdateMaterialNeedByUID(CurrentMaterialNeed, uid);
                }
                service.Close();
                PMSHelper.ViewModels.MaterialNeed.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.MaterialNeed);
        }

        private DcMaterialNeed currentMaterialNeed;
        public DcMaterialNeed CurrentMaterialNeed
        {
            get { return currentMaterialNeed; }
            set
            {
                currentMaterialNeed = value;
                RaisePropertyChanged(nameof(CurrentMaterialNeed));
            }
        }
        public List<string> States { get; set; }

        public RelayCommand Select { get; set; }

        public RelayCommand Calculator { get; set; }
    }
}
