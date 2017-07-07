using SentimentAnalysis.SentimentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SentimentAnalysis.VisualisationModule.Controllers
{
    public class ReviewController : Controller
    {

        // GET: Review
        public ActionResult Index()
        {
            DataHandler.ImportReviewData(1);
            var model = DataHandler.Reviews;
            return View(model);
        }

        public ActionResult AnalyzeLexiconally(string asin, string reviewerID)
        {
            DataHandler.ImportReviewData(1);
            var model = DataHandler.Reviews.Where(review => review.reviewerID == reviewerID && review.asin == asin).First();
            var analysisModel = LexiconSentimentAnalizator.AnalyzeReview(model);
            return View(analysisModel);
        }

        public ActionResult AnalyzeAllLexiconally()
        {
            DataHandler.ImportReviewData(1);
            var model = new List<SentimentAnalysisData>();
            double error = 0;
            foreach (ReviewData review in DataHandler.Reviews)
            {
                var currentSentiment = LexiconSentimentAnalizator.AnalyzeReview(review);
                error += Math.Abs(review.overall - currentSentiment.SentimentEvaluation);
                model.Add(currentSentiment);
            }
            Console.WriteLine(error);
            return View(model.OrderBy(m => m.Review.overall));
        }

        public ActionResult ChartView(PieData data)
        {
            return View(data);
        }

        public ActionResult AnalyzeAllNaively()
        {
            NaiveBayesSentimentAnalizator.Train();

            var model = new List<SentimentAnalysisData>();
            double error = 0;

            DataHandler.ImportReviewData(1);

            foreach (ReviewData review in DataHandler.Reviews)
            {
                var currentSentiment = NaiveBayesSentimentAnalizator.AnalyzeReview(review);
                error += Math.Abs(review.overall - currentSentiment.SentimentEvaluation);
                model.Add(currentSentiment);
            }

            Console.WriteLine(error);
            ViewBag.Name = "Naive Bayes Classification";
            return View("AnalyzeAllML", model);
        }

        public ActionResult AnalyzeAllSVMly()
        {
            SVMSentimentAnalyzator.Train();

            var model = new List<SentimentAnalysisData>();
            double error = 0;

            DataHandler.ImportReviewData(1);

            foreach (ReviewData review in DataHandler.Reviews)
            {
                var currentSentiment = SVMSentimentAnalyzator.AnalyzeReview(review);
                error += Math.Abs(review.overall - currentSentiment.SentimentEvaluation);
                model.Add(currentSentiment);
            }

            Console.WriteLine(error);
            ViewBag.Name = "SVM Classification";

            return View("AnalyzeAllML", model);
        }

        public ActionResult AnalyzeAllMaxEntropically()
        {
            MaxEntropySentimentAnalizator.Train();

            var model = new List<SentimentAnalysisData>();
            double error = 0;

            DataHandler.ImportReviewData(1);

            foreach (ReviewData review in DataHandler.Reviews)
            {
                var currentSentiment = MaxEntropySentimentAnalizator.AnalyzeReview(review);
                error += Math.Abs(review.overall - currentSentiment.SentimentEvaluation);
                model.Add(currentSentiment);
            }

            Console.WriteLine(error);
            ViewBag.Name = "Maximum Entropy Classification";
            return View("AnalyzeAllML", model);
        }


        public ActionResult CompareResults()
        {
            NaiveBayesSentimentAnalizator.Train();
            SVMSentimentAnalyzator.Train();
            MaxEntropySentimentAnalizator.Train();

            DataHandler.ImportReviewData(1);

            var model = new List<MultipleSentimentAnalysisData>();
            foreach (ReviewData review in DataHandler.Reviews)
            {
                MultipleSentimentAnalysisData resultItem = new MultipleSentimentAnalysisData() { ReviewText = review.reviewText, Grade = review.overall };
                var currentSentiment = LexiconSentimentAnalizator.AnalyzeReview(review);
                resultItem.LexiconSentimentEvaluation = currentSentiment.SentimentEvaluation;
                currentSentiment = NaiveBayesSentimentAnalizator.AnalyzeReview(review);
                resultItem.NaiveBayesSentimentEvaluation = currentSentiment.SentimentEvaluation;
                currentSentiment = SVMSentimentAnalyzator.AnalyzeReview(review);
                resultItem.SVMSentimentEvaluation = currentSentiment.SentimentEvaluation;

                model.Add(resultItem);
            }

            return View(model);

        }

    }
}