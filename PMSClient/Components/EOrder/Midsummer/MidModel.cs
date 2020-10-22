using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.EOrder.Midsummer
{
    public class MidModel
    {
        public MidModel()
        {
            Items = new List<MidModelItem>();
        }
        public string SoftwareManufacturer { get; set; }
        public string OrderNumber { get; set; }

        public string SupplierCodeEdi { get; set; }
        public string Supplier_Name { get; set; }
        public string Supplier_StreetBox1 { get; set; }
        public string Supplier_StreatBox2 { get; set; }
        public string Supplier_ZipCity1 { get; set; }
        public string Supplier_ZipCity2 { get; set; }
        public string Supplier_Country { get; set; }

        public string Buyer_Name { get; set; }
        public string Buyer_StreetBox1 { get; set; }
        public string Buyer_StreatBox2 { get; set; }
        public string Buyer_ZipCity1 { get; set; }
        public string Buyer_ZipCity2 { get; set; }
        public string Buyer_Country { get; set; }


        public DateTime OrderDate { get; set; }
        public int TermsOfPaymentDays { get; set; }

        public string Currency { get; set; }


        public List<MidModelItem> Items { get; set; }
    }
}
