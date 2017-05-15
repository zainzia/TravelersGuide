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

using TravelersGuide.Core.Model;
using TravelersGuide.HelperClasses;
using TravelersGuide.Activities;
using TravelersGuide.Adapters;

namespace TravelersGuide.Fragments
{
    public class TextDetailFragment : Fragment
    {
        public List<TranslationsFromID> textDetail;
        public Android.Support.V7.Widget.GridLayoutManager mLayoutManager;
        public View view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);

            view = inflater.Inflate(Resource.Layout.TextDetailView, container, false);

            textDetail = GlobalVariables.db.GetAllTranslations(Activity.Intent.GetIntExtra("TextToTranslateID", 1));
            ((TextDetailActivity)Activity).setTextDetail(textDetail);

            var adapter = new TextDetailAdapter(textDetail);
            adapter.ItemClick += OnItemClick;

            var mRecyclerView = view.FindViewById<Android.Support.V7.Widget.RecyclerView>(Resource.Id.TextDetailRecyclerView);
            mRecyclerView.SetAdapter(adapter);

            mLayoutManager = new Android.Support.V7.Widget.GridLayoutManager(Context, 1, Android.Support.V7.Widget.GridLayoutManager.Vertical, false);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            var fab = view.FindViewById<CustomFAB>(Resource.Id.TextDetailViewFAB);

            fab.AttachToRecyclerView(mRecyclerView);
            var rootView = fab.RootView;

            fab.Click += (sender, args) =>
            {
                var intent2 = new Intent(Context, typeof(TranslatorActivity));
                intent2.PutExtra("Source", textDetail[0].SourceName.Prefix);
                intent2.PutExtra("TextToTranslate", textDetail[0].Text1);
                intent2.PutExtra("TextToTranslateID", Activity.Intent.GetIntExtra("TextToTranslateID", 0));

                StartActivity(intent2);
            };

            return view;
        }

        void OnItemClick(object sender, int position)
        {

        }
    }
}