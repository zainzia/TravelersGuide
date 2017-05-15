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
using Android.Provider;

using TravelersGuide.Adapters;
using TravelersGuide.HelperClasses;
using TravelersGuide.Activities;

namespace TravelersGuide.Fragments
{
    public class GalleryFragment : BaseFragment
    {

        public View view;
        public Android.Support.V7.Widget.GridLayoutManager mLayoutManager;
        public GalleryAdapter adapter;
        private Android.Support.V7.Widget.RecyclerView mRecyclerView;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        void OnItemClick(object sender, int position)
        {
            var intent = new Intent(Activity, typeof(ImageDetailActivity));
            intent.PutExtra("ImageID", position);
            StartActivity(intent);
        }

        public override void OnResume()
        {
            base.OnResume();

            adapter.capturedImages = GlobalVariables.db.GetAllCapturedImages();
            adapter.NotifyDataSetChanged();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            view = inflater.Inflate(Resource.Layout.GalleryFragment, container, false);

            var capturedImages = GlobalVariables.db.GetAllCapturedImages();
            adapter = new GalleryAdapter(capturedImages);
            adapter.ItemClick += OnItemClick;

            mRecyclerView = view.FindViewById<Android.Support.V7.Widget.RecyclerView>(Resource.Id.GalleryRecyclerView);
            var emptyTextView = view.FindViewById<TextView>(Resource.Id.EmptyGalleryTextView);
            var fab = view.FindViewById<CustomFAB>(Resource.Id.GalleryTabFAB);

            if (capturedImages.Count < 1)
            {
                mRecyclerView.Visibility = ViewStates.Gone;
                emptyTextView.Visibility = ViewStates.Visible;
            }
            else
            {
                emptyTextView.Visibility = ViewStates.Gone;
                mRecyclerView.Visibility = ViewStates.Visible;

            }
            mRecyclerView.SetAdapter(adapter);
            
            mLayoutManager = new Android.Support.V7.Widget.GridLayoutManager(Context, 2, Android.Support.V7.Widget.GridLayoutManager.Vertical, false);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            
            fab.AttachToRecyclerView(mRecyclerView);
            var rootView = fab.RootView;

            fab.Click += (sender, args) =>
            {
                var ImageDirectory = FileAccessHelper.GetImageDirectory();
                var imageName = Guid.NewGuid() + ".jpg";
                Java.IO.File ImageFile = new Java.IO.File(ImageDirectory, imageName.ToString());

                ((MainActivity)Activity).setImageFile(ImageFile);
                ((MainActivity)Activity).setImageName(imageName.ToString());
                ((MainActivity)Activity).setImageDirectory(ImageDirectory.AbsolutePath);

                var intent = new Intent(MediaStore.ActionImageCapture);
                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(ImageFile));
                ((MainActivity)Activity).StartActivityForResult(intent, 0);
            };
            
            return view;
        }
    }
}