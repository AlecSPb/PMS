using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSQuotation.Models;
using Dapper;
using System.Data.SQLite;
using System.Data;
using System.Configuration;

namespace PMSQuotation.Services
{
    /// <summary>
    /// 报价服务类
    /// </summary>
    public class QuotationService
    {
        private string conn_str;
        public QuotationService()
        {
            conn_str = ConfigurationManager.ConnectionStrings["sqlDb"].ConnectionString;
        }

        public List<Quotation> GetQuotations(string customer,string title)
        {
            return null;
        }

        public void Add(Quotation model)
        {

        }

        public void Update(Quotation model)
        {

        }

        public List<Contacts> GetCustomerInfos()
        {
            IDbConnection conn = new SQLiteConnection(conn_str);
            conn.Open();
            string sql_cmd = "select * from customerinfos";
            var customerinfos = conn.Query<Contacts>(sql_cmd);
            conn.Close();
            return customerinfos.ToList();
        }

        public void Add(Contacts model)
        {

        }

        public void Update(Contacts model)
        {

        }

    }
}
