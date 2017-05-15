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

namespace TravelersGuide.Adapters
{
    public class TextDetailAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        private List<TranslationsFromID> TextDetail;
        public event EventHandler<int> ItemClick;

        public TextDetailAdapter(List<TranslationsFromID> detail)
        {
            TextDetail = detail;
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TextDetailCardView, parent, false);
            TextDetailViewHolder viewHolder = new TextDetailViewHolder(itemView, OnClick);
            return viewHolder;
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            TextDetailViewHolder viewHolder = holder as TextDetailViewHolder;

            if (position == 0 && TextDetail.Count > 0)
            {
                viewHolder.textHeadingView.Text = TextDetail[0].SourceName.Name;
                viewHolder.textView.Text = TextDetail[0].Text1;
            }
            else if (TextDetail.Count > 0)
            {
                viewHolder.textHeadingView.Text = TextDetail[position-1].Name;
                viewHolder.textView.Text = TextDetail[position-1].Text2;
            }
        }

        public override int ItemCount
        {
            get
            {
                if (TextDetail != null && TextDetail[0].TextTranslationID != 0)
                {
                    return TextDetail.Count + 1;
                }
                else
                {
                    return TextDetail.Count;
                }
            }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }



    }
}