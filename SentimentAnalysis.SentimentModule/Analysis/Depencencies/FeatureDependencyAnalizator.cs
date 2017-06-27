using Accord.Statistics.Models.Regression.Linear;
using Accord.Statistics.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public static class FeatureDependencyAnalizator
    {
        private static List<SentimentAnalysisData> analyzedReviews;
        

        public static List<SentimentAnalysisData> AnalyzedReviews
        {
            get
            {
                if(analyzedReviews == null)
                {
                    AnalyzeReviews();
                }
                return analyzedReviews;
            }

            set
            {
                analyzedReviews = value;
            }
        }

        private static void AnalyzeReviews()
        {
            analyzedReviews = new List<SentimentAnalysisData>();
            foreach (ReviewData item in DataHandler.Reviews)
            {
                analyzedReviews.Add(SentimentAnalizator.AnalyzeReview(item));
            }
        }

        public static SimpleLinearRegression AnalyzeMonthToGradeDependency()
        {
            CalculateCorrelationMonthToGrade();
            OrdinaryLeastSquares ols = new OrdinaryLeastSquares();
            SimpleLinearRegression regression = ols.Learn(DataHandler.Reviews.Select(r => (double)r.reviewTime.Month).ToArray(), DataHandler.Reviews.Select(r => r.overall).ToArray());

            double s = regression.Slope;     
            double c = regression.Intercept;

            return regression;
        }

        public static double CalculateCorrelationMonthToGrade()
        {
            var reviews = DataHandler.Reviews;
            var avgGrade = reviews.Average(r => r.overall);
            var avgMonth = reviews.Average(r => r.reviewTime.Month);

            var confidence = reviews.Sum(r => (r.overall - avgGrade) * (r.reviewTime.Month - avgMonth)) / reviews.Sum(r => Math.Pow((r.overall - avgGrade), 2) * Math.Pow((r.reviewTime.Month - avgMonth), 2));

            var ttest =confidence* Math.Sqrt((reviews.Count() - 2) / 1 -(confidence*confidence));

            return ttest;
        }

        public static SimpleLinearRegression AnalyzeYearToGradeDependency()
        {
            CalculateCorrelationYearToGrade();
            OrdinaryLeastSquares ols = new OrdinaryLeastSquares();
            SimpleLinearRegression regression = ols.Learn(DataHandler.Reviews.Select(r =>Math.Abs(2000- (double)r.reviewTime.Year)).ToArray(), DataHandler.Reviews.Select(r => r.overall).ToArray());

            double s = regression.Slope;
            double c = regression.Intercept;

            //var z = regression.Transform(9);
            //var z1 = regression.Transform(14);
            //var z2 = regression.Transform(11);
            //var z3 = regression.Transform(1);
            //var z4 = regression.Transform(2);

            return regression;
        }

        public static double CalculateCorrelationYearToGrade()
        {
            var reviews = DataHandler.Reviews;
            double avgGrade = reviews.Average(r => r.overall);
            double avgMonth = reviews.Average(r => r.reviewTime.Year);

            double confidence = reviews.Sum(r => (r.overall - avgGrade) * (r.reviewTime.Year - avgMonth)) / reviews.Sum(r => Math.Pow((r.overall - avgGrade), 2) * Math.Pow((r.reviewTime.Year - avgMonth), 2));

            double ttest = confidence * Math.Sqrt((reviews.Count() - 2) / 1 - (confidence * confidence));

            return ttest;
         }

        public static SimpleLinearRegression AnalyzeYearToSentimentDependency()
        {
            CalculateCorrelationYearToSentiment();
            OrdinaryLeastSquares ols = new OrdinaryLeastSquares();
            SimpleLinearRegression regression = ols.Learn(DataHandler.Reviews.Select(r => Math.Abs(2000 - (double)r.reviewTime.Year)).ToArray(), DataHandler.Reviews.Select(r => r.overall).ToArray());

            double s = regression.Slope;
            double c = regression.Intercept;

            //var z = regression.Transform(9);
            //var z1 = regression.Transform(14);
            //var z2 = regression.Transform(11);
            //var z3 = regression.Transform(1);
            //var z4 = regression.Transform(2);

            return regression;
        }

        public static double CalculateCorrelationYearToSentiment()
        {
            double avgGrade = AnalyzedReviews.Average(r => r.SentimentEvaluation);
            double avgMonth = AnalyzedReviews.Average(r => r.Review.reviewTime.Year);

            double confidence = AnalyzedReviews.Sum(r => (r.SentimentEvaluation - avgGrade) * (r.Review.reviewTime.Year - avgMonth)) / AnalyzedReviews.Sum(r => Math.Pow(((double)r.SentimentEvaluation - avgGrade), 2) * Math.Pow((r.Review.reviewTime.Year - avgMonth), 2));

            double ttest = confidence * Math.Sqrt((AnalyzedReviews.Count() - 2) / 1 - (confidence * confidence));

            return ttest;
        }
        

        public static ChiSquareTest CalculateCorrelationUserToGradeChiSquare()
        {
            var users = DataHandler.Reviews.Select(u => u.reviewerID).Distinct();
            var reviewGrades =new int[] { 1, 2, 3, 4, 5 };
            var total = DataHandler.Reviews.Count;
            var df = (users.Count() - 1) ; 

            List<double> expected = new List<double>();
            List<double> observed = new List<double>();
            
            foreach (var user in users)
            {
                foreach (var grade in reviewGrades)
                {
                    var rowTotal = DataHandler.Reviews.Count(u => u.reviewerID == user);
                    var columnTotal = DataHandler.Reviews.Count(r => r.overall == grade);
                    double expectedCount = ((double)rowTotal / (double)total) * columnTotal;
                    double observedCount = DataHandler.Reviews.Count(r => r.overall == grade && r.reviewerID == user);
                    expected.Add(expectedCount);
                    observed.Add(observedCount);
                }
            }

            ChiSquareTest chi = new ChiSquareTest(expected.ToArray(), observed.ToArray(), df);
            double pValue = chi.PValue;
            bool significant = chi.Significant;

            return chi;
        }
    }
}
