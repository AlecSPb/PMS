using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFControls
{
    public class NormalizedDataHelper
    {
        public static List<string> Analysis(string s)
        {
            try
            {
                string[] data = s.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                return data.ToList().Select(i => i.Trim()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
