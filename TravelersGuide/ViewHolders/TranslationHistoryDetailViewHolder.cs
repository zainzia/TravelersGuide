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
    public class TranslationHistoryDetailViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public TextView LanguageNameHistoryTextView;
        public TextView TranslatedTextTextView;


        public TranslationHistoryDetailViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            LanguageNameHistoryTextView = itemView.FindViewById<TextView>(Resource.Id.LanguageNameHistoryDetailView);
            TranslatedTextTextView = itemView.FindViewById<TextView>(Resource.Id.TranslatedTextDetailView);

            itemView.Click += (sender, e) => listener(base.Position);
        }
    }
}