using Strive.BLL.Service.Models;
using System;
using System.Collections.Generic;

namespace Strive.BLL.Service
{
    public interface IStriveBLService
    {
        List<ToneAnalysisRecordBLDto> GetAllAnalysisResults(int userId, string textForToneAnalysis);
        ToneAnalysisResultBLDto AnalyzeTextTone(int userId, string textForToneAnalysis);
    }
}
