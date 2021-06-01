using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using PMSDAL;
using System.IO;

namespace MaterialOrderOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            string conStr = "server=192.168.16.254;uid=sa;pwd=newlifechou;database=PMS";
            string sqlMaterialOrder = "select * from materialorders where createtime >= '2021-5-1' and createtime <='2021-6-1' and state !='作废' order by createtime desc";
            string sqlMaterialOrderItem = "select * from materialorderitems where materialorderid=@id and state !='作废' order by pminumber";

            using (var conn=new SqlConnection(conStr))
            {
                var materialOrder = conn.Query<MaterialOrder>(sqlMaterialOrder);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("CreateTime,OrderPO,PMINumber,Weight,Remark");
                foreach (var item in materialOrder)
                {
                    sb.Append(item.CreateTime);
                    sb.Append(",");
                    sb.Append(item.OrderPO);
                    sb.Append(",");

                    StringBuilder sbb = new StringBuilder();

                    var materialOrderItems = conn.Query<MaterialOrderItem>(sqlMaterialOrderItem,new { id=item.ID});

                    double weight = materialOrderItems.Sum(i => i.Weight);

                    foreach (var i in materialOrderItems)
                    {
                        sbb.Append(i.PMINumber);
                        sbb.Append(" ");
                    }

                    sb.Append(sbb.ToString());

                    sb.Append(",");

                    sb.Append(weight.ToString());

                    sb.Append(",");

                    sb.AppendLine(item.Remark);

                }


                File.WriteAllText("Results.csv",sb.ToString());
                System.Diagnostics.Process.Start("Results.csv");


                Console.WriteLine("OK");

            }


            Console.Read();
        }
    }
}
