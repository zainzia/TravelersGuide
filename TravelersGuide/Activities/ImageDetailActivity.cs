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

using TravelersGuide;
using TravelersGuide.Core;
using TravelersGuide.HelperClasses;
using TravelersGuide.Adapters;
using TravelersGuide.Activities;
using TravelersGuide.Fragments;

using TravelersGuide.Core.Model;


namespace TravelersGuide.Activities
{
    [Activity(Label = "ImageDetailActivity")]
    public class ImageDetailActivity : Activity
    {
        private List<CustomImageDetail> imageDetail;

        public void setImageDetail(List<CustomImageDetail> Detail)
        {
            imageDetail = Detail;
        }

        public List<CustomImageDetail> getImageDetail()
        {
            return imageDetail;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ImageDetailActivity);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();
            var fragment = new ImageDetailFragment();
            var fragmentContainer = Resource.Id.ImageDetailFragmentContainer;

            fragmentTx.Add(fragmentContainer, fragment);
            fragmentTx.Commit();
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ImageDetailMenu, menu);
            
            var shareMenuItem = menu.FindItem(Resource.Id.ImageDetail_shareMenuItem);
            var shareActionProvider = (ShareActionProvider)shareMenuItem.ActionProvider;
            shareActionProvider.SetShareIntent(CreateSharePicIntent());

            return true;
        }

        private Intent CreateSharePicIntent()
        {
            var sendPicIntent = new Intent(Intent.ActionSend);
            sendPicIntent.SetType("image/*");

            var uri = Android.Net.Uri.Parse("file://" + imageDetail[0].ImagePath + "/" + imageDetail[0].ImageName);
            sendPicIntent.PutExtra(Intent.ExtraStream, uri);

            return sendPicIntent;
        }
    }
}