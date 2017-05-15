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
    public class GalleryFragmentViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {

        public ImageView CapturedImageView { get; set; }
        public TextView DetectedTextView { get; set; }

        public GalleryFragmentViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            CapturedImageView = itemView.FindViewById<ImageView>(Resource.Id.Gallery_CapturedImageView);
            DetectedTextView = itemView.FindViewById<TextView>(Resource.Id.Gallery_DetectedTextView);

            itemView.Click += (sender, e) => listener(base.Position);
        }
    }
}