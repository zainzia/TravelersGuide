using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelersGuide.Core.Model;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TravelersGuide.Core.Service
{
    public class TranslationService
    {
        

        //private string textToTranslate;
        //private string translatedText;
        //private string sourceLanguage;
        //private string targetLanguage;
        //private bool AutoDetectResponse;
        private DatabaseWrapper db;


        public TranslationService(string dbPath)
        {
            db = new DatabaseWrapper(dbPath);
        }


        public string GetTranslation(string TextToTranslate, string source, string target)
        {
        }

    }
}
