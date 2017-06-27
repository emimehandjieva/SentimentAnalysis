using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public class SentimentAnalysisData 
    {

        public ReviewData Review
        {
            get;
            set;
        }

        public double SentimentEvaluation
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

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.AppendLine("Review ID: " +Review.asin);
            build.AppendLine("Review text: " +Review.reviewText);
            build.AppendLine("Review evaluation: " +SentimentEvaluation);
            build.AppendLine("Positive words detected: " +PositiveWordsCount);
            build.AppendLine("Negative words detected: " +NegativeWordsCount);

            return build.ToString();

        }

    }
}
