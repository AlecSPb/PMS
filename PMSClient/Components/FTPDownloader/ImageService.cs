using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;
using System.Net;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace PMSClient.Components.FTPDownloader
{
    /// <summary>
    /// 主要用于从FTP服务器获取文件目的
    /// 直接访问速度太慢，考虑在本地图片文件夹里做一个缓存文件夹来存放缓存
    /// 
    /// 
    /// </summary>
    public class ImageService
    {
        private string host;
        private NetworkCredential credential;

        private string serverFolderName;
        private string imageCacheFolderName;
        private string folderName;

        public ImageService()
        {
            //read config from property
            host = Properties.Settings.Default.FtpImageServerIp;
            credential = new NetworkCredential("photoadmin","cdpmiadmin");

            serverFolderName = "Y_CSCAN_PMS";
            imageCacheFolderName = Properties.Settings.Default.FtpImageCache;
            folderName = "targets";
        }

        #region Helpers
        /// <summary>
        /// 检查并创建文件夹
        /// </summary>
        /// <param name="dirName"></param>
        private string CheckOrCreateDirectory(string dirName)
        {
            try
            {
                string myPicture = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                string dir = Path.Combine(myPicture, imageCacheFolderName, dirName);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return dir;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /// <summary>
        /// 查找靶材图片
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public ImageFoundResult FindTargetImage(string productid)
        {
            folderName = "targets";
            return FindImageByProductID(productid, folderName);
        }
        /// <summary>
        /// 查找绑定图片
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public ImageFoundResult FindBondingImage(string productid)
        {
            folderName = "bondings";
            return FindImageByProductID(productid,folderName);
        }

        /// <summary>
        /// 通过产品id
        /// 依次从缓存和ftp服务器查找图像
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        private ImageFoundResult FindImageByProductID(string productid, string folderName)
        {
            //检查本地文件夹
            CheckOrCreateDirectory("");

            ImageFoundResult foundResult = new ImageFoundResult();

            string localPath= CheckOrCreateDirectory(folderName);
            //先从本地缓存找
            var file = Directory.GetFiles(localPath, $"{productid}.jpg", SearchOption.AllDirectories)
                .FirstOrDefault();
            if (file != null)
            {
                foundResult.IsFound = true;
                foundResult.ImagePath = file;
                foundResult.InfoMessage = "在本地缓存中找到";
            }
            else
            {
                //去服务器上找
                #region FTPDownload
                CheckOrCreateDirectory(folderName);

                string fileName_Server = $"/{serverFolderName}/{folderName}/{productid}.jpg";
                string fileName_Local = Path.Combine(CheckOrCreateDirectory(folderName), $"{productid}.jpg");

                var ftp = new FtpClient(host, credential);
                ftp.Encoding = Encoding.Default;

                try
                {
                    ftp.Connect();
                    bool is_exist_bondings = ftp.FileExists(fileName_Server);
                    if (is_exist_bondings)
                    {
                        ftp.DownloadFile(fileName_Local, fileName_Server);
                        foundResult.IsFound = true;
                        foundResult.ImagePath = fileName_Local;
                        foundResult.InfoMessage = "在服务器上已找到，并已下载到本地缓存";
                    }
                    else
                    {
                        foundResult.IsFound = false;
                        foundResult.InfoMessage = $"服务器文件夹{folderName}中{productid}.jpg不存在";
                    }
                }
                catch (Exception ex)
                {
                    PMSDialogService.ShowWarning(ex.Message);
                }
                finally
                {
                    ftp.Disconnect();
                }
                #endregion
            }

            return foundResult;
        }


    }
}
