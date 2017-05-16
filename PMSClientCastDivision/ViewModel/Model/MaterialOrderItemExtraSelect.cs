using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.SanjieService;

namespace PMSClient.ViewModel.Model
{
   public class MaterialOrderItemExtraSelect
    {
        public MaterialOrderItemExtraSelect()
        {
            IsSelected = false;
        }
        public bool IsSelected { get; set; }
        public DcMaterialOrderItemExtra Item { get; set; }
    }
}
