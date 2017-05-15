using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersGuide.Core.Service;
using TravelersGuide.Core.Model;
using System.Collections.Generic;


namespace TravelersGuide.Core.UnitTests
{
    [TestClass]
    public class TranslationServiceTest
    {
        private string mocJSONString;
        private TranslationService testService;

        [TestInitialize]
        private void Setup()
        {
            mocJSONString = "{\n \"data\": {\n  \"translations\": [\n   {\n    \"translatedText\": \"Hello\"\n   }\n  ]\n }\n}\n";
        }

        [TestMethod]
        public void TestDesrializeTranslatedResponse()
        {
            Setup();

            testService = new TranslationService("");
            testService.DesrializeTranslatedResponse(mocJSONString);
            //Assert.AreEqual(response, "Hello");
        }

    }
}
