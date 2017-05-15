using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TravelersGuide.Fragments;
using TravelersGuide.HelperClasses;

using System.Collections.Generic;

using TravelersGuide.Core.Model;

namespace TravelersGuide.Activities
{
    [Activity(Label = "TravelersGuide", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private static string dictionaryWord;
        private static string translatedString;
        private static string textToTranslate;

        private static Java.IO.File ImageFile;

        private static int ImageHeight;
        private static int ImageWidth;
        private static string ImageDirectory;
        private static string ImageName;

        public void setImageDirectory(string dirPath)
        {
            ImageDirectory = dirPath;
        }

        public void setImageName(string name)
        {
            ImageName = name;
        }

        public void setImageHeight(int result)
        {
            ImageHeight = result;
        }

        public int getImageHeight()
        {
            return ImageHeight;
        }

        public void setImageWidth(int width)
        {
            ImageWidth = width;
        }

        public int getImageWidth()
        {
            return ImageWidth;
        }

        public Java.IO.File getImageFile()
        {
            return ImageFile;
        }

        public void setImageFile(Java.IO.File file)
        {
            ImageFile = file;
        }

        


        private void AddTab(string tabText, int IconResourceId, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(IconResourceId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);

                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab(GetString(Resource.String.placesTabName), Resource.Drawable.test, new PlacesFragment());
            AddTab(GetString(Resource.String.galleryTabName), Resource.Drawable.test, new GalleryFragment());
            AddTab(GetString(Resource.String.TranslationHistoryMainTab), Resource.Drawable.test, new TranslationHistoryList());

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (resultCode != Result.Canceled)
            {
                base.OnActivityResult(requestCode, resultCode, data);
                var ThumbnailDirectory = FileAccessHelper.GetImageThumbnailDirectory().ToString();
                var imageID = GlobalVariables.db.InsertCapturedImage(ImageDirectory, ImageName.ToString(), ImageHeight, ImageWidth, ThumbnailDirectory);

                ImageHelper.CreateImageThumbnail(imageID);

                var intent2 = new Intent(this, typeof(ImageDetailActivity));
                intent2.PutExtra("ImageID", imageID);

                StartActivity(intent2);
            }
        }
    }
}

