using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public static class NaiveBayesSentimentAnalizator
    {
        static BayesClassifier classifier;

        public static void Train()
        {
            classifier = new BayesClassifier("1", "2", "3", "4", "5");
            DataHandler.ImportReviewData(3);
            for (int i = 1; i <= 5; i++)
            {
                var data = DataHandler.Reviews.Where(r => r.overall == i);

                foreach (ReviewData item in data)
                {
                    var text = item.reviewText;
                    classifier.Train(i.ToString(), text);
                }
            }
        }

        public static SentimentAnalysisData AnalyzeReview(ReviewData data)
        {
            SentimentAnalysisData result = new SentimentAnalysisData { Review = data };
            result.SentimentEvaluation = int.Parse(classifier.Classify(result.Review.reviewText));
            return result;
        }
    }
}
