using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using SentimentAnalysis.SentimentModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalysis.DataOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write down what you want to do and press enter!");
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("Excell n - exports set 1-5 to excell");
            Console.WriteLine("order - orders reviews by user and gets the first 2000 ");
            var command = Console.ReadLine().Trim();
            if(command== "order")
            {
                ExecuteOrdering();
            }
            else
            {
                var number =Int32.Parse( command.Split(' ')[1]);
                ExecuteExtration(number);
            }
        }

        private static void ExecuteExtration(int number)
        {
            DataHandler.number = number;
            var rawModel = DataHandler.Reviews;


            ExcelUtlity obj = new ExcelUtlity();
            System.Data.DataTable dt = obj.ConvertToDataTable(rawModel);
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Data.xlsx";
            obj.WriteDataTableToExcel(dt, "Reviews", path, "Details");
            
        }

        private static List<ReviewData> _reviews;
        private static void ExecuteOrdering()
        {
            _reviews = new List<ReviewData>();
            string outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(outputDirectory, "Cell_Phones_and_Accessories_5.json");
            using (var stream = new StreamReader(path))
            {
                string line;
                ReviewData result;
                while ((line = stream.ReadLine()) != null)
                {
                    result = JsonConvert.DeserializeObject<ReviewData>(line);
                    _reviews.Add(result);
                }
            }

            var orderedReviewList = _reviews.GroupBy(r => r.reviewerID).OrderByDescending(g => g.Count()).SelectMany(g => g).Take(2000);
            var outputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\newReviews.json";

            using (StreamWriter file = File.CreateText(outputPath))
            {
                foreach (ReviewData data  in orderedReviewList)
                {
                    var obj = JsonConvert.SerializeObject(data);
                    file.WriteLine(obj);
                }
                
            }
                
        }
    }
}
