using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelersGuide.Core.Model
{
    public class TranslatedResponse
    {

        public string translatedText { get; set; }
        public bool Error { get; set; }

        public TranslatedResponse()
        {

        }

    }
}
