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

//using Xamarin.Forms;
using Xamarin.Android;

using TravelersGuide.Fragments;
using TravelersGuide.Adapters;
using TravelersGuide.HelperClasses;
using TravelersGuide.Core.Model;

namespace TravelersGuide.Activities
{
    [Activity(Label = "TranslationHistoryMainActivity")]
    public class TranslationHistoryMainActivity : Activity
    {
        public List<CustomTranslationHistory> translationHistoryDetails;


        private void AddTab(string tabText, int IconResourceId, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(IconResourceId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.TranslationHistoryFragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);

                e.FragmentTransaction.Add(Resource.Id.TranslationHistoryFragmentContainer, view);
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

            SetContentView(Resource.Layout.TranslationHistoryMain);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab(GetString(Resource.String.TranslationHistoryMainTab), Resource.Drawable.test, new TranslationHistoryList());
            AddTab(GetString(Resource.String.TranslationHistoryDetailTab), Resource.Drawable.test, new TranslationHistoryDetail());
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