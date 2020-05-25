using Models.CKAN;
using Models.Rabbit;
using Repositories;
using System;
using System.Threading.Tasks;
using System.Linq;
using CentralExecutive.Utilities;
using System.Collections.Generic;
using Models;
using Models.Enums;

namespace CentralExecutive.Services
{
    internal class ResourceService
    {
        public ResourceRepository resRepo = new ResourceRepository();

        public async Task<Models.CKAN.Resources.Result2> ProcessResource(string resource, PackageResult2 existingPackage, string packageName)
        {
            var url = resource;
            var existingResource = (await resRepo.GetResourceByUrl(url)).result.results
                    .FirstOrDefault(x => x.package_id == existingPackage.id);

            while (existingResource == null)
            {
                Console.WriteLine("No existing resource found, adding...");

                var format = StringUtils.GuessFileFormat(url);
                await resRepo.CreateResource(existingPackage.id, url, format.ToString(), packageName);
                existingResource = (await resRepo.GetResourceByUrl(url)).result.results
                    .FirstOrDefault(x => x.package_id == existingPackage.id);
            }

            if (existingResource != null)
                Console.WriteLine($"Existing resource found for { packageName }");

            return existingResource;
        }

        //TODO parser that downloads a resource file given a URL, parses it to a C# object or array or some other thing, and looks for keyword matches
        // returns true if resource contains at least one keyword

        public async Task<List<string>> ValidateResources(PackageMessage package)
        {
            var results = new List<string>();
            foreach (var resource in package.Resources)
            {
                // validate file type
                var fileType = StringUtils.GuessFileFormat(resource);
                if (fileType != FileType.Unsupported)
                    results.Add(resource);

                //actually try to download the file... I feel like many/most of these datasets on data.gov are broken links
            }

            return results;
        }

    }
}
