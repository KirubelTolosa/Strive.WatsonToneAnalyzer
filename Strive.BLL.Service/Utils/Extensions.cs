using Strive.BLL.Service.Models;
using Strive.DAL.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BLL.Service.Utils
{
    public static class Extensions
    {
        public static ToneAnalysisResultDADto ToToneAnalysisResultDADto(this ToneAnalysisResultBLDto BLLResult)
        {
            return new ToneAnalysisResultDADto
            {
                 UserId = BLLResult.UserID,
                 Date = BLLResult.Date,
                 Text = BLLResult.Text,
                 DetectedEmotions = ToDALDetectedEmotions(BLLResult.DetectedEmotions),
                 PrevailingEmotion = BLLResult.PrevailingEmotion
            };
        }
        public static List<ToneAnalysisRecordBLDto> ToToneAnalysisRecordsBLDto(this List<ToneAnalysisRecordDADto> DALRecords)
        {
            List<ToneAnalysisRecordBLDto> analysisRecordsBLDto = new List<ToneAnalysisRecordBLDto>();
            foreach(var analysisRecord in DALRecords)
            {
                analysisRecordsBLDto.Add(new ToneAnalysisRecordBLDto
                {
                    UserId = analysisRecord.UserId,
                    Text = analysisRecord.Text,
                    Date = analysisRecord.Date,
                    PrevailingEmotion = analysisRecord.PrevailingEmotion,
                    Joy = analysisRecord.Joy,
                    Anger = analysisRecord.Anger,
                    Fear = analysisRecord.Anger,
                    sadness = analysisRecord.sadness,
                    others = analysisRecord.others
                });
            }
            return analysisRecordsBLDto;
        }
        public static List<DetectedEmotionsDADto> ToDALDetectedEmotions(List<DetectedEmotionsBLDto> detectedEmotions)
        {
            List<DetectedEmotionsDADto> detectedEmotionsDADto = new List<DetectedEmotionsDADto>();
            foreach (var emotion in detectedEmotions)
            {
                detectedEmotionsDADto.Add(new DetectedEmotionsDADto
                {
                    ToneID = emotion.tone_id,
                    ToneName = emotion.tone_name,
                    Score = emotion.score
                });
            }
            return detectedEmotionsDADto;
        }
    }
}
