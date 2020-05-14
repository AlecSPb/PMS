using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using Dapper;
using Dapper.Contrib;
using PMSEOrder.Model;
using System.IO;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using XSHelper;

namespace PMSEOrder.Service
{
    /// <summary>
    /// Sqlite数据库服务
    /// </summary>
    public class DataService
    {

        public DataService()
        {
            string dbPath = Path.Combine(XSHelper.XS.File.GetCurrentFolderPath("DB"), "pmieorder.db");
            conStr = $"Data Source={dbPath};Version=3";
        }
        private string conStr;
        public List<Order> GetOrders(string customername, string composition, string po)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(conStr))
                {
                    connection.Open();
                    System.Diagnostics.Debug.Write(connection.State);
                    /*
                     * LIKE '%@Title%' 会解析成'%'@Title'%' 这里用拼接也是不行的'%'+@Title+'%'
                     */
                    string sql = "select * from localorder where customername like @ct and composition like @cp and po like @po";
                    var parameters = new { 
                        ct = $"%{customername}%", 
                        cp = $"%{composition}%", 
                        po = $"%{po}%"
                    };
                    var result = connection.Query<Order>(sql, parameters).ToList();
                    connection.Close();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Order> GetAllOrder()
        {
            using (IDbConnection connection=new SQLiteConnection(conStr))
            {
                return connection.GetAll<Order>().OrderByDescending(i=>i.CreateTime).ToList();
            }
        }

        public void AddOrder(Order model)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(conStr))
                {
                    connection.Insert<Order>(model);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateOrder(Order model)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(conStr))
                {
                    connection.Update<Order>(model);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 从json文件获取customer
        /// </summary>
        public List<string> GetCustomer()
        {
            string jsonStr = XS.File.ReadText(XS.File.GetCurrentFolderPath("DB") + "\\customer.json");
            var customers = JsonConvert.DeserializeObject<List<CustomerInfo>>(jsonStr);

            return customers.Select(i => i.CustomerName)
                            .OrderBy(i=>i)
                            .ToList();
        }

    }
}
