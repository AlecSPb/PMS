using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDesktopClient.View;

namespace PMSDesktopClient
{
    /// <summary>
    /// 视图类类名集合
    /// </summary>
    public static class VNCollection
    {

        public static string MaterialOrderItemEdit
        {
            get
            {
                return "MaterialOrderItemEditView";
            }
        }

        public static string MaterialInventory
        {
            get
            { return nameof(MaterialInventoryView); }
        }
        public static string MaterialNeed
        {
            get
            { return nameof(MaterialNeedView); }
        }

        public static string MaterialNeedEdit
        {
            get
            {
                return nameof(MaterialNeedEditView);
            }
        }
        public static string MaterialOrder
        {
            get
            { return nameof(MaterialOrderView); }
        }

        public static string MaterialOrderEdit
        {
            get
            {
                return nameof(MaterialOrderEditView);
            }
        }
        public static string Order
        {
            get { return nameof(OrderView); }
        }

        public static string OrderEdit
        {
            get { return nameof(OrderEditView); }
        }

        public static string OrderSelect
        {
            get { return nameof(OrderSelectView); }
        }

        public static string Plan
        {
            get { return nameof(PlanView); }
        }

        public static string OrderCheck
        {
            get
            {
                return nameof(OrderCheckView);
            }
        }
        public static string OrderCheckEdit
        {
            get
            {
                return nameof(OrderCheckEditView);
            }
        }

        public static string LogIn
        {
            get
            {
                return nameof(LogInView);
            }
        }

        public static string Navigation
        {
            get
            {
                return nameof(NavigationView);
            }
        }

        public static string Misson
        {
            get
            {
                return nameof(MissonView);
            }
        }


        public static string RecordTestResult
        {
            get
            {
                return nameof(RecordTestResultView);
            }
        }

        public static string RecordTestResultEdit
        {
            get
            {
                return nameof(RecordTestResultEditView);
            }
        }



    }
}
