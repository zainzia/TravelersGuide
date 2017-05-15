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

using TravelersGuide.Fragments;
using TravelersGuide.Core.Model;
using TravelersGuide.HelperClasses;

namespace TravelersGuide.Activities
{
    [Activity(Label = "TranslatorActivity")]
    public class TranslatorActivity : Activity
    {

        private static string dictionaryWord;
        private static string translatedString;
        private static string textToTranslate;

        public List<WorldLanguage> getLanguageTable()
        {
            return GlobalVariables.db.GetAllLanguages();
        }

        public string getSourceLanguage()
        {
            return GlobalVariables.db.GetSourceLanguagePrefix();
        }

        public void setSourceLanguage(string text)
        {
            GlobalVariables.db.setSourceLanguage(text);
        }


        public string getTargetLanguage()
        {
            return GlobalVariables.db.GetTargetLanguagePrefix();
        }

        public void setTargetLanguage(string text)
        {
            GlobalVariables.db.setTargetLanguage(text);
        }


        public string getTranslatedString()
        {
            return translatedString;
        }

        public void setTranslatedString(string text)
        {
            translatedString = text;
        }

        public string getTextToTranslate()
        {
            return textToTranslate;
        }

        public void setTextToTranslate(string text)
        {
            textToTranslate = text;
        }

        public string getDictionaryWord()
        {
            return dictionaryWord;
        }

        public void setDictionaryWord(string word)
        {
            dictionaryWord = word;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TranslatorActivity);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            FragmentTransaction fragmentTx = FragmentManager.BeginTransaction();
            var fragment = new TranslatorFragment();
            var fragmentContainer = Resource.Id.TranslatorFragmentContainer;

            fragmentTx.Add(fragmentContainer, fragment);
            fragmentTx.Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    // app icon in action bar clicked; goto parent activity.
                    Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}