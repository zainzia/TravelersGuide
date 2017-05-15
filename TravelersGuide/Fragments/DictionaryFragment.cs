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

using TravelersGuide.Activities;
using TravelersGuide.HelperClasses;

namespace TravelersGuide.Fragments
{
    public class DictionaryFragment : BaseFragment
    {
        private View view;
        private string wordToSearch;
        private Button takePictureButton;
        private Java.IO.File ImageFile;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            
            //string word = ((MainActivity)this.Activity).getDictionaryWord();
            //if (!String.IsNullOrEmpty(word))
            //{
            //    wordToSearch = word;
            //    ((MainActivity)this.Activity).setDictionaryWord("");

            //    //Start Search
            //}
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            view = inflater.Inflate(Resource.Layout.DictionaryFragment, container, false);

            takePictureButton = view.FindViewById<Button>(Resource.Id.TakePictureButton);
            takePictureButton.Click += TakePictureButton_Click;

            ((MainActivity)Activity).setImageHeight(Resources.DisplayMetrics.HeightPixels);
            ((MainActivity)Activity).setImageWidth(Resources.DisplayMetrics.WidthPixels);

            return view;
        }

        private void TakePictureButton_Click(object sender, EventArgs e)
        {
            
        }

    }
}