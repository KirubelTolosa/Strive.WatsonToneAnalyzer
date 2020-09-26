using Strive.BLL.Service.Models;
using Strive.BLL.Service;
using Strive.DAL.Service;
using CsvHelper;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using IBM.Watson.ToneAnalyzer.v3;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using Strive.DAL.Service.Models;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.ToneAnalyzer.v3.Model;
using System.Text.Json;
using Newtonsoft.Json;

namespace Strive.BLL.Service
{
    public class StriveBLService : IStriveBLService
    {
        private IStriveDataRepository striveDataRepository;
        private IConfiguration configuration;
        public ToneAnalyzerService service;
        public StriveBLService(IStriveDataRepository striveDataRepository, IConfiguration configuration)
        {
            this.striveDataRepository = striveDataRepository;
            this.configuration = configuration;
        }
        
        public Dictionary<string, List<string>> GetListOfEmotions()
        {
            Dictionary<string, List<string>> emotionsList = new Dictionary<string, List<string>>();
            emotionsList.Add("Joy", new List<string>
            {
                "Confident",
                "Excitement",
                "Politeness",
                "Satisfaction"
            });
            emotionsList.Add("Sadness", new List<string>
            {
                "Sadness",
                "Sympathy"                
            });
            emotionsList.Add("Anger", new List<string>
            {
                "Frustration",
                "Analytical",
                "Impoliteness"
            });
            emotionsList.Add("Fear", new List<string>
            {
                "Tentative",
                "Fear"
            });
            return emotionsList;
        }
        public ToneAnalysisResultBLDto AnalyzeTextTone(int userId, string textForToneAnalysis)
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "zHCE9sv_3E_frWM9q41CcUDc3uAi5e_6Z4jpi0Y9khlP");
            var service = new ToneAnalyzerService("2017-09-21", authenticator);
            service.SetServiceUrl("https://api.us-south.tone-analyzer.watson.cloud.ibm.com/instances/083c88e7-7cd0-4762-b4c4-09acfe0a4c5c");            
            ToneInput toneInput = new ToneInput()
            {
                Text = textForToneAnalysis
            };
            var result = service.Tone(
                toneInput: toneInput
                );
            WatsonResponseBLDto watsonResponse = JsonConvert.DeserializeObject<WatsonResponseBLDto>(result.Response);
            ToneAnalysisResultBLDto analysisResult = new ToneAnalysisResultBLDto {
                UserID = userId,
                Date = DateTime.Now,
                Text = textForToneAnalysis,
                DetectedEmotions = watsonResponse.document_tone.tones                  
            };
            double emotionScore = 0;
            string prevailingEmotion = "";
            foreach (var emotion in analysisResult.DetectedEmotions)
            {                
                if(emotion.score > emotionScore)
                {
                    emotionScore = emotion.score;
                    prevailingEmotion = emotion.tone_name;
                }
            }            
            foreach(KeyValuePair<string, List<string>> emotionType in GetListOfEmotions())
            {
                if (emotionType.Value.Contains(prevailingEmotion))
                {
                    analysisResult.PrevailingEmotion = emotionType.Key;
                }               
            }    
            if(analysisResult.PrevailingEmotion == null)
            {
                analysisResult.PrevailingEmotion = "OtherEmotion";
            }
            return analysisResult;
        }

        public List<ToneAnalysisRecordBLDto> GetAllAnalysisResults(int userId, string textForToneAnalysis)
        {
            ToneAnalysisResultBLDto emotionsDetected = AnalyzeTextTone(userId, textForToneAnalysis);
            return Utils.Utilities.MapToneAnalysisRecordsDADtoToBLDto(striveDataRepository.GetAllAnalysisResults(userId, Utils.Utilities.MapToneAnalysisResultBLDtoToDADto(emotionsDetected)));
        }
    }
}
