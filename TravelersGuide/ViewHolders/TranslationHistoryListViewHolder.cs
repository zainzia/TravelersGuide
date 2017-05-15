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
    public class TranslationHistoryListViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public TextView SourceLanguageTextView { get; set; }
        public TextView TargetLanguageTextView { get; set; }
        public TextView TextToTranslateTextView { get; set; }
        public TextView TranslatedTextTextView { get; set; }

        public TranslationHistoryListViewHolder(View itemView, Action<int> listener) : base(itemView)
        {

            SourceLanguageTextView = itemView.FindViewById<TextView>(Resource.Id.SourceLanguageHistoryListView);
            TargetLanguageTextView = itemView.FindViewById<TextView>(Resource.Id.TargetLanguageHistoryListView);
            TextToTranslateTextView = itemView.FindViewById<TextView>(Resource.Id.TextToTranslateListView);
            TranslatedTextTextView = itemView.FindViewById<TextView>(Resource.Id.TranslatedTextListView);

            itemView.Click += (sender, e) => listener (base.Position);
        }
        
    }
}