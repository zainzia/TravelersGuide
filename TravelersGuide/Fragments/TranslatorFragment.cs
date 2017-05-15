using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Text.Style;
using Android.Text;
using Android.Views.InputMethods;

using System.Web;
using TravelersGuide.Activities;
using TravelersGuide.HelperClasses;

using TravelersGuide.Core.Model;

namespace TravelersGuide.Fragments
{

    public class TranslatorFragment : BaseFragment
    {
        private View view;
        private List<WorldLanguage> languageTable;
        private int TextTranslationID;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        private void setupControls(View view)
        {
            EditText editText = view.FindViewById<EditText>(Resource.Id.textToTranslateView);
            editText.SetHeight(view.Height / 3);

            TextView textView = view.FindViewById<TextView>(Resource.Id.translatedTextView);

            string text1 = ((TranslatorActivity)Activity).getTextToTranslate();
            if(!String.IsNullOrEmpty (text1))
                editText.Text = text1;

            string text2 = ((TranslatorActivity)Activity).getTranslatedString();
            if (!String.IsNullOrEmpty(text2))
                textView.Text = text2;
        }

        private void saveText (View view)
        {
            EditText editText = view.FindViewById<EditText>(Resource.Id.textToTranslateView);
            editText.SetHeight(view.Height / 3);

            TextView textView = view.FindViewById<TextView>(Resource.Id.translatedTextView);

            if (!String.IsNullOrEmpty(editText.Text)) 
                ((TranslatorActivity)Activity).setTextToTranslate(editText.Text);

            if(!String.IsNullOrEmpty(textView.Text))
                ((TranslatorActivity)Activity).setTranslatedString(textView.Text);
        }


