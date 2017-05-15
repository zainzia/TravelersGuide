using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

namespace TravelersGuide.Core.Model
{
    public class DatabaseWrapper
    {

        private SQLiteConnection connection;
        private string dbPath;

        public DatabaseWrapper(string path)
        {
            dbPath = path;
        }


        public string getLastTranslatedString()
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                var lastRecord = connection.Query<TranslatedText>("SELECT * FROM TranslatedText ORDER BY TranslatedTextID DESC").FirstOrDefault();
                if (lastRecord == null)
                    return "";
                return lastRecord.Text2;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return "";
        }


        public string getLastTextToTranslate()
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                var lastRecord = connection.Query<TextToTranslate>("SELECT * FROM TextToTranslate ORDER BY TextToTranslateID DESC").FirstOrDefault();
                connection.Close();
                if (lastRecord == null)
                    return "";

                return lastRecord.Text1;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return "";
        }

        public List<CustomImageDetail> GetImageDetail(int ImageID)
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                string query = "SELECT CapturedImages.*, ImageTranslation.*, TextTranslation.*, TranslatedText.*, TextToTranslate.*" +
                                " FROM CapturedImages" +
                                " LEFT JOIN ImageTranslation" +
                                " ON ImageTranslation.ImageID = CapturedImages.CapturedImagesID" +
                                " LEFT JOIN TextTranslation" +
                                " ON TextTranslation.TextTranslationID = ImageTranslation.TranslationID" +
                                " LEFT JOIN TranslatedText" +
                                " ON TranslatedText.TranslatedTextID = TextTranslation.TranslatedText" +
                                " LEFT JOIN TextToTranslate" +
                                " ON TextToTranslate.TextToTranslateID = CapturedImages.TextToTransID" +
                                " WHERE CapturedImages.CapturedImagesID = " + ImageID;

                var imageDetail = connection.Query<CustomImageDetail>(query);
                return imageDetail;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        
        public void UpdateImageCaption(int CapturedImagesID, string Caption)
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                var query = "UPDATE CapturedImages SET Caption = '" + Caption + "' WHERE CapturedImagesID = " + CapturedImagesID;

                int result = connection.Execute(query);

                if (result != 1)
                {
                    Logger.Log("Could not update Caption for Image ID " + CapturedImagesID);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }


        public int InsertCapturedImage(string path, string Name, int height, int width, string thumbnailPath)
        {
            try
            {
                var capturedImage = new CapturedImages()
                {
                    ImageName = Name,
                    ImagePath = path,
                    ImageHeight = height,
                    ThumbnailPath = thumbnailPath,
                    ImageWidth = width,
                    DateImageCaptured = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                };

                connection = new SQLiteConnection(dbPath, false);
                var returnedVal = connection.Insert(capturedImage);

                if (returnedVal < 1)
                {
                    Logger.Log("Could not insert Captured Image in DB");
                }
                return capturedImage.CapturedImagesID;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return 1;
        }

        public void AddDetectedTextToImage(int ImageID, string DetectedText)
        {
            try
            {
                var TextDetected = 0;
                string query;
                connection = new SQLiteConnection(dbPath, false);

                if (!String.IsNullOrEmpty(DetectedText))
                {
                    TextDetected = 1;

                    //Add Detected text into TextToTranslate
                    TextToTranslate dText = new TextToTranslate();
                    dText.Text1 = DetectedText;

                    connection.Insert(dText);

                    query = "UPDATE CapturedImages SET TextDetected = " + TextDetected + ", TextToTransID = " + dText.TextToTranslateID + ", DateTextDetected = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' WHERE CapturedImagesID = " + ImageID;
                }
                else
                {
                    query = "UPDATE CapturedImages SET TextDetected = " + TextDetected + ", DateTextDetected = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' WHERE CapturedImagesID = " + ImageID;
                }

                int result = connection.Execute(query);

                if (result != 1)
                {
                    Logger.Log("Could not Add detected Text to Image with ID " + ImageID);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        public List<TranslationsFromID> GetAllTranslations(int TextToTranslateID)
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                
                var translations = connection.Query<TranslationsFromID>("SELECT TextTranslation.*, TextToTranslate.*, TranslatedText.*, WorldLanguage.*" +
                                                                        " FROM TextTranslation" +
                                                                        " LEFT JOIN TextToTranslate" +
                                                                        " ON TextToTranslate.TextToTranslateID = TextTranslation.TextToTranslate" +
                                                                        " LEFT JOIN TranslatedText" +
                                                                        " ON TranslatedText.TranslatedTextID = TextTranslation.TranslatedText" +
                                                                        " LEFT JOIN WorldLanguage" +
                                                                        " ON WorldLanguage.WorldLanguageID = TextTranslation.Target" + 
                                                                        " WHERE TextTranslation.TextToTranslate = " + TextToTranslateID +
                                                                        " ORDER BY DateCreated DESC");

                if (translations.Count == 0)
                {
                    var textToTranslate = connection.Query<TextToTranslate>("SELECT * FROM TextToTranslate WHERE TextToTranslate.TextToTranslateID = " + TextToTranslateID).FirstOrDefault();

                    var translationsObject = new TranslationsFromID() { Text1 = textToTranslate.Text1 };
                    translationsObject.SourceName = new SourceNameFromID();
                    translationsObject.SourceName.Name = "Auto Detect";
                    translationsObject.SourceName.Prefix = "1";

                    translations.Add(translationsObject);
                }
                else
                {
                    var sourceNameQuery = connection.Query<SourceNameFromID>("SELECT TextTranslation.Source, WorldLanguage.Name, WorldLanguage.Prefix" +
                                                                            " FROM TextTranslation" +
                                                                            " LEFT JOIN WorldLanguage" +
                                                                            " ON WorldLanguage.WorldLanguageID = TextTranslation.Source" +
                                                                            " WHERE TextTranslation.TextToTranslate = " + TextToTranslateID);

                    translations[0].SourceName = new SourceNameFromID();
                    translations[0].SourceName.Name = sourceNameQuery[0].Name;
                    translations[0].SourceName.Prefix = sourceNameQuery[0].Prefix;
                }
                    
                return translations;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return null;

        }

        public List<CapturedImages> GetAllCapturedImages()
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                var capturedImages = connection.Query<CapturedImages>("SELECT * FROM CapturedImages LEFT JOIN TextToTranslate ON TextToTranslate.TextToTranslateID = CapturedImages.TextToTransID ORDER BY CapturedImages.DateImageCaptured DESC");
                return capturedImages;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public int InsertTranslationHistory(int source, int target, string textToTranslate, string translatedText, string notes, int textToTranslateID)
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);

                if (textToTranslateID == 0)
                {
                    var TextToTranslate = new TextToTranslate() { Text1 = textToTranslate };
                    connection.Insert(TextToTranslate);
                    textToTranslateID = TextToTranslate.TextToTranslateID;
                }

                var TranslatedText = new TranslatedText() { Text2 = translatedText };
                connection.Insert(TranslatedText);
                var TranslatedTextID = TranslatedText.TranslatedTextID;

                var translationHistory = new TextTranslation()
                {
                    Source = source,
                    Target = target,
                    TextToTranslate = textToTranslateID,
                    TranslatedText = TranslatedTextID,
                    DateCreated = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    Notes = notes
                };

                var translationHistoryID = connection.Insert(translationHistory);

                if (translationHistoryID < 1)
                {
                    Logger.Log("Could not insert History in DB");
                }

                return translationHistory.ID;

            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return 1;
        }

        public int GetTextToTransIDFromTranslation(int TextTranslationID)
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                var query = "SELECT * FROM TextTranslation WHERE TextTranslationID = " + TextTranslationID;
                var textTranslation = connection.Query<TextTranslation>(query).FirstOrDefault();

                return textTranslation.TextToTranslate;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return 1;
        }

        public List<CustomTranslationHistory> GetAllTranslationsForItem(int translationHistoryID)
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                string query = "SELECT * FROM TextTranslation WHERE ID = " + translationHistoryID;
                var translationHistoryForItem = connection.Query<TextTranslation>(query).FirstOrDefault();

                query = "SELECT TextTranslation.TextTranslationID AS ID, TextTranslation.Source AS Source, TextTranslation.Target AS Target, TextTranslation.DateCreated AS DateCreated," +
                                " TextToTranslate.Text1 AS TextToTranslate, TranslatedText.Text2 AS TranslatedText, TextTranslation.Notes AS Notes" +
                                " FROM TextTranslation" +
                                " LEFT JOIN TextToTranslate ON TextToTranslate.TextToTranslateID = TextTranslation.TextToTranslate" +
                                " LEFT JOIN TranslatedText ON TranslatedText.TranslatedTextID = TextTranslation.TranslatedText" +
                                " WHERE TextTranslation.Text = " + translationHistoryForItem.ID +
                                " ORDER BY DateCreated DESC";
                var history = connection.Query<CustomTranslationHistory>(query);
                
                return history;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return null;
        }


        public List<CustomTranslationHistory> GetAllTranslationHistory()
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);

                string Query = "SELECT TextTranslation.TextTranslationID AS ID, TextTranslation.Source AS Source, TextTranslation.Target AS Target, TextTranslation.DateCreated AS DateCreated," +
                                " TextToTranslate.Text1 AS TextToTranslate, TranslatedText.Text2 AS TranslatedText, TextTranslation.Notes AS Notes, TextToTranslate.TextToTranslateID as TextToTransID"+ 
                                " FROM TextTranslation" + 
                                " LEFT JOIN TextToTranslate ON TextToTranslate.TextToTranslateID = TextTranslation.TextToTranslate" + 
                                " LEFT JOIN TranslatedText ON TranslatedText.TranslatedTextID = TextTranslation.TranslatedText" +
                                " GROUP BY TextToTransID" + 
                                " ORDER BY DateCreated DESC";

                var history = connection.Query<CustomTranslationHistory>(Query);
                return history;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public List<WorldLanguage> GetAllLanguages()
        {
            //If open return

            try
            {
                //open sqlite connection
                connection = new SQLiteConnection(dbPath, false);
                return connection.Query<WorldLanguage>("SELECT * FROM WorldLanguage");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }      

            return null;
        }

        public string GetSourceLanguagePrefix()
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                FavoriteLanguage lang = connection.Query<FavoriteLanguage>("SELECT * FROM FavoriteLanguage WHERE ID = 1").FirstOrDefault();
                return lang.Prefix;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return "";
        }

        public void setSourceLanguage(string text)
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                int result = connection.Execute("UPDATE FavoriteLanguage SET Prefix = '" + text + "' WHERE ID = 1");
                if(result != 1)
                {
                    Logger.Log("Could not update Source Language");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }


        public string GetTargetLanguagePrefix()
        {
            try
            { 
                connection = new SQLiteConnection(dbPath, false);
                FavoriteLanguage lang = connection.Query<FavoriteLanguage>("SELECT * FROM FavoriteLanguage WHERE ID = 2").FirstOrDefault();
                return lang.Prefix;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return "";
        }

        public void setTargetLanguage(string text)
        {
            try
            {
                connection = new SQLiteConnection(dbPath, false);
                int result = connection.Execute("UPDATE FavoriteLanguage SET Prefix = '" + text + "' WHERE ID = 2");
                if (result != 1)
                {
                    Logger.Log("Could not update Target Language");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        public WorldLanguage GetLanguageFromPrefix(string prefix)
        {
            try
            { 
                //open sqlite connection
                connection = new SQLiteConnection(dbPath, false);
                return connection.Query<WorldLanguage>("SELECT * FROM WorldLanguage WHERE Prefix = '"+ prefix + "'").FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public List<WorldLanguage> GetAllLanguages(string databasePath)
        {
            try
            { 
                List<WorldLanguage> languages;

                connection = new SQLiteConnection(databasePath);
                languages = connection.ExecuteScalar<List<WorldLanguage>>("SELECT * FROM WorldLanguages");
                return languages;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        private int CreateDatabase(string Path)
        {
            try
            {
                connection = new SQLiteConnection(Path);

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
