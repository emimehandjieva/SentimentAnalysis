using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.SentimentModule
{
    public class ReviewData
    {
        //ID of the person who wrote the review
        public string reviewerID { get; set; }

        //ID of the product itself
        public string asin { get; set; }

        //NAme of the reviewer
        public string reviewerName { get; set; }

        //how helpful the review is, in the form of an array
        //the first element depicts how many people rated iut as useful and the second represents the total number of peole who rated the review itself
        public List<int> helpful { get; set; }

        //Text that reflects the reviewer's opinion
        public string reviewText { get; set; }

        //Review rating,a.k.a. polarity as viewed by the reviewer themself
        public double overall { get; set; }

        //More or less a title for the review, less likely to be an actual summary-
        public string summary { get; set; }

        //unixReviewTime - time of the review(unix time)
        public int unixReviewTime { get; set; }

        // time of the review (raw)
        public DateTime reviewTime { get; set; }
    }
}
