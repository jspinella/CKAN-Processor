using System;

namespace CKANScraper
{
    class Program
    {
        // Pass in base URL for a CKAN-backed API, the program does the rest
        // (given an optional list of keywords, find data that matches these keywords
        //  and put them in our CKAN instance)
        static void Main(string[] args)
        {
            if (args[0] != null)
            {
                Console.WriteLine("Hello World!");
            }
            else
            {
                Console.WriteLine("Please provide a URL to scrape (e.g. \"dotnet CKANScraper.dll https://api.ckan.com/v3\"");
            }
        }
    }
}
