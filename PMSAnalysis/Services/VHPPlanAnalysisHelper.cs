using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PMSAnalysis.VHPPlan.Models;

namespace PMSAnalysis.Services
{
    public class VHPPlanAnalysisHelper
    {
        private string conStr = "server=192.168.16.254;database=PMS;uid=sa;pwd=newlifechou;";


        public List<AnalysisModel> GetAnalysis(DateTime start,DateTime end)
        {

            List<AnalysisModel> models = new List<AnalysisModel>();

            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandText = "select vhpdevicecode,plantype from pmsplanvhps where plandate=@plandate and state!='作废'";

            while (start <= end)
            {
                AnalysisModel model = new AnalysisModel();
                model.PlanDate = start.Date;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@plandate", start));

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string vhpcode = dr.GetString(0);
                    switch (vhpcode)
                    {
                        case "A":
                            model.A = CheckResult(start, dr.GetString(0), dr.GetString(1));
                            break;
                        case "B":
                            model.B = CheckResult(start, dr.GetString(0), dr.GetString(1));
                            break;
                        case "C":
                            model.C = CheckResult(start, dr.GetString(0), dr.GetString(1));
                            break;
                        case "D":
                            model.D = CheckResult(start, dr.GetString(0), dr.GetString(1));
                            break;
                        case "E":
                            model.E = CheckResult(start, dr.GetString(0), dr.GetString(1));
                            break;
                        case "F":
                            model.F = CheckResult(start, dr.GetString(0), dr.GetString(1));
                            break;
                        case "G":
                            model.G = CheckResult(start, dr.GetString(0), dr.GetString(1));
                            break;
                        default:
                            break;
                    }
                }

                dr.Close();
                models.Add(model);

                start = start.AddDays(1);
            }



            conn.Close();

            return models;
        }


        private PlanResult CheckResult(DateTime date, string vhpdevicecode, string plantype)
        {
            if (string.IsNullOrEmpty(vhpdevicecode))
                return PlanResult.Empty;

            if (plantype != "加工" && plantype!="外协")
                return PlanResult.W1;

            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select count(*) from deliveryitems where productid like '%'+@productid+'%' and state!='作废'";
            cmd.Parameters.Add(new SqlParameter("@productid", date.ToString("yyMMdd") + "-" + vhpdevicecode));
            conn.Open();
            int result = (int)cmd.ExecuteScalar();
            conn.Close();

            if (result > 0)
                return PlanResult.W2_Success;
            else
                return PlanResult.W2_Fail;

        }


    }
}
