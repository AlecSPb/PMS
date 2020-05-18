using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PMSEOrder.Service
{
    public class BackupService
    {
        public static void BackUp()
        {
            try
            {
                string dbFolder = XSHelper.XS.File.GetCurrentFolderPath("DB");
                string sourceFile = Path.Combine(dbFolder, "pmieorder.db");
                string targetFile = Path.Combine(dbFolder, "AutoBackUp", "pmieorder.db");
                if (File.Exists(sourceFile))
                {
                    File.Copy(sourceFile, targetFile, true);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
