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
    public class FTPService
    {
        private string host;
        private NetworkCredential credential;

        private string cacheFolder;
        public FTPService()
        {
            host = "192.168.16.254";
            credential = new NetworkCredential("photoadmin", "cdpmiadmin");
        }
        public FTPService(string hostStr, string uid, string pwd) : this()
        {
            host = hostStr;
            credential = new NetworkCredential(uid, pwd);
        }

        public async void ShowFTPImage(string productid)
        {
            try
            {
                MemoryStream ms = await DownloadImage(productid);
                if (ms != null)
                {
                    //流位置必须会到开头才可以，否则会报错；
                    ms.Seek(0, SeekOrigin.Begin);
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = ms;
                    bmp.EndInit();
                    var dg = new FTPImageShow();
                    dg.MainImage.Source = bmp;
                    dg.ShowDialog();
                }
                else
                {
                    PMSDialogService.ShowWarning($"没有找到[{productid}.jpg]的图片");
                }
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }


        private async Task<MemoryStream> DownloadImage(string productid)
        {
            string fileName_bondings = $"/Y_CSCAN_PMS/Bondings/{productid}.jpg";
            string fileName_targets = $"/Y_CSCAN_PMS/Targets/{productid}.jpg";


            var ftp = new FtpClient(host, credential);
            ftp.Encoding = Encoding.Default;
            ftp.DownloadDataType = FtpDataType.Binary;

            bool is_downloaded = false;
            MemoryStream ms = new MemoryStream();
            try
            {
                ftp.Connect();
                //先查找bondings文件夹
                bool is_exist_bondings = await ftp.FileExistsAsync(fileName_bondings);
                if (is_exist_bondings)
                {
                    is_downloaded = await ftp.DownloadAsync(ms, fileName_bondings);
                }
                else
                {
                    //在考虑targets文件夹
                    bool is_exist_targets = await ftp.FileExistsAsync(fileName_targets);
                    if (is_exist_targets)
                    {
                        is_downloaded = await ftp.DownloadAsync(ms, fileName_targets);
                    }
                }

            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
                return null;
            }
            finally
            {
                ftp.Disconnect();
            }

            if (is_downloaded)
            {
                return ms;
            }
            else
            {
                return null;
            }
        }

        private void CheckCacheFolder()
        {
            string imageFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            cacheFolder = Path.Combine(imageFolder, "Y_CSCAN_PMS");
            if (!Directory.Exists(cacheFolder))
            {
                Directory.CreateDirectory(cacheFolder);
            }
        }


        /// <summary>
        /// 从本地缓存文件夹查找
        /// 先查找Bondings
        /// 再查找Targets
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public string GetImagePathFromCache(string productid)
        {
            CheckCacheFolder();

            string imagePath = Directory.GetFiles(cacheFolder, "*.jpg")
                 .Where(i => Path.GetFileNameWithoutExtension(i) == productid)
                 .FirstOrDefault();

            return imagePath;
        }

        /// <summary>
        /// 从服务器下载文件
        /// </summary>
        /// <param name="productid"></param>
        public async Task<string> GetImageFromServer(string productid)
        {
            CheckCacheFolder();
            string fileName_bondings = $"/Y_CSCAN_PMS/Bondings/{productid}.jpg";
            string fileName_targets = $"/Y_CSCAN_PMS/Targets/{productid}.jpg";

            string fileName_local = Path.Combine(cacheFolder, $"{productid}".jpg);

            var ftp = new FtpClient(host, credential);
            ftp.Encoding = Encoding.Default;
            bool is_downloaded = false;

            try
            {
                ftp.Connect();
                //先查找bondings文件夹
                bool is_exist_bondings = await ftp.FileExistsAsync(fileName_bondings);
                if (is_exist_bondings)
                {
                    await ftp.DownloadFileAsync(fileName_local, fileName_bondings);
                    is_downloaded = true;
                }
                else
                {
                    //在考虑targets文件夹
                    bool is_exist_targets = await ftp.FileExistsAsync(fileName_targets);
                    if (is_exist_targets)
                    {
                        await ftp.DownloadFileAsync(fileName_local, fileName_targets);
                        is_downloaded = true;
                    }
                }

            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
                return null;
            }
            finally
            {
                ftp.Disconnect();
            }

            if (is_downloaded)
            {
                return ms;
            }
            else
            {
                return null;
            }
        }




    }
}
