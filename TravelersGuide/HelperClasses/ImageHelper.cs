using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using System.Drawing;

using TravelersGuide.Core.Model;

namespace TravelersGuide.HelperClasses
{
    public class ImageHelper
    {

        public static Bitmap GetImageBitmapFromFilePath(string fileName, int width, int height)
        {
            try
            {
                BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
                var file = BitmapFactory.DecodeFile(fileName, options);

                //int inSampleSize = -1;
                //if (options.OutWidth > width || options.OutHeight > height)
                //{
                //    inSampleSize = options.OutWidth > options.OutHeight ? options.OutHeight / height : options.OutWidth / width;
                //}

                //options.InSampleSize = inSampleSize;
                options.InSampleSize = 6;
                options.InJustDecodeBounds = false;

                Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);
                return resizedBitmap;
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }

            return null;
        }


        public static void CreateImageThumbnail(int ImageID)
        {
            var imageDetail = GlobalVariables.db.GetImageDetail(ImageID);

            var filePath = imageDetail[0].ImagePath + "/" + imageDetail[0].ImageName;
            var thumbnailDir = FileAccessHelper.GetImageThumbnailDirectory() + "/" + imageDetail[0].ImageName;
            var imageBitmap = GetImageBitmapFromFilePath(filePath, imageDetail[0].ImageWidth, imageDetail[0].ImageHeight);

            
            var memoryStream = new MemoryStream();
            imageBitmap.Compress(Bitmap.CompressFormat.Jpeg, 30, memoryStream);
            File.WriteAllBytes(thumbnailDir, memoryStream.ToArray());

        }

    }
}