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

        public ActionResult Analyze(string asin,string reviewerID )
        {
            DataHandler.ImportReviewData(1);
            var model = DataHandler.Reviews.Where(review => review.reviewerID == reviewerID && review.asin == asin).First();
            var analysisModel = SentimentAnalizator.AnalyzeReview(model);
            return View(analysisModel);
        }

        public ActionResult AnalyzeAll()
        {
            DataHandler.ImportReviewData(1);
            var model = new List<SentimentAnalysisData>();
            foreach (ReviewData review in DataHandler.Reviews)
            {
                model.Add(SentimentAnalizator.AnalyzeReview(review));
            }
            return View(model.OrderBy(m=>m.Review.overall));
        }

        public ActionResult ChartView(PieData data)
        {
            return View(data);
        }

    }
}