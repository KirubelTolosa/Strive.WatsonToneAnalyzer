using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BLL.Service.Models
{
    public class DetectedEmotionsBLDto
    {        
        public double score { get; set; }
        public string tone_id { get; set; }
        public string tone_name { get; set; }
    }
}