        private void displayResults(string results)
        {
            try
            {
                if (!String.IsNullOrEmpty(results))
                {
                    ((TranslatorActivity)Activity).setTranslatedString(results);

                    TextView translatedTextView = View.FindViewById<TextView>(Resource.Id.translatedTextView);
                    translatedTextView.Click += (s, e) =>
                    {
                        if (TextTranslationID != null)
                        {
                            var intent = new Intent(Activity, typeof(TextDetailActivity));
                            var TextToTransID = GlobalVariables.db.GetTextToTransIDFromTranslation(TextTranslationID);
                            intent.PutExtra("TextToTranslateID", TextToTransID);
                            StartActivity(intent);
                        }
                    };

                    translatedTextView.Text = results;

                    //var spannableResult = new SpannableString(results);
                    //MyClickableSpan clickableSpan;

                    //int start = 0;
                    //foreach (string word in results.Split(' '))
                    //{
                    //    clickableSpan = new MyClickableSpan(word);
                    //    clickableSpan.Click += (Word) =>
                    //    {
                    //        Logger.Log("Tapped on " + word + ". Going to Dictionary now");

                    //        var intent = new Intent(Activity, typeof(TextDetailActivity));
                    //        intent.PutExtra("TextToTranslateID", TextToTransID);
                    //        StartActivity(intent);
                    //    };
                    //    spannableResult.SetSpan(clickableSpan, start, start + word.Length, SpanTypes.ExclusiveExclusive);
                    //    start += word.Length;
                    //    for (int i = 0; start < results.Length; i++)
                    //    {
                    //        if (results[start] == ' ')
                    //            start++;
                    //        else
                    //            break;
                    //    }
                    //}

                    //translatedTextView.TextFormatted = spannableResult;
                    //translatedTextView.MovementMethod = new Android.Text.Method.LinkMovementMethod();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }


        private void UpdateLanguage(string languageName)
        {
            Button sourceButton = view.FindViewById<Button>(Resource.Id.sourceLanguageBtn);
            sourceButton.Text = languageName;
            GlobalVariables.db.setSourceLanguage(languageTable.FirstOrDefault(x => x.Name.Equals(languageName)).Prefix);

            Toast.MakeText(Context, "Language Detected: "+languageName, ToastLength.Long).Show();
        }

        private string OnResultsObtained(string results)
        {   
            Activity.RunOnUiThread(() => displayResults(results));

            int sourceID = languageTable.FirstOrDefault(x => x.Prefix == ((TranslatorActivity)Activity).getSourceLanguage()).WorldLanguageID;
            int targetID = languageTable.FirstOrDefault(x => x.Prefix == ((TranslatorActivity)Activity).getTargetLanguage()).WorldLanguageID;
            var textToTranslateID = Activity.Intent.GetIntExtra("TextToTranslateID", 0);

            if (!string.IsNullOrEmpty(results) && !string.IsNullOrEmpty(((TranslatorActivity)Activity).getTextToTranslate()))
                TextTranslationID = GlobalVariables.db.InsertTranslationHistory(sourceID, targetID, ((TranslatorActivity)Activity).getTextToTranslate(), results, "", textToTranslateID);

            return "";
        }

        private string OnLanguageDetected(string results)
        {
            Activity.RunOnUiThread(() => UpdateLanguage(results));
            return "";
        }

        private void removeHandlers(View view)
        {
            Button translateButton = view.FindViewById<Button>(Resource.Id.translateButton);
            translateButton.Click -= TranslateButton_Click;

            Button sourceButton = view.FindViewById<Button>(Resource.Id.sourceLanguageBtn);
            sourceButton.Click -= SourceButton_Click;

            Button targetButton = view.FindViewById<Button>(Resource.Id.targetLanguageBtn);
            targetButton.Click -= TargetButton_Click;
        }

        private void TargetButton_Click(Object sender, EventArgs e)
        {
            string targetLang = ((TranslatorActivity)Activity).getTargetLanguage();

            Button targetButton = view.FindViewById<Button>(Resource.Id.targetLanguageBtn);

            PopupMenu targetMenu = new PopupMenu(Context, targetButton);
            foreach (var language in languageTable)
            {
                targetMenu.Menu.Add(0, language.WorldLanguageID, language.WorldLanguageID, language.Name);
            }

            targetMenu.MenuItemClick += (s1, arg1) =>
            {
                ((TranslatorActivity)Activity).setTargetLanguage(languageTable[arg1.Item.ItemId - 1].Prefix);
                targetButton.Text = languageTable[arg1.Item.ItemId - 1].Name;
            };

            targetMenu.Show();
        }

        private void SourceButton_Click(Object sender, EventArgs e)
        {
            string sourceLang = ((TranslatorActivity)Activity).getSourceLanguage();

            Button sourceButton = view.FindViewById<Button>(Resource.Id.sourceLanguageBtn);

            PopupMenu sourceMenu = new PopupMenu(Context, sourceButton);
            foreach (var language in languageTable)
            {
                sourceMenu.Menu.Add(0, language.WorldLanguageID, language.WorldLanguageID, language.Name);
            }

            sourceMenu.MenuItemClick += (s1, arg1) =>
            {
                ((TranslatorActivity)Activity).setSourceLanguage(languageTable[arg1.Item.ItemId - 1].Prefix);
                sourceButton.Text = languageTable[arg1.Item.ItemId - 1].Name;
            };

            sourceMenu.Show();
        }

        private void TranslateButton_Click(object sender, EventArgs e)
        {
            string sourceLang = ((TranslatorActivity)Activity).getSourceLanguage();
            ((TranslatorActivity)Activity).setTextToTranslate(View.FindViewById<TextView>(Resource.Id.textToTranslateView).Text);
            if (!String.IsNullOrEmpty(((TranslatorActivity)Activity).getTextToTranslate()))
            {
                try
                {

                    string encodedTextToTranslate = Uri.EscapeDataString(((TranslatorActivity)Activity).getTextToTranslate());
                    if (!sourceLang.Equals("1"))
                    {
                        //Get Translation
                        service.OnResultsObtained = OnResultsObtained;
                        service.GetTranslation(encodedTextToTranslate, sourceLang, ((TranslatorActivity)Activity).getTargetLanguage());
                    }
                    else
                    {
                        //Detect Language
                        service.OnLanguageDetected = OnLanguageDetected;
                        service.OnResultsObtained = OnResultsObtained;
                        service.DetectLanguage(encodedTextToTranslate, ((TranslatorActivity)Activity).getSourceLanguage(), ((TranslatorActivity)Activity).getTargetLanguage());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                }

                //Dismiss Keybaord
                InputMethodManager imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(View.FindViewById<TextView>(Resource.Id.textToTranslateView).WindowToken, 0);

            }
        }

        private void HandlerEvents(View view)
        {
            string sourceLang;
            var source = Activity.Intent.GetStringExtra("Source");
            if (!String.IsNullOrEmpty(source))
            {
                sourceLang = source;
            }
            else
            { 
                sourceLang = ((TranslatorActivity)Activity).getSourceLanguage();
            }
            string targetLang = ((TranslatorActivity)Activity).getTargetLanguage();

            Button translateButton = view.FindViewById<Button>(Resource.Id.translateButton);
            translateButton.Click += TranslateButton_Click;

            Button sourceButton = view.FindViewById<Button>(Resource.Id.sourceLanguageBtn);
            sourceButton.Text = (languageTable.First(x => x.Prefix == sourceLang)).Name;
            sourceButton.Click += SourceButton_Click;
                //var intent = new Intent(this.Context, typeof(SourceLanguageActivity));
                //StartActivity(intent);
                
            Button targetButton = view.FindViewById<Button>(Resource.Id.targetLanguageBtn);
            targetButton.Text = (languageTable.First(x => x.Prefix == targetLang)).Name;
            targetButton.Click += TargetButton_Click;

            var TextToTrans = Activity.Intent.GetStringExtra("TextToTranslate");
            if (!String.IsNullOrEmpty(TextToTrans))
            {
                EditText textToTransView = view.FindViewById<EditText>(Resource.Id.textToTranslateView);
                textToTransView.Text = TextToTrans;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.TranslatorFragment, container, false);

            return view;
        }


        public override void OnStart()
        {
            base.OnStart();
            
            languageTable = ((TranslatorActivity)Activity).getLanguageTable();

            setupControls(view);
            HandlerEvents(view);
            displayResults(((TranslatorActivity)Activity).getTranslatedString());
        }

        public override void OnStop()
        {
            base.OnStop();

            saveText(view);
            removeHandlers(view);
        }


        private class MyClickableSpan : ClickableSpan
        {
            public Action<string> Click;
            private string Word;

            public MyClickableSpan(string word)
            {
                Word = word;
            }

            public override void OnClick(View widget)
            {
                if (Click != null)
                    Click(Word);
            }
        }

    }
}
