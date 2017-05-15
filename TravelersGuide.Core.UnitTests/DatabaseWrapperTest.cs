using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersGuide.Core.Model;
using System.Collections.Generic;


namespace TravelersGuide.Core.UnitTests
{
    [TestClass]
    public class DatabaseWrapperTest
    {

        private DatabaseWrapper db;
        private List<WorldLanguage> Languages;

        [TestInitialize]
        public void Setup()
        {
            db = new DatabaseWrapper(@"C: \Users\Zain\Documents\Visual Studio 2015\Projects\TravelersGuide\TravelersGuide.Core\Database\TravelersGuide.db");
        }

        [TestMethod]
        public void TestGetAllLanguages()
        {
            Setup();

            try
            {
                db.GetAllLanguages();
            }

            catch (Exception ex)
            {

            }
        }


        public void GetAllLanguages()
        {

            try
            {
                Languages = db.GetAllLanguages();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

    }
}
