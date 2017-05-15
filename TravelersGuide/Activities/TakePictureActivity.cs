using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using TravelersGuide.Core;
using TravelersGuide.HelperClasses;
using TravelersGuide.Adapters;
using TravelersGuide.Activities;
using TravelersGuide.Fragments;


namespace TravelersGuide.Activities
{
    [Activity(Label = "TakePictureActivity")]
    public class TakePictureActivity : Activity
    {
        private Bitmap ImageBitmap;

        public Bitmap getImageBitmap()
        {
            return ImageBitmap;
        }

        private void AddTab(string tabText, int IconResourceId, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(IconResourceId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.TakePictureFragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);

                e.FragmentTransaction.Add(Resource.Id.TakePictureFragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            ActionBar.AddTab(tab);
        }



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.TakePictureMain);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            ImageBitmap = GlobalVariables.PictureTaken;

            AddTab(GetString(Resource.String.TakePictureMainTab), Resource.Drawable.test, new TakePictureImageViewFragment());
            AddTab(GetString(Resource.String.TakePictureDetailsTab), Resource.Drawable.test, new TakePictureDetailFragment());
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    // app icon in action bar clicked; goto parent activity.
                    Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}