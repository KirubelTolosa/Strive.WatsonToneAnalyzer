using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.DAL.Service.Models
{
    public class ToneAnalysisResultDADto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string PrevailingEmotion { get; set; }
        public List<DetectedEmotionsDADto> DetectedEmotions { get; set; }
    }
}
