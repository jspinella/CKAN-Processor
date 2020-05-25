
using CentralExecutive.Utilities;
using Models.Rabbit;
using Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CentralExecutive.Services
{
    internal class OrganizationService
    {
        private OrganizationRepository orgRepo = new OrganizationRepository();

        /// <summary>
        /// See if organization name already exists in CKAN; if not, add it
        /// </summary>
        /// <returns></returns>
        public async Task<string> ProcessOrganization(PackageMessage package)
        {
            var orgName = StringUtils.ParseName(package.OrganizationTitle);
            var existingOrg = GetExistingOrganization(orgName);
            if (existingOrg == null)
            {
                await orgRepo.CreateOrganization(orgName, package.OrganizationTitle, package.OrganizationDescription, package.OrganizationImageUrl);
                Console.WriteLine($"Organization {package.OrganizationTitle} created");
            }
            else
                Console.WriteLine("Organization already exists");

            return existingOrg != null ? existingOrg : orgName;
        }

        private string GetExistingOrganization(string newOrg)
        {
            var existingOrgs = orgRepo.GetOrganizations().Result; //TODO specify a max limit or pass in first word of orgName to limit results
            var existingOrg = existingOrgs.result.Where(x => DamerauLevenshtein.Distance(x, newOrg) < 5).SingleOrDefault(); // Some organizations have duplicates with very similar names
            return existingOrg;
        }
    }
}
