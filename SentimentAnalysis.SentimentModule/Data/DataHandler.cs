using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public static class DataHandler
    {
        public static int number;

        private static List<ReviewData> _reviews;
        public static List<ReviewData> Reviews
        {
            get
            {
                if (_reviews == null)
                {
                    if (number == 0)
                    {

                        ImportReviewData(5);
                    }
                    else
                    {
                        ImportReviewData(number);
                    }
                }

                return _reviews;
            }
        }


        private static List<string> _negationWords;

        public static List<string> NegationWords
        {
            get
            {
                if(_negationWords == null)
                {
                    ImportNegationWords();
                }
                return _negationWords;
            }
            
        }


        private static Dictionary<string, decimal> _intensifiers;

        public static Dictionary<string, decimal> Intensifiers
        {
            get
            {
                if (_intensifiers == null)
                {
                    ImportIntensifiers();
                }
                return _intensifiers;
            }


        }


        private static Dictionary<string,int> _lexicon;

        public static Dictionary<string, int> Lexicon
        {
            get
            {
                if (_lexicon == null)
                {
                    ImportLexicon();
                }
                return _lexicon;
            }


        }

        private static List<string>  _stopWords;

        public static List<string> StopWords
        {
            get
            {
                if (_stopWords == null)
                {
                    ImportStopWords();
                }
                return _stopWords;
            }


        }

        private static Dictionary<string, decimal> _bigrams;

        public static Dictionary<string, decimal> Bigrams
        {
            get
            {
                if (_bigrams == null)
                {
                    ImportBigrams();
                }
                return _bigrams;
            }


        }

        private static void ImportStopWords()
        {
            _stopWords = new List<string>();
            string outputDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            outputDirectory = Path.Combine(outputDirectory, "Lexicon");
            string path = Path.Combine(outputDirectory, "StopWords.txt");
            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    _stopWords.Add(line.Trim());
                }
            }
        }

        private static void ImportIntensifiers()
        {
            _intensifiers = new Dictionary<string, decimal>();
            string outputDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            outputDirectory = Path.Combine(outputDirectory, "Lexicon");
            string path = Path.Combine(outputDirectory, "intensifiers.txt");
            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    var word = line.Trim().Split(' ')[0];
                    var sentiment = Decimal.Parse(line.Split(' ')[1]);
                    _intensifiers.Add(word, sentiment);
                }
            }
        }

        private static void ImportLexicon()
        {
            _lexicon = new Dictionary<string, int>();
              string outputDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            outputDirectory = Path.Combine(outputDirectory, "Lexicon");
            string path = Path.Combine(outputDirectory, "AFINN-en-165.txt");
            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    line = line.Trim().Replace("\t", " ");
                    var word = line.Split(' ')[0];
                    var sentiment =int.Parse( line.Split(' ')[1]);
                    _lexicon.Add(word,sentiment);
                }
            }
        }

        private static void ImportNegationWords()
        {
            _negationWords = new List<string>();
            string outputDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            outputDirectory = Path.Combine(outputDirectory, "Lexicon");
            string path = Path.Combine(outputDirectory, "negationWords.txt");
            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null )
                {
                    _negationWords.Add(line.Trim());
                }
            }
        }

        private static void ImportBigrams()
        {
            _bigrams = new Dictionary<string, decimal>();
            string outputDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            outputDirectory = Path.Combine(outputDirectory, "Lexicon");
            string path = Path.Combine(outputDirectory, "bigrams-pmilexicon.txt");
            using (var stream = new StreamReader(path))
            {
                string line;
                int delimeter = 9;
                char _delimeter = Convert.ToChar(delimeter);
                while ((line = stream.ReadLine()) != null)
                {
                    var values = line.Split(_delimeter);
                    _bigrams.Add(values[0], Convert.ToDecimal(values[1].Replace(".",",")));
                }
            }
        }

        public static void ImportReviewData(int count)
        {
            _reviews = new List<ReviewData>();
            string outputDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            outputDirectory = Path.Combine(outputDirectory, "Data");
            string path = Path.Combine(outputDirectory, "Cell_Phones_and_Accessories_small"+count+".json");
            using (var stream = new StreamReader(path))
            {
                string line;
                ReviewData result;
                while ((line = stream.ReadLine()) != null)
                {
                    result = JsonConvert.DeserializeObject<ReviewData>(line);
                    if(result == null)
                    {
                        var error = "wtf";
                    }
                    _reviews.Add(result);
                }
            }
        }
    }
}
