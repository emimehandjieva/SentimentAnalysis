using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public class NLPData
    {
        public long ID { get; set; }
        public string Word { get; set; }
        public string Stem { get; set; }
        public decimal Idf { get; set; }
        public decimal Tf { get; set; }
        public decimal TfIdf { get; set; }


    }
}
