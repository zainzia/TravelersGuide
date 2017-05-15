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
    public class TextDetailViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public TextView textHeadingView { get; set; }
        public TextView textView { get; set; }

        public TextDetailViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            textHeadingView = itemView.FindViewById<TextView>(Resource.Id.TextDetail_HeadingTextView);
            textView = itemView.FindViewById<TextView>(Resource.Id.TextDetail_TextView);
        }
    }

}