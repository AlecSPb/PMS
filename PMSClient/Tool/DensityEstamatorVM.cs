using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.Tool.Models;
using System.Collections.ObjectModel;

namespace PMSClient.Tool
{
    public class DensityEstamatorVM : ViewModelBase
    {
        public DensityEstamatorVM()
        {
            GraphicPapers = new ObservableCollection<GraphicPaper>();
            GraphicPapers.Clear();
            GraphicPaperCollections.Papers.ForEach(i => GraphicPapers.Add(i));


            Save = new RelayCommand(ActionSave);
            GiveUp = new RelayCommand(GoBack);
        }

        private void ActionSave()
        {
            switch (requestView)
            {
                case PMSViews.RecordDeMoldEdit:
                    PMSHelper.ViewModels.RecordDeMoldEdit.SetDensity(1, 1);
                    break;
                default:
                    break;
            }
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
        public ObservableCollection<GraphicPaper> GraphicPapers { get; set; }

        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
    }
}
