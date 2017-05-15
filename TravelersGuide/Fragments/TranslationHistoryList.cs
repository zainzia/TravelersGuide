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

using TravelersGuide.Core;
using TravelersGuide.HelperClasses;
using TravelersGuide.Adapters;
using TravelersGuide.Activities;

namespace TravelersGuide.Fragments
{
    public class TranslationHistoryList : BaseFragment
    {
        private View view;
        public TranslationHistoryAdapter translationHistoryAdapter;
        public Android.Support.V7.Widget.GridLayoutManager mLayoutManager;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        void OnItemClick(object sender, int TextToTransID)
        {
            var intent = new Intent(Activity, typeof(TextDetailActivity));
            intent.PutExtra("TextToTranslateID", TextToTransID);
            StartActivity(intent);

        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            view = inflater.Inflate(Resource.Layout.TranslationHistoryList, container, false);

            var translationHistory = GlobalVariables.db.GetAllTranslationHistory();
            translationHistoryAdapter = new TranslationHistoryAdapter(translationHistory, GlobalVariables.db.GetAllLanguages());
            translationHistoryAdapter.ItemClick += OnItemClick;
            Android.Support.V7.Widget.RecyclerView mRecyclerView = view.FindViewById<Android.Support.V7.Widget.RecyclerView>(Resource.Id.TranslationListRecyclerView);

            var emptyTextView = view.FindViewById<TextView>(Resource.Id.EmptyTextHistoryTextView);
            var fab = view.FindViewById<CustomFAB>(Resource.Id.TextTabFAB);

            if (translationHistory.Count < 1)
            {
                mRecyclerView.Visibility = ViewStates.Gone;
                emptyTextView.Visibility = ViewStates.Visible;
            }
            else
            {
                emptyTextView.Visibility = ViewStates.Gone;
                mRecyclerView.Visibility = ViewStates.Visible;

            }

            mRecyclerView.SetAdapter(translationHistoryAdapter);
            
            mLayoutManager = new Android.Support.V7.Widget.GridLayoutManager(Context, 2, Android.Support.V7.Widget.GridLayoutManager.Vertical, false);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            fab.AttachToRecyclerView(mRecyclerView);
            var rootView = fab.RootView;

            fab.Click += (sender, args) =>
            {
                var intent2 = new Intent(Context, typeof(TranslatorActivity));

                StartActivity(intent2);
            };

            return view;

        }
    }
}