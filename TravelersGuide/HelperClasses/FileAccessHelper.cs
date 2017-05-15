using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.IO;
using TravelersGuide.HelperClasses;
using TravelersGuide.Core.Model;

namespace TravelersGuide.HelperClasses
{
    public class FileAccessHelper
    {

        private static void CopyDatabaseIfNotExists(string dbPath, string fileName)
        {
            try
            {
                if (!File.Exists(dbPath))
                {
                    using (var br = new BinaryReader(Application.Context.Assets.Open(fileName)))
                    {
                        using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                        {
                            byte[] buffer = new byte[2048];
                            int length = 0;
                            while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bw.Write(buffer, 0, length);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }



        public static string GetLocalFilePath(string fileName)
        {
            try
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string dbPath = Path.Combine(path, fileName);

                CopyDatabaseIfNotExists(dbPath, fileName);

                return dbPath;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }

            return null;
        }

        public static Java.IO.File GetImageDirectory()
        {
            try
            {
                Java.IO.File ImageDirectory = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "TravelersGuideImages");

                if (!ImageDirectory.Exists())
                {
                    ImageDirectory.Mkdirs();
                }

                return ImageDirectory;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }

            return null;
        }

        public static Java.IO.File GetImageThumbnailDirectory()
        {
            try
            {
                Java.IO.File ImageDirectory = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "TravelersGuideImages/Thumbnails");

                if (!ImageDirectory.Exists())
                {
                    ImageDirectory.Mkdirs();
                }

                return ImageDirectory;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }

            return null;
        }

    }
}