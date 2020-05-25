using Models.Rabbit;
using Repositories;
using System;
using System.Threading.Tasks;
using System.Linq;
using Models.CKAN;
using Models;
using System.Collections.Generic;

namespace CentralExecutive.Services
{
    internal class PackageService
    {
        public PackageRepository packageRepo = new PackageRepository();

        // validate package (see that the PackageName contains a match to a tag in at least one group in local CKAN
        // package is valid if list of groups isn't empty
        public async Task<List<string>> ValidatePackage(string packageName, List<string> resources, List<Group> groups)
        {
            var results = new List<string>();
            foreach (var group in groups)
            {
                if (group.Tags.Any(s => packageName.Contains(s, StringComparison.OrdinalIgnoreCase)))
                    results.Add(group.Id);
                //TODO: do this in LINQ
                foreach (var resource in resources)
                {
                    if (group.Tags.Any(s => resource.Contains(s, StringComparison.OrdinalIgnoreCase)))
                        results.Add(group.Id);
                }
            }

            return results.Distinct().ToList();
        }   

        public async Task<PackageResult2> ProcessPackage(PackageMessage package, string packageName, string orgName, List<string> groups)
        {
            var url = package.SourceUrl;
            var existingPackage = (await packageRepo.GetPackage(packageName)).result.results
                    .FirstOrDefault(x => x?.organization?.name == orgName && x?.name == packageName);

            while (existingPackage == null)
            {
                Console.WriteLine("No existing package found, adding...");
                await packageRepo.CreatePackage(packageName, url, groups, package.PackageTitle, package.Author, package.Notes, orgName);
                var existingPackageResponse = packageRepo.GetPackage(packageName).Result.result.results;
                existingPackage = existingPackageResponse.FirstOrDefault(x => x.organization?.name == orgName && x?.name == packageName);
            }

            Console.WriteLine($"Existing package found for {orgName}");
            return existingPackage;
        }
    }
}
