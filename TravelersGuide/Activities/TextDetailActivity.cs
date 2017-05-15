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

using TravelersGuide.Fragments;
using TravelersGuide.Core.Model;

namespace TravelersGuide.Activities
{
    [Activity(Label = "TextDetailActivity")]
    public class TextDetailActivity : Activity
    {

        public static List<TranslationsFromID> TextDetail;

        public List<TranslationsFromID> getTextDetail()
        {
            return TextDetail;
        }

        public void setTextDetail(List<TranslationsFromID> detail)
        {
            TextDetail = detail;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TextDetailActivity);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();
            var fragment = new TextDetailFragment();
            var fragmentContainer = Resource.Id.TextDetailFragmentContainer;

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
            MenuInflater.Inflate(Resource.Menu.TextDetailMenu, menu);

            var shareMenuItem = menu.FindItem(Resource.Id.TextDetail_shareMenuItem);
            var shareActionProvider = (ShareActionProvider)shareMenuItem.ActionProvider;
            shareActionProvider.SetShareIntent(CreateShareTextIntent());

            return true;
        }

        private Intent CreateShareTextIntent()
        {
            //Needs Implementation BUD
            return Intent;
        }
    }
}