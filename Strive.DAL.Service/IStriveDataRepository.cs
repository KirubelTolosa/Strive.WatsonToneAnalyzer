using Strive.DAL.Service.Models;
using System;
using System.Collections.Generic;



namespace Strive.DAL.Service
{
    public interface IStriveDataRepository
    {
        List<ToneAnalysisRecordDADto> GetAllAnalysisResults(int userId, ToneAnalysisResultDADto emotionsDetected);
    }
}
