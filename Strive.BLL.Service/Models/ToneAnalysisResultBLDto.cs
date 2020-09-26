using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BLL.Service.Models
{
    public class ToneAnalysisResultBLDto
    {
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string PrevailingEmotion { get; set; }
        public List<DetectedEmotionsBLDto> DetectedEmotions { get; set; }
    }
}
