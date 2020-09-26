using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.DAL.Service.Models
{
    public class DetectedEmotionsDADto
    {        
        public string ToneID { get; set; }
        public string ToneName { get; set; }        
        public Double Score { get; set; }
    }
}
