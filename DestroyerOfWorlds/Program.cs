using Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DestroyerOfWorlds
{
    class Program
    {
        /// <summary>
        /// Deletes all organizations, packages, and resources in CKAN
        /// !!!!!!!!!!!!!! DO NOT RUN THIS EVER !!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var processor = new Processor();
            var deleteConfirm = Shared._baseUrl;
            Console.WriteLine($"This will wipe out all data in the CKAN instance at {Shared._baseUrl} PERMANENTLY");
            Console.WriteLine("Please ensure you are running this script against the correct CKAN instance");
            Console.WriteLine("Type the following text to continue:");
            Console.WriteLine(deleteConfirm);
            var userConfirm = Console.ReadLine();

            if (userConfirm != null && userConfirm.Equals(deleteConfirm))
            {
                Console.WriteLine("\nVerification accepted, deleting all data in CKAN instance...");
                processor.DeleteAllData().Wait();
            }
        }
    }
}
