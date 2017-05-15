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

using TravelersGuide.ViewHolders;
using TravelersGuide.HelperClasses;
using TravelersGuide.Core.Model;

namespace TravelersGuide.Adapters
{
    public class GalleryAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<CapturedImages> capturedImages;

        public GalleryAdapter(List<CapturedImages> Images)
        {
            capturedImages = Images;
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.GalleryFragmentCardView, parent, false);
            GalleryFragmentViewHolder viewHolder = new GalleryFragmentViewHolder(itemView, OnClick);
            return viewHolder;
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            var filePath = capturedImages[position].ThumbnailPath + "/" + capturedImages[position].ImageName;
            var imageBitmap = ImageHelper.GetImageBitmapFromFilePath(filePath, capturedImages[position].ImageWidth, capturedImages[position].ImageHeight);

            GalleryFragmentViewHolder viewHolder = holder as GalleryFragmentViewHolder;
            viewHolder.CapturedImageView.SetImageBitmap(imageBitmap);
            if (!String.IsNullOrEmpty(capturedImages[position].TextDetected))
            {
                viewHolder.DetectedTextView.Text = capturedImages[position].TextDetected;
            }
        }

        public override int ItemCount
        {
            get { return capturedImages.Count; }
        }

        void OnClick(int position)
        {
            int imageID = capturedImages.Count - position;
            if (ItemClick != null)
                ItemClick(this, imageID);
        }



    }
}