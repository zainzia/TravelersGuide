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

using TravelersGuide.Core.Model;
using Android.Graphics;

namespace TravelersGuide.HelperClasses
{
    public static class GlobalVariables
    {

        public static DatabaseWrapper db { get; set; }
        public static Bitmap PictureTaken { get; set; }


        static GlobalVariables()
        {
            string dbPath = FileAccessHelper.GetLocalFilePath("TravelersGuide.db");
            db = new DatabaseWrapper(dbPath);
        }
    }
}