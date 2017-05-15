using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight;
using PMSClient.Tool.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PMSClient.BasicService;

namespace PMSClient.Tool
{
    public class MaterialNeedCalcualtionVM : ViewModelBase
    {
        private const double defaultDensity = 5.75;
        public MaterialNeedCalcualtionVM()
        {
            Compounds = new List<DcBDCompound>();
            Molds = new List<DcBDVHPMold>();
            CalculationItems = new ObservableCollection<MaterialNeedCalculationItem>();
            IsDensityReadOnly = true;
            ReadOnlyButton = "手动输入密度";


            InitializeCurrentItem(defaultDensity);
            InitializeBasicData();

            Add = new RelayCommand(ActionAdd);
            Delete = new RelayCommand<MaterialNeedCalculationItem>(ActionDelete);
            GiveUp = new RelayCommand(GoBack);
            CompoundsSelectionChanged = new RelayCommand<DcBDCompound>(ActionCompoundSelectionChanged);
            MoldsSelectionChanged = new RelayCommand<DcBDVHPMold>(ActionMoldsSelectionChanged);
            Save = new RelayCommand(ActionSave);
            ManualInputDensity = new RelayCommand(ActionManulInputDensity);
        }

        private void ActionManulInputDensity()
        {
            SetReadOnlyDensity(!IsDensityReadOnly);
        }

        private void ActionSave()
        {
            switch (requestView)
            {
                case PMSViews.MaterialNeedEdit:
                    PMSHelper.ViewModels.MaterialNeedEdit.SetByCalculate(TotalWeight);
                    break;
                case PMSViews.MaterialOrderItemEdit:
                    PMSHelper.ViewModels.MaterialOrderItemEdit.SetByCalculate(TotalWeight);
                    break;
                case PMSViews.RecordMilling:
                    break;
                case PMSViews.RecordMillingEdit:
                    break;
                default:
                    break;
            }
            GoBack();
        }

        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private void ActionMoldsSelectionChanged(DcBDVHPMold model)
        {
            if (model != null && CurrentCalculationItem != null)
            {
                var newValue = new MaterialNeedCalculationItem();
                newValue.ID = Guid.NewGuid();
                newValue.Diameter = model.InnerDiameter;
                newValue.Thickness = CurrentCalculationItem.Thickness;
                newValue.Weight = CurrentCalculationItem.Weight;
                newValue.WeightLoss = CurrentCalculationItem.WeightLoss;
                newValue.Quantity = CurrentCalculationItem.Quantity;
                newValue.Remark = CurrentCalculationItem.Remark;
                CurrentCalculationItem = newValue;
            }
        }
        private void ActionCompoundSelectionChanged(DcBDCompound model)
        {
            if (model != null)
            {
                //每次更新密度，就清空所有计算项
                InitializeCurrentItem(model.Density);
            }
        }

        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
            //clear data
            InitializeCurrentItem(defaultDensity);
        }
        private void InitializeCurrentItem(double density)
        {
            CalculationItems.Clear();
            CurrentCalculationItem = new MaterialNeedCalculationItem()
            {
                ID = Guid.NewGuid(),
                Diameter = 233,
                Thickness = 5.5,
                Quantity = 1,
                Weight = 0,
                WeightLoss = 0
            };
            CurrentDensity = density;
            TotalWeight = 0;
        }

        private void InitializeBasicData()
        {
            try
            {
                Molds.Clear();
                PMSBasicDataService.VHPMolds.ToList().ForEach(i => Molds.Add(i));
                Compounds.Clear();
                PMSBasicDataService.Compounds.ToList().ForEach(i => Compounds.Add(i));
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionAdd()
        {
            if (CurrentCalculationItem != null)
            {
                var model = new MaterialNeedCalculationItem();
                model.ID = CurrentCalculationItem.ID;
                model.Diameter = CurrentCalculationItem.Diameter;
                model.Thickness = CurrentCalculationItem.Thickness;
                model.Quantity = CurrentCalculationItem.Quantity;
                model.WeightLoss = CurrentCalculationItem.WeightLoss;
                model.Remark = CurrentCalculationItem.Remark;

                CalculateCurrentWeight(model);

                CalculationItems.Add(model);

                CalcualteTotalWeight();

                SetReadOnlyDensity(true);
            }
        }

        private void SetReadOnlyDensity(bool flag)
        {
            if (flag)
            {
                IsDensityReadOnly = true;
                ReadOnlyButton = "手动输入密度";
            }
            else
            {
                IsDensityReadOnly = false;
                ReadOnlyButton = "关闭手动输入";
                InitializeCurrentItem(defaultDensity);
            }

        }


        private void ActionDelete(MaterialNeedCalculationItem item)
        {
            if (item != null)
            {
                CalculationItems.Remove(item);
                CalcualteTotalWeight();
            }
        }

        private void CalculateCurrentWeight(MaterialNeedCalculationItem item)
        {
            if (item != null)
            {
                item.Weight = Math.PI * item.Diameter * item.Diameter * item.Thickness / 4 / 1000 * CurrentDensity * item.Quantity + item.WeightLoss;
            }
        }
        private void CalcualteTotalWeight()
        {
            double result = 0;
            foreach (var item in CalculationItems)
            {
                result += item.Weight;
            }
            TotalWeight = result;
        }

        public RelayCommand Add { get; set; }
        public RelayCommand<MaterialNeedCalculationItem> Delete { get; set; }

        private MaterialNeedCalculationItem currentCalculationItem;

        public MaterialNeedCalculationItem CurrentCalculationItem
        {
            get { return currentCalculationItem; }
            set { currentCalculationItem = value; RaisePropertyChanged(nameof(CurrentCalculationItem)); }
        }

        private double currentDensity;

        public double CurrentDensity
        {
            get { return currentDensity; }
            set { currentDensity = value; RaisePropertyChanged(nameof(CurrentDensity)); }
        }
        private double totalWeight;
        public double TotalWeight
        {
            get { return totalWeight; }
            set
            {
                totalWeight = value;
                RaisePropertyChanged(nameof(TotalWeight));
            }
        }

        private bool isDensityReadOnly;

        public bool IsDensityReadOnly
        {
            get { return isDensityReadOnly; }
            set { isDensityReadOnly = value; RaisePropertyChanged(nameof(IsDensityReadOnly)); }
        }
        private string readonlyButton;

        public string ReadOnlyButton
        {
            get { return readonlyButton; }
            set { readonlyButton = value; RaisePropertyChanged(nameof(ReadOnlyButton)); }
        }


        public ObservableCollection<MaterialNeedCalculationItem> CalculationItems { get; set; }

        public List<DcBDCompound> Compounds { get; set; }
        public List<DcBDVHPMold> Molds { get; set; }


        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }

        public RelayCommand<DcBDCompound> CompoundsSelectionChanged { get; set; }
        public RelayCommand<DcBDVHPMold> MoldsSelectionChanged { get; set; }

        public RelayCommand ManualInputDensity { get; set; }
    }
}
