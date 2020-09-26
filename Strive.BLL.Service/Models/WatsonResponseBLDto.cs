using IBM.Watson.ToneAnalyzer.v3.Model;
using Strive.DAL.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BLL.Service.Models
{    
    
    public class DocumentTone
    {
        public List<DetectedEmotionsBLDto> tones { get; set; }
    }

    public class WatsonResponseBLDto
    {
        public DocumentTone document_tone { get; set; }
    }

}
