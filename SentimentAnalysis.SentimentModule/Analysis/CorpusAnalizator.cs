using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public static class CorpusAnalizator
    {
        public static List<NLPData> AnalyzeCorpus()
        {
            var corpus = DataHandler.Reviews.Select(r => r.reviewText);
            var outputWords = new List<NLPData>();
            List<List<string>> stemList = new List<List<string>>();
            long id = 1;
            foreach (var review in corpus)
            {
                var stemmer = new EnglishStemmer();
                var reviewContent = review.Split(' ');
                List<string> currentStemList = new List<string>();
                List<NLPData> currentWords = new List<NLPData>();
                //handle initial word analysis
                foreach (var word in reviewContent)
                {
                    if (!DataHandler.StopWords.Contains(word))
                    {
                        NLPData newWord = new NLPData()
                        { Word = word,
                          ID = id,
                          Stem = stemmer.Stem(word),
                    };
                        currentStemList.Add(newWord.Stem);
                        id++;
                        currentWords.Add(newWord);
                    }
                }

                foreach (var item in currentWords)
                {
                    item.Tf = currentWords.Count(i => i.Stem == item.Stem);
                }
                outputWords.AddRange(currentWords.Where(word => !string.IsNullOrEmpty(word.Stem) && !string.IsNullOrEmpty(word.Word)));
                stemList.Add(currentStemList);
            }
            foreach (var word in outputWords)
            {
                word.Idf = (stemList.Count() / (stemList.Count(doc => doc.Contains(word.Stem))));
                word.TfIdf = word.Tf / word.Idf;
            }

            return outputWords;
        }

    }
}
