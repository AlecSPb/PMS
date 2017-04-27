using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel.Model
{
    public class PlateExtra
    {
        public PlateExtra()
        {
            IsSelected = false;
        }
        public bool IsSelected { get; set; }
        public DcPlate Plate { get; set; }
    }
}
