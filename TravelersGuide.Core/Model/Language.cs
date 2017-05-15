using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TravelersGuide.Core.Model
{
    public class WorldLanguage
    {
        [PrimaryKey, AutoIncrement]
        public int WorldLanguageID { get; set; }

        public string Name { get; set; }

        public string Prefix { get; set; }
    }

    public class FavoriteLanguage
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Prefix { get; set; }
    }

    public class CustomTranslationHistory
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int Source { get; set; }

        public int Target { get; set; }

        public int TextToTransID { get; set; }

        public string TextToTranslate { get; set;  }

        public string TranslatedText { get; set; }

        public string DateCreated { get; set; }

        public string Notes { get; set; }

    }

    public class TextTranslation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int Source { get; set; }

        public int Target { get; set; }

        public int TextToTranslate { get; set; }

        public int TranslatedText { get; set; }

        public string DateCreated { get; set; }

        public string Notes { get; set; }

    }

    public class TextToTranslate
    {
        [PrimaryKey, AutoIncrement]
        public int TextToTranslateID { get; set; }

        public string Text1 { get; set; }
    }

    public class TranslatedText
    {
        [PrimaryKey, AutoIncrement]
        public int TranslatedTextID { get; set; }

        public string Text2 { get; set; }
    }

    public class CapturedImages
    {
        [PrimaryKey, AutoIncrement]
        public int CapturedImagesID { get; set; }

        public string ImagePath { get; set; }

        public string ImageName { get; set; }

        public string TextDetected { get; set; }

        public int ImageHeight { get; set; }

        public int ImageWidth { get; set; }

        public string Caption { get; set; }

        public string DateImageCaptured { get; set; }

        public string DateTextDetected { get; set; }

        public string ThumbnailPath { get; set; }
    }


    public class ImageTranslation
    {
        [PrimaryKey, AutoIncrement]
        public int ImageTranslationID { get; set; }

        public int ImageID { get; set; }
        
        public int TranslationID { get; set; } 
    }


    public class CustomImageDetail
    {
        public int CapturedImagesID { get; set; }

        public string ImagePath { get; set; }

        public string ImageName { get; set; }

        public int TextDetected { get; set; }

        public int ImageHeight { get; set; }

        public int ImageWidth { get; set; }

        public int TextToTransID { get; set; }

        public string Caption { get; set; }

        public string DateImageCaptured { get; set; }

        public string DateTextDetected { get; set; }

        public string ThumbnailPath { get; set; }

        public int ImageTranslationID { get; set; }

        public int ImageID { get; set; }

        public int TranslationID { get; set; }


        public int TextTranslationID { get; set; }
        public int Source { get; set; }
        public int Target { get; set; }
        public int TextToTranslate { get; set; }
        public int TranslatedText { get; set; }
        public string DateCreated { get; set; }
        public string Notes { get; set; }

        public string TextToTranslateID { get; set; }
        public string Text1 { get; set; }
    }


    public class TranslationsFromID
    {
        public int TextTranslationID { get; set; }
        public int Source { get; set; }
        public int Target { get; set; }

        public int TextToTranslate { get; set; }
        public int TranslatedText { get; set; }
        public string DateCreated { get; set; }
        public string Notes { get; set; }

        public int TextToTranslateID { get; set; }
        public string Text1 { get; set; }
        public int TranslatedTextID { get; set; }
        public string Text2 { get; set; }

        public int WorldLanguageID { get; set; }
        public string Name { get; set; }

        public SourceNameFromID SourceName { get; set; }
    }

    public class SourceNameFromID
    {
        public string Name { get; set; }
        public int Source { get; set; }

        public string Prefix { get; set; }
    }

    


}
