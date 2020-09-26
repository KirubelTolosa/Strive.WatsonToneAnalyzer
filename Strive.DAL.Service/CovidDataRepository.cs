using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Strive.DAL.Service.Models;

namespace Strive.DAL.Service
{
    public class StriveDataRepository : IStriveDataRepository
    {
        private IConfiguration _configuration;
        public StriveDataRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Dictionary<string, List<string>> GetListOfEmotions()
        {
            Dictionary<string, List<string>> emotionsList = new Dictionary<string, List<string>>();
            emotionsList.Add("Joy", new List<string>
            {
                "Confident",
                "Excitement",
                "Politeness",
                "Satisfaction",
                "Joy"
            });
            emotionsList.Add("Sadness", new List<string>
            {
                "Sadness",
                "Sympathy"
            });
            emotionsList.Add("Anger", new List<string>
            {
                "Frustration",               
                "Impoliteness"
            });
            emotionsList.Add("Fear", new List<string>
            {
                "Tentative",
                "Fear"
            });
            return emotionsList;
        }
        private void InsertEmotions(ToneAnalysisResultDADto emotions)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("StriveDatabase")))
            {
                connection.Open();
                SqlCommand commandInsertIntoJournal = new SqlCommand(@"Insert into Journal(UserID, Date, Text, PrevailingEmotion, Joy, Anger, Fear, Sadness, other) values (@id, @dte, @txt, @pem, @joy, @ang, @fr, @sd, @oth)", connection);
                SqlParameter id = new SqlParameter("id", System.Data.SqlDbType.Int);
                SqlParameter dte = new SqlParameter("dte", System.Data.SqlDbType.DateTime);
                SqlParameter txt = new SqlParameter("txt", System.Data.SqlDbType.NVarChar);
                SqlParameter pem = new SqlParameter("pem", System.Data.SqlDbType.NVarChar);
                SqlParameter joy = new SqlParameter("joy", System.Data.SqlDbType.Float);
                SqlParameter sd = new SqlParameter("sd", System.Data.SqlDbType.Float);
                SqlParameter ang = new SqlParameter("ang", System.Data.SqlDbType.Float);
                SqlParameter fr = new SqlParameter("fr", System.Data.SqlDbType.Float);
                SqlParameter oth = new SqlParameter("oth", System.Data.SqlDbType.Float);

                commandInsertIntoJournal.Parameters.Add(id);
                commandInsertIntoJournal.Parameters.Add(dte);
                commandInsertIntoJournal.Parameters.Add(txt);
                commandInsertIntoJournal.Parameters.Add(pem);
                commandInsertIntoJournal.Parameters.Add(joy);
                commandInsertIntoJournal.Parameters.Add(sd);
                commandInsertIntoJournal.Parameters.Add(ang);
                commandInsertIntoJournal.Parameters.Add(fr);
                commandInsertIntoJournal.Parameters.Add(oth);
                id.Value = emotions.UserId;
                dte.Value = emotions.Date;
                txt.Value =  emotions.Text;
                pem.Value =  emotions.PrevailingEmotion;
                var listOfEmotions = GetListOfEmotions();
                joy.Value = 0;
                sd.Value = 0;
                ang.Value = 0;
                fr.Value = 0;
                oth.Value = 0;
                foreach (var emotion in emotions.DetectedEmotions)
                {
                    if(listOfEmotions["Joy"].Contains(new CultureInfo("en-US", false).TextInfo.ToTitleCase(emotion.ToneName)))
                    {
                        joy.Value = emotion.Score;
                    }
                    else if (listOfEmotions["Sadness"].Contains(new CultureInfo("en-US", false).TextInfo.ToTitleCase(emotion.ToneName)))
                    {
                        sd.Value = emotion.Score;
                    }
                    else if (listOfEmotions["Anger"].Contains(new CultureInfo("en-US", false).TextInfo.ToTitleCase(emotion.ToneName)))
                    {
                        ang.Value = emotion.Score;
                    }
                    else if (listOfEmotions["Fear"].Contains(new CultureInfo("en-US", false).TextInfo.ToTitleCase(emotion.ToneName)))
                    {
                        fr.Value = emotion.Score;
                    }
                    else
                    {
                        oth.Value = emotion.Score;
                    }
                }
                SqlTransaction transaction;
                transaction = connection.BeginTransaction();
                commandInsertIntoJournal.Transaction = transaction;
                try
                {
                    commandInsertIntoJournal.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    throw new Exception("User Creation Failed!");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<ToneAnalysisRecordDADto> GetListOfAnalysisRecords(int userId)
        {
            List<ToneAnalysisRecordDADto> analysisRecords = new List<ToneAnalysisRecordDADto>();            
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("StriveDatabase")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"Select 
                                                           UserId, Date, Text, PrevailingEmotion, Joy, Anger, Fear, Sadness, other                    
                                                        From Journal Where UserId = @id Order By [Date] Desc", connection);
                SqlParameter id = new SqlParameter("id", System.Data.SqlDbType.Int);
                command.Parameters.Add(id);
                id.Value = userId; 
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        analysisRecords.Add(new ToneAnalysisRecordDADto
                        {
                            UserId = (Int32)reader["UserId"],
                            Date  = (DateTime)reader["Date"],
                            Text = (string)reader["Text"],
                            PrevailingEmotion = (string)reader["PrevailingEmotion"],
                            Joy = (Double)reader["Joy"],
                            Anger = (Double)reader["Anger"],
                            Fear = (Double)reader["Fear"],
                            sadness = (Double)reader["Sadness"],
                            others = (Double)reader["other"]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return analysisRecords;
        }
        public List<ToneAnalysisRecordDADto> GetAllAnalysisResults(int userId, ToneAnalysisResultDADto emotionsDetected)
        {            
            InsertEmotions(emotionsDetected);
            return GetListOfAnalysisRecords(userId);
        }
    }
}




