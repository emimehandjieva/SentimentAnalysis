using Accord.Statistics.Models.Regression.Linear;
using Accord.Statistics.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public class StatisticalAnalysisData 
    {
        private SimpleLinearRegression _monthToGradeDependency;

        private double _mothToGradeTTest;

        private SimpleLinearRegression _yearToGradeDependency;

        private double _yearToGradeTTest;

        private SimpleLinearRegression _yearToSentimentDependency;

        private double _yearToSentimentTTest;

        private ChiSquareTest _userToGradeCorrelation;

        public SimpleLinearRegression MonthToGradeDependency
        {
            get
            {
                return _monthToGradeDependency;
            }

            set
            {
                _monthToGradeDependency = value;
            }
        }

        public double MothToGradeTTest
        {
            get
            {
                return _mothToGradeTTest;
            }

            set
            {
                _mothToGradeTTest = value;
            }
        }

        public SimpleLinearRegression YearToGradeDependency
        {
            get
            {
                return _yearToGradeDependency;
            }

            set
            {
                _yearToGradeDependency = value;
            }
        }

        public double YearToGradeTTest
        {
            get
            {
                return _yearToGradeTTest;
            }

            set
            {
                _yearToGradeTTest = value;
            }
        }

        public SimpleLinearRegression YearToSentimentDependency
        {
            get
            {
                return _yearToSentimentDependency;
            }

            set
            {
                _yearToSentimentDependency = value;
            }
        }

        public double YearToSentimentTTest
        {
            get
            {
                return _yearToSentimentTTest;
            }

            set
            {
                _yearToSentimentTTest = value;
            }
        }

        public ChiSquareTest UserToGradeCorrelation
        {
            get
            {
                return _userToGradeCorrelation;
            }

            set
            {
                _userToGradeCorrelation = value;
            }
        }

        public string UserToGradeCorrelationText
        {
            get
            {
                return _userToGradeCorrelation.Significant? "Correlation is statistically significant" : "Correlation isn't statistically significant";
            }
        }

        public string Year
        {
            get;
            set;
        }

        public double YearToGradeCorrelationResult
        {
            get
            {
                int year = string.IsNullOrEmpty(this.Year) ? 0 : Int32.Parse(Year);
                return this.YearToGradeDependency.Transform(year);
            }
        }

        public double YearToSentimentCorrelationResult
        {
            get
            {
                int year = string.IsNullOrEmpty(this.Year) ? 0 : Int32.Parse(Year);
                return this.YearToSentimentDependency.Transform(year);
            }
        }
    }
}
