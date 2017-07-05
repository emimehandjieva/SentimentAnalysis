using libsvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public static class SVMSentimentAnalyzator
    {
        private static C_SVC model;
        public static void Train()
        {
            DataHandler.ImportReviewData(3);
            var x = DataHandler.Reviews.Select(r=>r.reviewText);
            double[] y = DataHandler.Reviews.Select(r => r.overall).ToArray();
            

            var problemBuilder = new TextClassificationProblemBuilder();
            var problem = problemBuilder.CreateProblem(x, y, DataHandler.Vocabulary);
            const int C = 1;
            model = new C_SVC(problem, KernelHelper.LinearKernel(), C);
        }

        public static SentimentAnalysisData AnalyzeReview(ReviewData data)
        {
            SentimentAnalysisData result = new SentimentAnalysisData { Review = data };
            var newText = TextClassificationProblemBuilder.CreateNode(data.reviewText, DataHandler.Vocabulary);
            result.SentimentEvaluation = model.Predict(newText);
            return result;
        }
    }
}
