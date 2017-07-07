using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public class MultipleSentimentAnalysisData
    {
        public string ReviewText
        {
            get;
            set;
        }

        public double Grade
        {
            get;
            set;
        }

        public double LexiconSentimentEvaluation
        {
            get;
            set;
        }

        public double NaiveBayesSentimentEvaluation
        {
            get;
            set;
        }

        public double SVMSentimentEvaluation
        {
            get;
            set;
        }
        
    }
}
