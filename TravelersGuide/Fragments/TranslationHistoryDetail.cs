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

using TravelersGuide.Adapters;
using TravelersGuide.HelperClasses;
using TravelersGuide.Activities;

namespace TravelersGuide.Fragments
{
    public class TranslationHistoryDetail : BaseFragment
    {
        private View view;
        public TranslationHistoryDetailAdapter translationHistoryDetailAdapter;
        public Android.Support.V7.Widget.LinearLayoutManager mLayoutManager;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        void OnItemClick(object sender, int position)
        {
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            view = inflater.Inflate(Resource.Layout.TranslationHistoryDetail, container, false);

            translationHistoryDetailAdapter = new TranslationHistoryDetailAdapter(((TranslationHistoryMainActivity)Activity).translationHistoryDetails, GlobalVariables.db.GetAllLanguages());
            translationHistoryDetailAdapter.ItemClick += OnItemClick;

            Android.Support.V7.Widget.RecyclerView mRecyclerView = view.FindViewById<Android.Support.V7.Widget.RecyclerView>(Resource.Id.TranslationDetailRecyclerView);
            mRecyclerView.SetAdapter(translationHistoryDetailAdapter);

            mLayoutManager = new Android.Support.V7.Widget.LinearLayoutManager(Context, Android.Support.V7.Widget.LinearLayoutManager.Vertical, false);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            return view;
        }
    }
}