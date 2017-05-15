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
    public class TranslationHistoryDetailAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {

        List<CustomTranslationHistory> translationHistory;
        List<WorldLanguage> languageTable;

        public event EventHandler<int> ItemClick;

        public TranslationHistoryDetailAdapter(List<CustomTranslationHistory> history, List<WorldLanguage> languages)
        {
            translationHistory = history;
            languageTable = languages;
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TranslationHistoryDetailCardView, parent, false);
            var viewHolder = new TranslationHistoryDetailViewHolder(itemView, OnClick);

            return viewHolder;
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as TranslationHistoryDetailViewHolder;
            if (position == 0)
            {
                viewHolder.LanguageNameHistoryTextView.Text = languageTable.FirstOrDefault(x => x.WorldLanguageID == translationHistory[position].Source).Name;
                viewHolder.TranslatedTextTextView.Text = translationHistory[position].TextToTranslate;
            }
            else
            {
                viewHolder.LanguageNameHistoryTextView.Text = languageTable.FirstOrDefault(x => x.WorldLanguageID == translationHistory[position-1].Target).Name;
                viewHolder.TranslatedTextTextView.Text = translationHistory[position -1].TranslatedText;
            }
        }

        public override int ItemCount
        {
            get { return translationHistory.Count+1; }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}