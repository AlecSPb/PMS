using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;



namespace PMSLastOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            string conStr = "server=192.168.16.254;database=pms;uid=sa;pwd=newlifechou;";
            SqlConnection conn = new SqlConnection(conStr);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select distinct customername from pmsorders order by customername";

            conn.Open();
            SqlDataReader srd = cmd.ExecuteReader();

            List<Model> customernames = new List<Model>();
            while (srd.Read())
            {
                customernames.Add(new Model { CustomerName = srd.GetString(0) });
            }

            srd.Close();
            foreach (var item in customernames)
            {
                cmd.CommandText = "select top 1 createtime from pmsorders where customername=@c order by createtime desc";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@c", item.CustomerName);

                DateTime lastorderdate = (DateTime)cmd.ExecuteScalar();
                item.LastOrderDate = lastorderdate;
            }

            conn.Close();

            StringBuilder sb = new StringBuilder();
            foreach (var item in customernames)
            {
                Console.WriteLine($"{item.CustomerName}:{item.LastOrderDate.ToShortDateString()}");
                sb.AppendLine($"{item.CustomerName},{item.LastOrderDate.ToShortDateString()}");              
            }


            File.WriteAllText("data.txt", sb.ToString());

            Console.WriteLine(customernames.Count);

            Console.Read();
        }
    }
}
