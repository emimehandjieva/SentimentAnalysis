using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public class AnalysisData
    {
        public ReviewData Review
        {
            get;
            set;
        }

        public decimal SentimentEvaluation
        {
            get;
            set;
        }

        public decimal PositiveWordsCount
        {
            get;
            set;
        }

        public decimal NegativeWordsCount
        {
            get;
            set;
        }

    }
}
