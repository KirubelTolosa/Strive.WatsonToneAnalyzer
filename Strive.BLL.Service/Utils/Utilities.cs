using Strive.BLL.Service.Models;
using Strive.DAL.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BLL.Service.Utils
{
    public class Utilities
    {
        public static ToneAnalysisResultDADto MapToneAnalysisResultBLDtoToDADto(ToneAnalysisResultBLDto toneAnalysisResult)
        {
            return toneAnalysisResult.ToToneAnalysisResultDADto();
        }

        public static List<ToneAnalysisRecordBLDto> MapToneAnalysisRecordsDADtoToBLDto(List<ToneAnalysisRecordDADto> toneAnalysisRecords)
        {
            return toneAnalysisRecords.ToToneAnalysisRecordsBLDto();
        }
    }
}
