using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.BarCodeService
{
    public class Helper
    {
        private string folder;
        public Helper()
        {
            folder = Path.Combine(Environment.CurrentDirectory, "Temp");
        }
        public string GetTempFile(string filename)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return Path.Combine(folder, filename);
        }
        public string GetTempFileInDocumentFolder(string filename)
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PMSTemp");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return Path.Combine(folder, filename);
        }

        public void DelAll()
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
        }
    }
}
