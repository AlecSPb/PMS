using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.Tool.Models;
using System.Collections.ObjectModel;
using PMSClient.MainService;

namespace PMSClient.Tool
{
    public class DensityEstamatorVM : ViewModelBase
    {
        public DensityEstamatorVM()
        {
            GraphitePapers = new ObservableCollection<GraphitePaper>();
            GraphitePapers.Clear();
            GraphicPaperCollections.Papers.ForEach(i => GraphitePapers.Add(i));

            CurrentCalculationItem = new DensityEstamatorModel();

            Save = new RelayCommand(ActionSave);
            GiveUp = new RelayCommand(GoBack);
            SelectionChanged = new RelayCommand<GraphitePaper>(ActionSelecitonChanged);
        }

        private void ActionSelecitonChanged(GraphitePaper model)
        {
            if (model!=null)
            {
                CurrentCalculationItem.GraphiteDiameter = model.Diameter;
                CurrentCalculationItem.GraphiteThickness = model.Thickness;
                CurrentCalculationItem.GraphiteWeight = model.Weight;
                CalculateDensity();
            }
        }

        public void CalculateDensity()
        {
            var t = CurrentCalculationItem;
            double diameterAverage = UsefulPackage.PMSTranslate.Average(t.D1, t.D2);
            double thicknessAverage = UsefulPackage.PMSTranslate.Average(t.T1, t.T2, t.T3, t.T4);
            t.TAverage = thicknessAverage - t.GraphiteThickness;

            double volumn = Math.PI * diameterAverage * diameterAverage * thicknessAverage / 4 / 1000;
            double targetWeight = t.Weight - t.GraphiteWeight;
            if (t.CalculateDensity > 0 && volumn > 0)
            {
                t.Density = targetWeight / volumn;
                t.RatioDensity = t.Density / t.CalculateDensity;
            }
            else
            {
                t.Density = 0;
                t.RatioDensity = 0;
            }
            CurrentCalculationItem.RaiseAll();
        }

        private void ActionSave()
        {
            switch (requestView)
            {
                case PMSViews.RecordDeMoldEdit:
                    PMSHelper.ViewModels.RecordDeMoldEdit.SetDensity(CurrentCalculationItem.Density, CurrentCalculationItem.RatioDensity);
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

        private PMSViews requestView;

        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }

        public void SetCalculationItem(DcRecordDeMold model)
        {
            if (model!=null)
            {
                CurrentCalculationItem.Weight = model.Weight;
                CurrentCalculationItem.CalculateDensity = model.CalculationDensity;
                CurrentCalculationItem.D1 = model.Diameter1;
                CurrentCalculationItem.D2 = model.Diameter2;
                CurrentCalculationItem.T1 = model.Thickness1;
                CurrentCalculationItem.T2 = model.Thickness2;
                CurrentCalculationItem.T3 = model.Thickness3;
                CurrentCalculationItem.T4 = model.Thickness4;
                
            }
        }

        private DensityEstamatorModel currentCalculationItem;

        public DensityEstamatorModel CurrentCalculationItem
        {
            get { return currentCalculationItem; }
            set { currentCalculationItem = value; RaisePropertyChanged(nameof(CurrentCalculationItem)); }
        }


        public RelayCommand<GraphitePaper> SelectionChanged { get; set; }
        public ObservableCollection<GraphitePaper> GraphitePapers { get; set; }

        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
    }
}
