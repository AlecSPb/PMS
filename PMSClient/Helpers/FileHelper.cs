using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace PMSClient.Helpers
{
    public static class FileHelper
    {
        public static void OpenFolder(string folder)
        {
            if (!Directory.Exists(folder))
                return;
            try
            {
                System.Diagnostics.Process.Start(folder);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
