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
using TravelersGuide.Core.Model;

namespace TravelersGuide.Adapters
{
    public class TranslationHistoryAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        List<CustomTranslationHistory> translationHistory;
        List<WorldLanguage> languageTable;

        public event EventHandler<int> ItemClick;

        public TranslationHistoryAdapter(List<CustomTranslationHistory> history, List<WorldLanguage> languages)
        {
            translationHistory = history;
            languageTable = languages;
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TranslationHistoryListCardView, parent, false);
            TranslationHistoryListViewHolder viewHolder = new TranslationHistoryListViewHolder(itemView, OnClick);

            return viewHolder;
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            TranslationHistoryListViewHolder viewHolder = holder as TranslationHistoryListViewHolder;
            viewHolder.SourceLanguageTextView.Text = languageTable.FirstOrDefault(x => x.WorldLanguageID == translationHistory[position].Source).Name;
            viewHolder.TargetLanguageTextView.Text = languageTable.FirstOrDefault(x => x.WorldLanguageID == translationHistory[position].Target).Name;
            viewHolder.TextToTranslateTextView.Text = translationHistory[position].TextToTranslate;
            viewHolder.TranslatedTextTextView.Text = translationHistory[position].TranslatedText;
        }

        public override int ItemCount
        {
            get { return translationHistory.Count; }
        }

        void OnClick(int position)
        {
            int TextToTransID = translationHistory[position].TextToTransID;
            if (ItemClick != null)
                ItemClick(this, TextToTransID);
        }

    }
}