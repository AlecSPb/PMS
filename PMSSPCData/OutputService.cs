using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace PMSSPCData
{
    public class OutputService
    {
        private string conStr = @"server=(local);database=192.168.16.254;uid=sa;pwd=newlifechou";

        public void GetDensityData()
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select density from recordtests where composition like '%Ga4.5%'";



        }





    }
}
