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

namespace TravelersGuide.ViewHolders
{
    public class ImageDetailViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public ImageDetailViewHolder(View itemView, Action<int> listener) : base(itemView)
        {

            itemView.Click += (sender, e) => listener(base.Position);
        }
    }


    public class ImageDetailImageViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public ImageView imageView { get; set; }

        public ImageDetailImageViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            imageView = itemView.FindViewById<ImageView>(Resource.Id.ImageDetail_ImageView);
            itemView.Click += (sender, e) => listener(base.Position);
        }
    }


    public class ImageDetailDetectedViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public TextView headingText { get; set; }
        public TextView detectedText { get; set; }
        public ProgressBar progressSpinner { get; set; }

        public ImageDetailDetectedViewHolder(View itemView, Action<int> listener) : base(itemView)
        {

            headingText = itemView.FindViewById<TextView>(Resource.Id.ImageDetail_DetectedHeadingView);
            detectedText = itemView.FindViewById<TextView>(Resource.Id.ImageDetail_DetectedView);
            progressSpinner = ItemView.FindViewById<ProgressBar>(Resource.Id.ImageDetail_DetectedSpinner);

            itemView.Click += (sender, e) => listener(base.Position);
        }
    }



    public class ImageDetailCaptionViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public TextView captionHeadingText { get; set; }
        public EditText captionEditView { get; set; }
        public Button saveBtn { get; set; }

        public ImageDetailCaptionViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            captionHeadingText = itemView.FindViewById<TextView>(Resource.Id.ImageDetail_CaptionHeadingView);
            captionEditView = itemView.FindViewById<EditText>(Resource.Id.ImageDetail_CaptionEditView);
            saveBtn = itemView.FindViewById<Button>(Resource.Id.ImageDetail_CaptionSaveBtn);

            itemView.Click += (sender, e) => listener(base.Position);
        }
    }


}
