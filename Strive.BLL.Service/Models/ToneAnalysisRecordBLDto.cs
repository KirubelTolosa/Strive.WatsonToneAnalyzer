using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BLL.Service.Models
{
    public class ToneAnalysisRecordBLDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string PrevailingEmotion { get; set; }
        public double Joy { get; set; }
        public double Anger { get; set; }
        public double sadness { get; set; }
        public double Fear { get; set; }
        public double others { get; set; }
    }
}
