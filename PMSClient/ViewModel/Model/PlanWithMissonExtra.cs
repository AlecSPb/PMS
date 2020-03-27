using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.NewService;

namespace PMSClient.ViewModel.Model
{
    public class PlanWithMissonExtra
    {
        public PlanWithMissonExtra()
        {
            IsSelected = false;
        }
        public bool IsSelected { get; set; }

        public DcPlanExtra PlanMisson { get; set; }
    }
}
