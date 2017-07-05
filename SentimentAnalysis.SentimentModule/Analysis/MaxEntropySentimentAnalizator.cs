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
        private static LogisticRegression regression;
        public static void Train()
        {
            DataHandler.ImportReviewData(3);
            var learner = new IterativeReweightedLeastSquares<LogisticRegression>()
            {
                Tolerance = 1e-4,  // Let's set some convergence parameters
                Iterations = 100,  // maximum number of iterations to perform
                Regularization = 0
            };
            double[][] input = new double[DataHandler.Reviews.Count][];
            for (int i = 0; i < DataHandler.Reviews.Count; i++)
            {
                input[i] = CalculateProbabilities(DataHandler.Reviews[i].reviewText);
            }

            double[] output = DataHandler.Reviews.Select(r => r.overall).ToArray();
            regression = learner.Learn(input, output);
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
            result.SentimentEvaluation = regression.Probability(CalculateProbabilities(data.reviewText));
            return result;
        }
    }
}
