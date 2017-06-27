using LinqToExcel;
using SentimentAnalysis.SentimentModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SentimentAnalysis.VisualisationModule.Controllers
{
    public class CorpusController : Controller
    {
        // GET: Corpus
        public ActionResult Index()
        {
            var model = CorpusAnalizator.AnalyzeCorpus();
            return View(model);
        }

        public ActionResult AnalyzeDependency()
        {
            DataHandler.ImportReviewData(5);

            StatisticalAnalysisData data = new StatisticalAnalysisData();
            data.MonthToGradeDependency = FeatureDependencyAnalizator.AnalyzeMonthToGradeDependency();
            data.MothToGradeTTest = FeatureDependencyAnalizator.CalculateCorrelationMonthToGrade();
            data.YearToGradeDependency = FeatureDependencyAnalizator.AnalyzeYearToGradeDependency();
            data.YearToGradeTTest = FeatureDependencyAnalizator.CalculateCorrelationYearToGrade();
            data.YearToSentimentDependency = FeatureDependencyAnalizator.AnalyzeYearToSentimentDependency();
            data.YearToSentimentTTest = FeatureDependencyAnalizator.CalculateCorrelationYearToSentiment();
            data.UserToGradeCorrelation = FeatureDependencyAnalizator.CalculateCorrelationUserToGradeChiSquare();

            return View(data);
        }
    }

}