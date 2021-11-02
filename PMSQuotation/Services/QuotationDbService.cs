using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSQuotation.Models;
using Dapper;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace PMSQuotation.Services
{
    /// <summary>
    /// 报价服务类
    /// </summary>
    public class QuotationDbService
    {
        private string conn_str;
        public QuotationDbService()
        {
            string dbPath = Path.Combine(XSHelper.XS.File.GetCurrentFolderPath("DB"), "pmsquotation.db");
            conn_str = $"Data Source={dbPath};Version=3";
        }

        public List<Quotation> GetQuotations(string customer, string keyword, string state)
        {
            List<Quotation> models = new List<Quotation>();
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "select * from quotations where customer_companyname like @cc and keyword like @kw and state like @st" +
                    " order by createtime desc";
                var parameters = new
                {
                    cc = $"%{customer}%",
                    kw = $"%{keyword}%",
                    st = $"%{state}%"
                };
                var result = conn.Query<Quotation>(sql, parameters);
                models.Clear();
                models.AddRange(result);
            }


            return models;
        }

        public void Add(Quotation model)
        {

        }

        public void Update(Quotation model)
        {

        }





        public List<QuotationItem> GetQuotationItems(int quotationId)
        {

            List<QuotationItem> models = new List<QuotationItem>();
            using (IDbConnection conn = new SQLiteConnection(conn_str))
            {
                string sql = "select * from quotationitems where quotationid=@quotationid order by createtime desc";
                var parameters = new
                {
                    quotationid = quotationId
                };
                var result = conn.Query<QuotationItem>(sql, parameters);
                models.Clear();
                models.AddRange(result);
            }


            return models;

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
