using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public static class MaxEntropySentimentAnalizator
    {
        private static LogisticRegressionAnalysis regression;
        public static void Train()
        {
            DataHandler.ImportReviewData(3);

            var maxCount = 1;
            double[][] input = new double[maxCount][];
            for (int i = 0; i < maxCount; i++)
            {
                input[i] = CalculateProbabilities(DataHandler.Reviews[i].reviewText);
            }

            double[] output = DataHandler.Reviews.Take(maxCount).Select(r => r.overall).ToArray();

            LogisticRegressionAnalysis regression = new LogisticRegressionAnalysis();
            LogisticRegression lr = regression.Learn(input, output);
        }

        private static double[] CalculateProbabilities(string text)
        {
            double[] result = new double[DataHandler.Vocabulary.Count];
            for (int i = 0; i < DataHandler.Vocabulary.Count; i++)
            {
                result[i] = Convert.ToDouble(text.Contains(DataHandler.Vocabulary[i]));
            }
            return result;
        }

        public static SentimentAnalysisData AnalyzeReview(ReviewData data)
        {
            SentimentAnalysisData result = new SentimentAnalysisData { Review = data };
            result.SentimentEvaluation = regression.Transform(CalculateProbabilities(data.reviewText));
            return result;
        }
    }
}
