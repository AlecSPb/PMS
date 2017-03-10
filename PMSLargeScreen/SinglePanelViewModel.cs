using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSLargeScreen
{
    public class SinglePanelViewModel : ViewModelBase
    {
        private SinglePanelModel model;
        public SinglePanelModel Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(nameof(Model)); }
        }



    }
}
