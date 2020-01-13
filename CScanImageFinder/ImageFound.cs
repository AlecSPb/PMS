using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonHelper;

namespace CScanImageFinder
{
    public class ImageFound
    {
        private BasicHelper helper = new BasicHelper();
        private string imageFolder;
        public ImageFound()
        {
            imageFolder = @"C:\Users\XS\Desktop\Bondings";
        }

        public string FindByID(string productid)
        {
            return Directory.GetFiles(imageFolder, $"{productid}.jpg").FirstOrDefault();
        }

        public void RunSearch(string filename)
        {
            string outputFolder = helper.CreateFolder(helper.GetDesktopPath(), "SearchResult");
            string[] lines = File.ReadAllLines(filename);
            foreach (var item in lines)
            {
                string imageFile = FindByID(item);
                if (imageFile != null)
                {
                    string targetFile = helper.GetFullFileName($"{item}.jpg", outputFolder);
                    File.Copy(imageFile, targetFile, true);

                }
            }

            GalleryService service = new GalleryService();
            service.CreateDocument(lines);

        }
    }
}
