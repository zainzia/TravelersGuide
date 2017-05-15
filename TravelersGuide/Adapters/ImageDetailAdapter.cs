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

using TravelersGuide.Core.Model;
using TravelersGuide.ViewHolders;
using TravelersGuide.HelperClasses;

namespace TravelersGuide.Adapters
{
    public class ImageDetailAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<CustomImageDetail> imageDetail;

        public static int IMAGE = 0;
        public static int DETECTED = 1;
        public static int CAPTION = 2;

        private int[] mDataSetTypes;

        public ImageDetailAdapter(List<CustomImageDetail> detail, int[] dataSetTypes)
        {
            imageDetail = detail;
            mDataSetTypes = dataSetTypes;
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView;

            if (viewType == IMAGE)
            {
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ImageDetailImageCardView, parent, false);
                ImageDetailImageViewHolder viewHolder = new ImageDetailImageViewHolder(itemView, OnClick);
                return viewHolder;
            }
            else if (viewType == DETECTED)
            {
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ImageDetailDetectedCardView, parent, false);
                ImageDetailDetectedViewHolder viewHolder = new ImageDetailDetectedViewHolder(itemView, OnClick);
                return viewHolder;
            }
            else
            {
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ImageDetailCaptionCardView, parent, false);
                ImageDetailCaptionViewHolder viewHolder = new ImageDetailCaptionViewHolder(itemView, OnClick);
                return viewHolder;
            }
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            if (holder.ItemViewType == IMAGE)
            {
                var imagePath = imageDetail[0].ImagePath + "/" + imageDetail[0].ImageName;
                var imageBitmap = ImageHelper.GetImageBitmapFromFilePath(imagePath, imageDetail[0].ImageWidth, imageDetail[0].ImageHeight);

                ImageDetailImageViewHolder viewHolder = holder as ImageDetailImageViewHolder;
                viewHolder.imageView.SetImageBitmap(imageBitmap);
            }
            else if (holder.ItemViewType == DETECTED)
            {
                ImageDetailDetectedViewHolder viewHolder = holder as ImageDetailDetectedViewHolder;

                viewHolder.headingText.Text = "Detected Text";

                if (imageDetail[0].TextDetected == 1)
                {
                    viewHolder.detectedText.Text = imageDetail[0].Text1;
                }
                else
                {
                    viewHolder.headingText.Text = "";
                    viewHolder.detectedText.Text = "Tap to Detect Text";
                }
            }
            else
            {
                ImageDetailCaptionViewHolder viewHolder = holder as ImageDetailCaptionViewHolder;
                if (!String.IsNullOrEmpty(imageDetail[0].Caption))
                {
                    viewHolder.captionEditView.Text = imageDetail[0].Caption;
                }

                viewHolder.saveBtn.Click += SaveBtn_Click;
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var Caption = btn.RootView.FindViewById<EditText>(Resource.Id.ImageDetail_CaptionEditView).Text;
            GlobalVariables.db.UpdateImageCaption(imageDetail[0].CapturedImagesID, Caption);
        }

        public override int GetItemViewType(int position)
        {
            return mDataSetTypes[position];
        }

        public override int ItemCount
        {
            get { return 3; }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}