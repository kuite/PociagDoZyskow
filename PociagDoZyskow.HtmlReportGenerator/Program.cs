using System;

namespace PociagDoZyskow.HtmlReportsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter days to include financial reports, eg. 10 means program will take financial reports up to 10 days from today");
            int days = Convert.ToInt32(Console.ReadLine());


        }
    }
}
