using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication.Helpers
{
    public static class FileUtils
    {
        public const string ImagesFolder = "Images";
        public static Image GetImage(string fileName, int sizeX=70, int sizeY=70)
        {
            string location = AppDomain.CurrentDomain.BaseDirectory;
            using (Bitmap sourceBmp = new Bitmap($"{location}/../../../{ImagesFolder}/{fileName}"))
            {
                Image clonedImg = new Bitmap(sourceBmp.Width, sourceBmp.Height, PixelFormat.Format32bppArgb);
                using (var copy = Graphics.FromImage(clonedImg))
                {
                    copy.DrawImage(sourceBmp, 0, 0, sizeX, sizeY);
                }
                return clonedImg;
            }
        }
    }
}
