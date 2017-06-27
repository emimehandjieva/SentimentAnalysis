using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public static class SentimentAnalizator
    {
        public static SentimentAnalysisData AnalyzeReview(ReviewData data)
        {
            SentimentAnalysisData result = new SentimentAnalysisData();
            result.Review = data;
            var review = data.reviewText.ToLower().Split(' ');
            List<decimal> resultValues = new List<decimal>();
            int positiveCount = 0;
            int negativeCount = 0;


            //bigrams
            for (int i = 0; i < review.Length - 1; i++)
            {
                if (review[i] != "" && review[i + 1] != "")
                {
                    var bigram = review[i] + " " + review[i + 1];
                    if (DataHandler.Bigrams.Keys.Contains(bigram))
                    {
                        decimal value = DataHandler.Bigrams[bigram];
                        if (value > 0)
                        {
                            positiveCount++;
                        }
                        else if (value < 0)
                        {
                            negativeCount++;
                        }
                        resultValues.Add(value);
                        review[i] = "";
                        review[i + 1] = "";
                    }
                }
            }

            for (int i = 0; i < review.Count(); i++)
            {


                decimal multiplier = 1;
                decimal value;
                bool shouldNegate = false;
                //negation
                //if (DataHandler.NegationWords.Contains(review[i]))
                //{
                //    shouldNegate = true;

                //    //Search for the next word that has some sentiment and negate it 
                //    if (DataHandler.Lexicon.Keys.Contains(review[i+1]))
                //    {
                //        i++;
                //    }
                //    else if (DataHandler.Lexicon.Keys.Contains(review[i + 2]))
                //    {
                //        i += 2;
                //    }
                //}

                //enchancement
                if (review[i] == "" )
                {
                    continue;
                }
                    if (DataHandler.Intensifiers.Keys.Contains(review[i]))
                {
                    multiplier += DataHandler.Intensifiers[review[i]];
                    i++;
                }

                if (i >= review.Length)
                {
                    break;
                }
                //regular parsing
                //TODO: think about a bigger lexicon

                if ((DataHandler.Lexicon.Keys.Contains(review[i])) == false)
                {
                    continue;
                }
                if (shouldNegate)
                {

                    value = (multiplier * (ApplyNegativeCalculationOnWord(review[i])));

                }
                else
                {
                    value = (multiplier * (DataHandler.Lexicon[review[i]]));
                }
                if (value > 0)
                {
                    positiveCount++;
                }
                else if (value < 0)
                {
                    negativeCount++;
                }
                resultValues.Add(value);
            }



            result.PositiveWordsCount = positiveCount;
            result.NegativeWordsCount = negativeCount;
            var totalEvaluated = positiveCount + negativeCount;
            if(totalEvaluated>0)
            {
                result.SentimentEvaluation = (double)CalculateOveralSentiment(resultValues, positiveCount,negativeCount,review.Length);

            }
            else
            {
                result.SentimentEvaluation = 0;
            }
            return result;
        }

        private static decimal CalculateOveralSentiment(List<decimal> resultValues, int Wp, int Wn, int length)
        {
            
            decimal Ap=0;
            decimal An=0;
            if (Wp > 0)
            {

                var positiveResults = resultValues.Where(x => x > 0);
                 Ap = positiveResults.Sum(x => x) / Wp;
            }
            if (Wn > 0)
            {
                var negativeResults = resultValues.Where(x => x < 0);
                 An = negativeResults.Sum(x => x) / Wn;
            }
            
            decimal value = 0;
            if (Ap > Math.Abs(An))
            {
                value = 4+Ap;
            }
            else if (Math.Abs(An) > Ap)
            {
                value =2+ An;
            }
            
            return value;
        }
        
        private static decimal ApplyNegativeCalculationOnWord(string word)
        {
            int wordSentiment = DataHandler.Lexicon[word];
            if (wordSentiment < 0)
            {
                decimal negatedValue = (wordSentiment + 100 / 2);
                decimal threshold = 10;
                return Math.Max(negatedValue, threshold);
            }
            if (wordSentiment > 0)
            {
                decimal negatedValue = (wordSentiment - 100 / 2);
                decimal threshold = -10;
                return Math.Min(negatedValue, threshold);
            }

            return 0;
        }
    }
}
