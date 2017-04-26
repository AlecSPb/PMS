using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ViewModel.Model
{
    public class RecordBondingExtra
    {
        public bool IsSelected { get; set; }
        public DcRecordBonding RecordBonding { get; set; }
    }
}
