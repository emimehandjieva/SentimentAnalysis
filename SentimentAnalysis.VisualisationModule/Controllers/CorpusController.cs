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

        //public void ExportToExcell()
        //{
        //    var rawModel = DataHandler.Reviews;


        //    ExcelUtlity obj = new ExcelUtlity();
        //    DataTable dt = obj.ConvertToDataTable(rawModel);
        //    var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Data.xlsx";
        //    obj.WriteDataTableToExcel(dt, "Reviews",path , "Details");

        //    System.Diagnostics.Process.Start(path);

        //    ReviewController ctrl = new ReviewController();
        //    ctrl.Index();

        //}

      
    }
}