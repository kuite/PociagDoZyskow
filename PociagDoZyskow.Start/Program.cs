using System;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.Start
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new Company();
            Console.WriteLine("dotnet ef database update --project ../PociagDoZyskow.DataAccess -c StockExchangeContext");
        }
    }
}
